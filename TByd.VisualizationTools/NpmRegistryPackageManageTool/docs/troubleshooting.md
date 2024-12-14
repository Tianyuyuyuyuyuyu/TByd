# 故障排除指南

## 目录

1. [常见问题](#常见问题)
2. [错误代码](#错误代码)
3. [性能问题](#性能问题)
4. [网络问题](#网络问题)
5. [日志分析](#日志分析)

## 常见问题

### 启动问题

#### 问题：应用程序无法启动
- **症状**：双击程序图标后无响应或闪退
- **可能原因**：
  1. 系统权限不足
  2. .NET Framework 版本不匹配
  3. 程序文件损坏
- **解决方案**：
  1. 以管理员身份运行
  2. 安装最新的 .NET Framework
  3. 重新安装程序

#### 问题：首次启动配置失败
- **症状**：无法完成初始化配置
- **可能原因**：
  1. 配置文件访问受限
  2. 磁盘空间不足
- **解决方案**：
  1. 检查用户权限
  2. 清理磁盘空间

### 连接问题

#### 问题：无法连接到 Verdaccio 服务器
- **症状**：连接超时或认证失败
- **可能原因**：
  1. 网络连接不稳定
  2. 服务器地址错误
  3. 认证信息过期
- **解决方案**：
  1. 检查网络连接
  2. 验证服务器地址
  3. 更新认证信息

#### 问题：SSL 证书错误
- **症状**：提示证书无效
- **解决方案**：
  1. 更新系统证书
  2. 检查系统时间
  3. 配置信任证书

## 错误代码

### 系统错误

| 错误代码 | 描述 | 解决方案 |
|---------|------|---------|
| E001 | 权限不足 | 以管理员身份运行 |
| E002 | 配置文件损坏 | 重置配置文件 |
| E003 | 依赖缺失 | 安装必要组件 |

### 网络错误

| 错误代码 | 描述 | 解决方案 |
|---------|------|---------|
| N001 | 连接超时 | 检查网络连接 |
| N002 | 认证失败 | 更新认证信息 |
| N003 | 服务器无响应 | 联系服务器管理员 |

## 性能问题

### 内存占用过高
- **症状**：
  - 程序运行缓慢
  - 系统响应迟钝
- **解决方案**：
  1. 清理缓存
  2. 减少同时操作的包数量
  3. 关闭不必要的功能

### 操作响应慢
- **症状**：
  - 界面卡顿
  - 操作延迟
- **解决方案**：
  1. 优化缓存设置
  2. 检查磁盘空间
  3. 更新显卡驱动

## 网络问题

### 代理设置
```json
{
  "proxy": {
    "http": "http://proxy.company.com:8080",
    "https": "http://proxy.company.com:8080",
    "no_proxy": "localhost,127.0.0.1"
  }
}
```

### 网络诊断
1. 检查网络连接
   ```bash
   ping your-verdaccio-server.com
   ```

2. 检查端口连通性
   ```bash
   telnet your-verdaccio-server.com 4873
   ```

3. 检查 DNS 解析
   ```bash
   nslookup your-verdaccio-server.com
   ```

## 日志分析

### 日志位置
- 安装日志：`%PROGRAMDATA%\\NPM Registry Manager\\Logs\\install.log`
- 运行日志：`%APPDATA%\\NPM Registry Manager\\Logs\\app.log`
- 错误日志：`%APPDATA%\\NPM Registry Manager\\Logs\\error.log`

### 日志级别
- ERROR: 严重错误
- WARN: 警告信息
- INFO: 一般信息
- DEBUG: 调试信息

### 常见日志模式

#### 1. 连接失败
```log
[ERROR] Failed to connect to server: Connection timeout
[INFO] Retrying connection (1/3)
[ERROR] Authentication failed
```

#### 2. 操作失败
```log
[ERROR] Failed to publish package: Version conflict
[WARN] Package already exists
[INFO] Rolling back changes
```

## 恢复和备份

### 配置恢复
1. 找到备份文件
2. 替换当前配置
3. 重启应用

### 数据备份
1. 导出当前配置
2. 备份认证信息
3. 保存自定义设置

## 联系支持

如果上述方法无法解决您的问题，请联系技术支持：

- 📧 Email: [tianyulovecars@gmail.com](mailto:tianyulovecars@gmail.com)
- 🐛 Issues: [GitHub Issues](https://github.com/tbyd/npm-registry-manager/issues)

提交问题时请提供：
1. 错误信息截图
2. 操作步骤描述
3. 系统环境信息
4. 相关日志文件 