# DH.NMap MVC 测试项目

本项目用于测试通过代理方式加载和渲染高德地图。参考 Pek.Zero 的 MVC 项目结构创建。

## 项目特点

- 基于 .NET 10.0
- 使用 DH.NCube.Core 和 DH.NCode
- MVC 架构
- 支持高德地图 JS API 和 REST API 代理

## 项目结构

```
MapMvcTest/
├── Controllers/
│   ├── HomeController.cs          # 主控制器
│   └── MapProxyController.cs      # 地图代理控制器
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml           # 首页
│   │   └── AmapTest.cshtml        # 高德地图测试页面
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
├── Properties/
│   └── launchSettings.json
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
├── MapMvcTest.csproj
└── Readme.md
```

## 功能说明

### 1. 地图代理功能

#### MapProxyController 提供两个代理端点：

- **JS API 代理**：`/amap/proxy?v=2.0&key=YOUR_KEY`
  - 代理高德地图 JavaScript API 加载请求
  - 将请求转发到 `https://webapi.amap.com/maps`

- **REST API 代理**：`/amap/api/{path}?params`
  - 代理高德地图 REST API 请求（如地理编码、逆地理编码等）
  - 将请求转发到 `https://restapi.amap.com/{path}`

### 2. 测试页面

#### Index.cshtml（首页）
- 项目说明和导航
- 使用指南
- 测试入口链接

#### AmapTest.cshtml（地图测试页面）
- 通过代理方式加载高德地图 JS API
- 初始化地图并显示北京天安门位置
- 提供两个测试按钮：
  - **测试代理**：验证 JS API 代理是否正常工作
  - **测试地理编码**：验证 REST API 代理是否正常工作
- 实时显示测试结果和地图交互状态

## 使用步骤

### 1. 配置 API Key

在 `AmapTest.cshtml` 中，将所有 `YOUR_AMAP_KEY` 替换为你的高德地图 API Key：

```html
<script src="/amap/proxy?v=2.0&key=YOUR_AMAP_KEY"></script>
```

```javascript
fetch('/amap/api/v3/geocode/geo?address=北京市朝阳区阜通东大街6号&key=YOUR_AMAP_KEY')
```

### 2. 运行项目

```bash
cd MapMvcTest
dotnet run
```

或在 Visual Studio 中直接运行项目。

### 3. 访问测试页面

- 首页：`http://localhost:5100`
- 地图测试：`http://localhost:5100/Home/AmapTest`

### 4. 验证功能

1. 访问地图测试页面
2. 检查地图是否正常加载并显示
3. 点击"测试代理"按钮，验证 JS API 代理
4. 点击"测试地理编码"按钮，验证 REST API 代理
5. 在地图上点击，查看坐标信息显示

## 代理原理

### JS API 代理流程
```
浏览器 → /amap/proxy?v=2.0&key=xxx
       → MapProxyController.AmapProxy()
       → https://webapi.amap.com/maps?v=2.0&key=xxx
       → 返回 JavaScript 文件
```

### REST API 代理流程
```
浏览器 → /amap/api/v3/geocode/geo?address=xxx&key=xxx
       → MapProxyController.AmapApiProxy("v3/geocode/geo")
       → https://restapi.amap.com/v3/geocode/geo?address=xxx&key=xxx
       → 返回 JSON 数据
```

## 优势

1. **安全性**：API Key 不直接暴露在前端代码中
2. **可控性**：可以在服务端对请求进行日志记录、验证、限流等处理
3. **灵活性**：可以轻松切换不同的地图服务或添加缓存策略
4. **统一管理**：所有地图相关的请求都经过统一的代理入口

## 注意事项

1. 需要有效的高德地图 API Key
2. 确保网络连接正常，能够访问高德地图服务
3. 生产环境中建议添加：
   - 请求频率限制
   - API Key 的安全存储（如使用配置文件或环境变量）
   - 缓存机制
   - 错误处理和日志记录

## 扩展建议

1. **添加其他地图服务**：可以仿照 MapProxyController 添加百度地图、腾讯地图等的代理
2. **添加缓存**：对于静态资源（如 JS API）可以添加缓存机制
3. **API Key 管理**：将 API Key 存储在配置文件中，而不是硬编码在代码中
4. **请求日志**：记录所有代理请求，便于调试和监控
5. **CORS 配置**：如果需要跨域访问，添加适当的 CORS 配置

## 参考资料

- [高德地图 JavaScript API 文档](https://lbs.amap.com/api/javascript-api/summary)
- [高德地图 Web 服务 API 文档](https://lbs.amap.com/api/webservice/summary)
- [ASP.NET Core MVC 文档](https://docs.microsoft.com/zh-cn/aspnet/core/mvc/)
