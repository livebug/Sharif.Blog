using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Logging;
using Sharif.Blog.Models;

namespace Sharif.Blog.Services
{

    public class GithubService
    {
        private readonly ILogger<GithubService> _logger;
        public HttpClient Client { get; }

        private IEnumerable<GithubReposContents> _githubReposContents;
        private GitTree _githubTrees;

        private DateTime InitDateTime { get; set; }

        public GithubService(
            ILogger<GithubService> logger,
            HttpClient client
        )
        {
            _logger = logger;
            _logger.Log(LogLevel.Information, "Github Service init.");
            // 初始化时间基准点
            InitDateTime = DateTime.Now;
            // 初始化http客户端
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            client.DefaultRequestHeaders.Add("User-Agent", "livebug"); // https://developer.github.com/v3/#user-agent-required
            client.DefaultRequestHeaders.Add("Authorization", " token 1b89a4e19298d9a6d211e8a51aa956e7117137fe");

            Client = client;
        }

        internal async Task<string> GetStringAsync(string url)
        {
            return await Client.GetStringAsync(url);
        }


        // 获取某路径下内容

        /// <summary>
        ///  获取目录结构
        /// </summary>
        internal async Task<IEnumerable<GithubReposContents>> GetGithubRepoBlobsAsync()
        {
            if (_githubReposContents == null || (DateTime.Now - InitDateTime).TotalSeconds > 3000)
            {
                try
                {
                    InitDateTime = DateTime.Now;
                    _logger.LogInformation("从Github获取仓库目录开始。");

                    var responseStream = await Client.GetStreamAsync("https://api.github.com/repos/livebug/blogs/contents/md");
                    _githubReposContents = await JsonSerializer.DeserializeAsync<IEnumerable<GithubReposContents>>(responseStream);

                    _logger.LogInformation("从Github获取仓库目录成功。");
                }
                catch (System.Exception e)
                {
                    _logger.LogError("从Github获取仓库目录失败！\n" + e);
                    _githubReposContents = null;
                    throw;
                }
            }
            return _githubReposContents;
        }


        public async Task<GitTree> GetGithubRepoTreeAsync()
        {
            if (_githubTrees == null || (DateTime.Now - InitDateTime).TotalSeconds > 3000)
            {
                try
                {
                    InitDateTime = DateTime.Now;
                    _logger.LogInformation($"{DateTime.Now} - 获取目录结构。");
                    var responseStream = await Client.GetStreamAsync("https://api.github.com/repos/livebug/blogs/git/trees/24a7e0b8893e2d61f98d66ab0a235772a78fcc77");
                    _githubTrees = await JsonSerializer.DeserializeAsync<GitTree>(responseStream);

                    _logger.LogInformation($"{DateTime.Now} - 从Github获取目录成功。");
                }
                catch (System.Exception e)
                {
                    _logger.LogError($"{DateTime.Now} - 从Github获取目录失败！\n" + e);
                    _githubTrees = null;
                    throw;
                }
            }

            return _githubTrees;
        }
    }

}
