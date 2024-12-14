# NPM Registry Manager 安装指南

## 目录

1. [系统要求](#系统要求)
2. [安装步骤](#安装步骤)
3. [配置说明](#配置说明)
4. [故障排除](#故障排除)
5. [升级指南](#升级指南)

## 系统要求

### 硬件要求
- CPU: 双核处理器及以上
- 内存: 4GB RAM 及以上
- 存储空间: 500MB 可用空间
- 显示器: 1280x720 分辨率及以上

### 软件要求
- 操作系统: Windows 10 及以上版本
- .NET Framework: 4.7.2 及以上
- Visual C++ Redistributable 2015-2022

### 网络要求
- 稳定的网络连接
- 支持 HTTPS 协议
- 防火墙允许应用程序网络访问

## 安装步骤

### 1. 预安装检查
1. 验证系统满足最低要求
2. 确保拥有管理员权限
3. 关闭可能影响安装的杀毒软件

### 2. 下载安装包
1. 从官方渠道下载最新版本安装包
2. 验证安装包的数字签名和校验和

### 3. 安装过程
1. 以管理员身份运行安装程序
2. 选择安装路径（建议使用默认路径）
3. 选择需要的组件
4. 等待安装完成

### 4. 首次启动配置
1. 启动应用程序
2. 完成初始化设置向导
3. 配置 Verdaccio 连接信息

## 配置说明

### Verdaccio 连接配置
```json
{
  "registry": {
    "url": "https://your-verdaccio-server.com",
    "auth": {
      "type": "basic",
      "scope": "@your-scope"
    }
  }
}
```

### 代理设置（可选）
```json
{
  "proxy": {
    "http": "http://proxy.company.com:8080",
    "https": "http://proxy.company.com:8080",
    "no_proxy": "localhost,127.0.0.1"
  }
}
```

### 性能优化建议
- 缓存大小: 建议设置为可用空间的 20%
- 并发连接数: 建议设置为 5-10
- 上传分块大小: 建议设置为 5MB

## 故障排除

### 常见安装错误
1. ERROR 1001: 权限不足
   - 解决方案: 以管理员身份运行安装程序

2. ERROR 1002: 依赖缺失
   - 解决方案: 安装所需的 Visual C++ Redistributable

3. ERROR 1003: 端口被占用
   - 解决方案: 更改配置中的默认端口

### 安装日志位置
- 安装日志: `%PROGRAMDATA%\\NPM Registry Manager\\Logs\\install.log`
- 运行日志: `%APPDATA%\\NPM Registry Manager\\Logs\\app.log`

## 升级指南

### 自动升级
1. 应用程序会自动检测新版本
2. 按照提示完成升级过程

### 手动升级
1. 下载新版本安装包
2. 卸载旧版本（可选）
3. 运行新版本安装程序
4. 验证配置是否正确迁移

### 数据迁移
- 配置文件会自动备份
- 用户数据会自动迁移
- 缓存数据需手动清理

## 安全建议

### 安装后检查
1. 验证安装目录权限
2. 检查服务是否正常运行
3. 验证网络连接是否正常

### 安全配置
1. 更改默认端口（如需要）
2. 配置访问控制
3. 启用日志审计

## 支持资源

### 技术支持
- 邮箱: [tianyulovecars@gmail.com](mailto:tianyulovecars@gmail.com)
- 问题反馈: [GitHub Issues](https://github.com/tbyd/npm-registry-manager/issues)

### 文档资源
- [用户手册](./README.md)
- [更新日志](./CHANGELOG.md)
- [常见问题](./FAQ.md)

## 免责声明

本软件为测试版本，可能存在未知问题。在生产环境使用前，请充分测试并评估风险。使用本软件所造成的任何直接或间接损失，开发团队不承担责任。 