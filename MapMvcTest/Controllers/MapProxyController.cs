using Microsoft.AspNetCore.Mvc;
using NewLife.Log;
using NewLife.Map;

namespace MapMvcTest.Controllers;

/// <summary>地图测试控制器</summary>
public class MapProxyController : Controller
{
    private readonly ILog _log = XTrace.Log;

    /// <summary>高德地图代理</summary>
    /// <returns></returns>
    [HttpGet("amap/proxy")]
    public async Task<IActionResult> AmapProxy()
    {
        try
        {
            // 获取原始请求的查询字符串
            var queryString = Request.QueryString.ToString();
            
            // 高德地图 API 基础地址
            var amapUrl = $"https://webapi.amap.com/maps{queryString}";
            
            _log.Info($"代理请求: {amapUrl}");
            
            using var client = new HttpClient();
            var response = await client.GetAsync(amapUrl);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/javascript";
                
                return File(content, contentType);
            }
            
            return StatusCode((Int32)response.StatusCode);
        }
        catch (Exception ex)
        {
            _log.Error($"代理请求失败: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>高德地图 API 代理（通用）</summary>
    /// <param name="path">请求路径</param>
    /// <returns></returns>
    [HttpGet("amap/api/{**path}")]
    public async Task<IActionResult> AmapApiProxy(String path)
    {
        try
        {
            // 获取原始请求的查询字符串
            var queryString = Request.QueryString.ToString();
            
            // 高德地图 API 基础地址
            var amapUrl = $"https://restapi.amap.com/{path}{queryString}";
            
            _log.Info($"API代理请求: {amapUrl}");
            
            using var client = new HttpClient();
            var response = await client.GetAsync(amapUrl);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            
            return StatusCode((Int32)response.StatusCode);
        }
        catch (Exception ex)
        {
            _log.Error($"API代理请求失败: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }
}
