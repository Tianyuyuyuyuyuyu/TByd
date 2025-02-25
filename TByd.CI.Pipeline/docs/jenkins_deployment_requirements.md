# Git + GitHub + Nexus 实施步骤

## 1. 版本控制：Git + GitHub

### 1.2 GitHub 配置
1. 在 GitHub 上创建仓库
2. 配置 GitHub Webhook：
   - URL: https://ci.tianyuyuyu.com/github-webhook/
   - 事件：Push 和 Pull Request

## 2. 构建产物管理：Nexus
### 2.1 Nexus 配置
1. 创建仓库：
   - 类型：raw（托管）
   - 名称：unity-builds
   - 版本策略：Release

2. 配置访问权限：
   - 创建专用账号用于 Jenkins 上传
   - 配置仓库访问权限

## 3. Jenkins 配置
### 3.1 必要插件
- Git Plugin（已安装）
- GitHub Integration（已安装）
- Pipeline（已安装）
- Nexus Artifact Uploader
- Workspace Cleanup（已安装）
- Build Timeout（已安装）

### 3.2 全局工具配置
1. 配置 Git：
   - 自动安装或指定系统 Git 路径

2. 配置 Unity：
   ```groovy
   // 在 Jenkins 全局配置中添加环境变量
   UNITY_PATH = "/path/to/unity/editor"
   ```

### 3.3 Jenkins Pipeline
```groovy
pipeline {
    agent any
    
    environment {
        UNITY_PATH = '/path/to/unity/editor'
        UNITY_PROJECT_PATH = '${WORKSPACE}'
        BUILD_OUTPUT_PATH = 'Builds'
        NEXUS_URL = 'http://your-nexus-url'
    }
    
    options {
        timeout(time: 1, unit: 'HOURS')
        buildDiscarder(logRotator(numToKeepStr: '10'))
    }
    
    stages {
        stage('Cleanup') {
            steps {
                cleanWs()
            }
        }
        
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/yourusername/your-repo.git'
            }
        }
        
        stage('Unity Build') {
            steps {
                sh '''
                    "${UNITY_PATH}" -quit -batchmode -nographics \\
                    -projectPath "${UNITY_PROJECT_PATH}" \\
                    -executeMethod Builder.BuildGame \\
                    -logFile unity_build.log
                '''
            }
        }
        
        stage('Upload to Nexus') {
            steps {
                nexusArtifactUploader(
                    nexusVersion: 'nexus3',
                    protocol: 'http',
                    nexusUrl: "${NEXUS_URL}",
                    groupId: 'com.yourcompany',
                    version: "${BUILD_NUMBER}",
                    repository: 'unity-builds',
                    credentialsId: 'nexus-credentials',
                    artifacts: [
                        [artifactId: 'game-windows',
                         classifier: '',
                         file: "${BUILD_OUTPUT_PATH}/Windows/Game.exe",
                         type: 'exe']
                    ]
                )
            }
        }
    }
    
    post {
        always {
            archiveArtifacts artifacts: 'unity_build.log', allowEmptyArchive: true
        }
        failure {
            emailext (
                subject: "构建失败: ${env.JOB_NAME} [${env.BUILD_NUMBER}]",
                body: "查看详情: ${env.BUILD_URL}",
                recipientProviders: [developers(), requestor()]
            )
        }
    }
}
```

### 3.4 Unity 构建脚本
在 Unity 项目中创建构建脚本（`Assets/Editor/Builder.cs`）：
```csharp
using UnityEditor;
using System;

public class Builder
{
    public static void BuildGame()
    {
        try
        {
            string[] scenes = FindEnabledEditorScenes();
            string buildPath = System.Environment.GetEnvironmentVariable("BUILD_OUTPUT_PATH") ?? "Builds";
            
            BuildPipeline.BuildPlayer(scenes, buildPath + "/Windows/Game.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        }
        catch (Exception e)
        {
            Console.WriteLine($"构建失败: {e.Message}");
            EditorApplication.Exit(1);
        }
    }
    
    private static string[] FindEnabledEditorScenes()
    {
        var scenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
                scenes.Add(scene.path);
        }
        return scenes.ToArray();
    }
}
```

## 4. 工作流程
1. 开发人员提交代码到 GitHub
2. GitHub Webhook 触发 Jenkins 构建
3. Jenkins 执行构建流程：
   - 清理工作空间
   - 检出代码
   - 执行 Unity 构建
   - 上传构建产物到 Nexus
4. 发送构建结果通知

## 5. 注意事项
1. Unity 命令行参数说明：
   - `-quit`: 构建完成后退出
   - `-batchmode`: 无界面模式
   - `-nographics`: 禁用图形设备
   - `-logFile`: 指定日志文件路径

2. 性能优化：
   - 合理设置构建超时时间
   - 定期清理工作空间
   - 优化构建日志保留策略

3. 故障排除：
   - 检查 Unity 日志文件
   - 验证 Nexus 连接和认证
   - 确保构建脚本权限正确 