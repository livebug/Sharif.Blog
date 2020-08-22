using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
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

        public HomeController(ILogger<HomeController> logger, IMarkdownService md)
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
        [HttpGet("Article/{name}")]
        public async Task<IActionResult> ArticleAsync(string name)
        {
            string content = await _md.GetMDArticleAsync(name);
            ViewData["Title"] = name;
            ViewData["articleContent"] = Regex.Escape(content);
            return View();
        }

        // 目录
        [HttpGet("Contents")]
        public async Task<IActionResult> ContentsAsync()
        {
            _logger.LogInformation("Get Contents Page. start");
            var contents = await _md.GetMDContentsAsync();
            if (contents == null)
            {
                _logger.LogError("第一次获取失败.进行第二次获取");
                contents = await _md.GetMDContentsAsync();
                if (contents == null)
                {
                    _logger.LogError("第一次获取失败.第二次获取失败.");
                }
                _logger.LogError("第一次获取失败.第二次获取成功.");
            }
            _logger.LogInformation("第一次获取成功");
            return View(contents);
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
