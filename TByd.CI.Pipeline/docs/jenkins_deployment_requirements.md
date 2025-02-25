# Jenkins 部署需求描述

## 1. 项目背景
为了实现高效的持续集成和持续交付（CI/CD）流程，我们计划在阿里云服务器上部署 Jenkins。该部署将支持我们的 Unity 项目，确保构建过程的自动化、稳定性和可维护性。

## 2. 部署目标
- 在阿里云服务器上成功安装和配置 Jenkins。
- 使用已备案的域名（ci.tianyuyuyu.com）进行访问。
- 确保 Jenkins 能够与我们的版本控制系统（如 Git）和构建工具（如 Unity）无缝集成。

## 3. 硬件和软件要求
- **服务器配置**：
  - 操作系统：Ubuntu 22.04.5 LTS
  - CPU：至少 2 核心
  - 内存：至少 4 GB RAM
  - 存储：至少 50 GB 可用空间

- **软件要求**：
  - Java 11 或更高版本
  - Jenkins 最新稳定版
  - 必要的 Jenkins 插件（如 Git、Pipeline、Docker 等）

## 3.1 Docker 部署建议
- 使用 Docker 部署 Jenkins，以实现更好的隔离性和资源管理。
- 可以使用以下命令拉取 Jenkins 官方镜像并运行：
  ```bash
  docker run -d -p 8080:8080 -p 50000:50000 --name jenkins \
  -v jenkins_home:/var/jenkins_home \
  jenkins/jenkins:lts
  ```
- 确保在 Docker 容器中配置必要的 Jenkins 插件和环境变量。

## 3.2 镜像管理建议
- 在部署 Jenkins 之前，先尝试从 Docker Hub 拉取相关镜像，以确保能够正常获取所需的镜像。
- 如果在中国无法正常拉取镜像，考虑使用阿里云容器镜像服务（ACR）创建自己的镜像仓库，以确保在中国的访问速度。
- 在 Dockerfile 中使用阿里云的镜像地址，确保快速拉取。
- 也可以使用其他国内镜像源（如 DaoCloud、网易云）来加速镜像拉取。
- 如果有特定的镜像需求，建议在本地构建并缓存这些镜像，避免频繁拉取。

## 3.3 所需 Docker 镜像
- **Jenkins 基础镜像**：
  - `jenkins/jenkins:lts-jdk11`（最新 LTS 版本）
  - 或 `jenkins/jenkins:2.426.1-lts-jdk11`（指定版本号）

- **Unity 构建环境**：
  - `unityci/editor:2023.2.9f1-linux-il2cpp-2.0`
  - 注意：Unity 镜像版本需要与项目使用的 Unity 版本保持一致

- **Docker-in-Docker（可选）**：
  - `docker:24.0.7-dind`
  - 用于在 Jenkins 中运行 Docker 命令

- **Node.js（可选，用于前端构建）**：
  - `node:20.11.1-alpine`
  - 轻量级版本，用于运行前端构建任务

- **代码质量分析**：
  - `sonarqube:9.9.4-community`
  - 用于静态代码分析和代码质量检查

- **监控工具**：
  - `prom/prometheus:v2.49.1`（监控数据收集）
  - `grafana/grafana:10.3.3`（监控数据可视化）
  - 用于监控 Jenkins 和构建任务的性能

- **测试环境（根据项目需求选择）**：
  - `nunit/nunit:3.16.3`（如果使用 NUnit 测试）
  - `mcr.microsoft.com/dotnet/sdk:8.0`（如果需要 .NET 环境）

注意：
1. 在拉取这些镜像之前，建议先测试是否能从 Docker Hub 直接拉取。
2. 如果遇到网络问题，可以使用前述的阿里云容器镜像服务或其他国内镜像源。
3. 某些镜像可能需要根据具体项目需求和版本兼容性进行调整。
4. 建议在测试环境中验证所有镜像的兼容性后再部署到生产环境。

## 4. 安全性要求
- 配置安全组，限制对 Jenkins 服务器的访问，仅允许特定 IP 地址访问。
- 启用 HTTPS，确保数据传输的安全性。
- 定期更新 Jenkins 和其插件，确保系统安全。

## 5. 备份和恢复策略
- 定期备份 Jenkins 配置和构建历史，确保数据的安全性。
- 制定恢复计划，以便在发生故障时能够快速恢复服务。

## 6. 监控和日志
- 配置监控工具（如 Prometheus、Grafana）监控 Jenkins 的性能和健康状态。
- 设置日志管理，确保构建日志和系统日志能够被有效存储和分析。

## 7. 域名配置
- 将域名（ci.tianyuyuyu.com 或 jenkins.tianyuyuyu.com）解析到阿里云服务器的 IP 地址。
- 配置 DNS 记录，确保域名能够正常访问 Jenkins。

# Git + GitHub + Nexus 实施步骤文档

## 1. 版本控制：Git + GitHub
### 1.1 本地 Git 配置
1. 安装 Git：
   - 在 [Git 官网](https://git-scm.com/) 下载并安装 Git。
2. 初始化 Git 仓库：
   ```bash
   cd /path/to/your/unity/project
   git init
   ```
3. 创建 `.gitignore` 文件，确保忽略不必要的文件：
   ```gitignore
   Library/
   Temp/
   Logs/
   ```
4. 提交初始代码：
   ```bash
   git add .
   git commit -m "初始提交"
   ```

### 1.2 GitHub 配置
1. 创建 GitHub 仓库：
   - 登录 GitHub，点击 "New" 创建新仓库。
2. 将本地仓库推送到 GitHub：
   ```bash
   git remote add origin https://github.com/yourusername/your-repo.git
   git push -u origin master
   ```
3. 配置分支管理和 Pull Request：
   - 在 GitHub 上创建分支，进行代码开发。
   - 提交 Pull Request 进行代码审查。

## 2. 构建产物管理：Nexus
### 2.1 Nexus 安装与配置
1. 下载 Nexus Repository Manager：
   - 从 [Nexus 官网](https://www.sonatype.com/nexus-repository-oss) 下载并安装 Nexus。
2. 启动 Nexus：
   ```bash
   ./nexus start
   ```
3. 访问 Nexus 界面：
   - 打开浏览器，访问 `http://localhost:8081`。
4. 创建仓库：
   - 在 Nexus 中创建一个新的仓库，用于存储构建产物。

### 2.2 上传构建产物
1. 在 Jenkins 中配置 Nexus 插件。
2. 在 Jenkins Pipeline 中添加上传构建产物的步骤：
   ```groovy
   stage('Upload to Nexus') {
       steps {
           nexusArtifactUploader artifacts: [[artifactId: 'your-artifact', classifier: '', file: 'builds/YourGame.exe', type: 'exe']], 
           nexusUrl: 'http://your-nexus-url', 
           groupId: 'com.yourcompany', 
           nexusVersion: 'nexus3', 
           repository: 'your-repo', 
           version: '1.0.0'
       }
   }
   ```

## 3. CI/CD 集成：Jenkins
### 3.1 Jenkins 安装与配置
1. 下载并安装 Jenkins：
   - 从 [Jenkins 官网](https://www.jenkins.io/download/) 下载并安装 Jenkins。
2. 启动 Jenkins：
   ```bash
   ./jenkins.war
   ```
3. 访问 Jenkins 界面：
   - 打开浏览器，访问 `http://localhost:8080`。
4. 安装必要的插件：
   - 在 Jenkins 中安装 Git、Nexus 和其他相关插件。

### 3.2 Jenkins Pipeline 配置
1. 创建 Jenkins Pipeline 项目。
2. 在 Jenkinsfile 中定义构建流程：
   ```groovy
   pipeline {
       agent any
       stages {
           stage('Checkout') {
               steps {
                   git 'https://github.com/yourusername/your-repo.git'
               }
           }
           stage('Build') {
               steps {
                   sh 'unity-editor -quit -batchmode -nographics -projectPath /path/to/your/unity/project -executeMethod Builder.BuildGame'
               }
           }
           stage('Upload to Nexus') {
               steps {
                   nexusArtifactUploader artifacts: [[artifactId: 'your-artifact', classifier: '', file: 'builds/YourGame.exe', type: 'exe']], 
                   nexusUrl: 'http://your-nexus-url', 
                   groupId: 'com.yourcompany', 
                   nexusVersion: 'nexus3', 
                   repository: 'your-repo', 
                   version: '1.0.0'
               }
           }
       }
   }
   ```

### 3.3 自动触发构建
1. 配置 GitHub Webhook：
   - 在 GitHub 仓库设置中，添加 Webhook，指向 Jenkins 的构建 URL。
2. 提交代码后，Jenkins 将自动触发构建流程。

## 总结
通过使用 Git + GitHub + Nexus + Jenkins 的组合，可以实现高效的版本控制、构建产物管理和自动化 CI/CD 流程，确保 Unity 项目的持续集成和交付。 