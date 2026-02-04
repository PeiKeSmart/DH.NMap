using Microsoft.AspNetCore.Mvc;

namespace MapMvcTest.Controllers;

/// <summary>首页控制器</summary>
public class HomeController : Controller
{
    /// <summary>首页</summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>高德地图测试页面</summary>
    /// <returns></returns>
    public IActionResult AmapTest()
    {
        return View();
    }
}
