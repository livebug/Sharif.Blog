


using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Sharif.Blog.Services
{
    class MarkdownService : IMarkdownService
    {
        private readonly ILogger<MarkdownService> _looger;
        public MarkdownService(ILogger<MarkdownService> log)
        {
            _looger = log;

            _looger.LogInformation($"{DateTime.Now} - Markdown Service Create.");
        }
        public async Task<string> GetContentAsync()
        {
            _looger.LogInformation($"{DateTime.Now} - get markdown article content.");
            var http = new HttpClient();
            string content = await http.GetStringAsync("https://raw.githubusercontent.com/livebug/blogs/master/md/1.netcore.%E8%AE%BE%E8%AE%A1%E6%A8%A1%E5%BC%8F.20200716.md");
            return content;
        }
    }

}