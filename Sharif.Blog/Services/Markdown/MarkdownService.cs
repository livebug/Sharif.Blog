


using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sharif.Blog.Models;
using Sharif.Blog.Models.ViewModels;

namespace Sharif.Blog.Services
{
    class MarkdownService : IMarkdownService
    {
        private readonly GithubService _github;
        private readonly ILogger<MarkdownService> _looger;


        public MarkdownService(ILogger<MarkdownService> log,
            GithubService gitHubService
            )
        {
            _github = gitHubService;
            _looger = log;

            _looger.LogInformation("Markdown Service Create.");
        }

        /// <summary>
        /// 获取博客文章内容
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetMDArticleAsync()
        {
            _looger.LogInformation("Get markdown article content.");
            string content = await _github.GetStringAsync("https://raw.githubusercontent.com/livebug/blogs/master/md/1.netcore.%E8%AE%BE%E8%AE%A1%E6%A8%A1%E5%BC%8F.20200716.md");
            return content;
        }

        public async Task<string> GetMDArticleAsync(string name)
        {
            _looger.LogInformation("Get markdown article content.");
            string content = await _github.GetStringAsync("https://raw.githubusercontent.com/livebug/blogs/master/md/" + name);
            return content;
        }

        /// <summary>
        /// 获取博客文章目录
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ContentsViewModel>> GetMDContentsAsync()
        {
            _looger.LogInformation("Get Blog Contents.");
            var contents = await _github.GetGithubReposContentsAsync();
            if (contents != null && contents.Count() > 0)
            {
                _looger.LogInformation("Get Blog Contents. successfully");
                return (IEnumerable<ContentsViewModel>)contents.Where(e => e.ContentsType != ContentsType.Dir).Select(e => new ContentsViewModel
                {
                    ID = e.Sha,
                    Name = e.Name
                });
            }
            else
            {
                if (contents == null || contents?.Count() == 0)
                {
                    _looger.LogError("api未获取到内容。");
                }
                _looger.LogError("Get Blog Contents. failed");
            }
            return null;
        }
    }

}