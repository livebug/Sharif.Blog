using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sharif.Blog.Models;
using Sharif.Blog.Services;

namespace Sharif.Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMarkdownService _md;

        public HomeController(ILogger<HomeController> logger,IMarkdownService md)
        {
            _logger = logger;
            _md = md;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        // 时间轴
        public IActionResult Timeline()
        {
            return View();
        }

        // 文章
        public IActionResult Article()
        {

            // 读取文件

            // 
            string mdStr = _md.GetContent();
            ViewData["articleContent"] = mdStr;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
