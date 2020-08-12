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
            // ��ʼ��ʱ���׼��
            InitDateTime = DateTime.Now;
            // ��ʼ��http�ͻ���
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            client.DefaultRequestHeaders.Add("User-Agent", "livebug"); // https://developer.github.com/v3/#user-agent-required
            client.DefaultRequestHeaders.Add("Authorization", " token 1b89a4e19298d9a6d211e8a51aa956e7117137fe");

            Client = client;
        }

        internal async Task<string> GetStringAsync(string url)
        {
            return await Client.GetStringAsync(url);
        }


        // ��ȡĳ·��������

        /// <summary>
        ///  ��ȡĿ¼�ṹ
        /// </summary>
        internal async Task<IEnumerable<GithubReposContents>> GetGithubRepoBlobsAsync()
        {
            if (_githubReposContents == null || (DateTime.Now - InitDateTime).TotalSeconds > 3000)
            {
                try
                {
                    InitDateTime = DateTime.Now;
                    _logger.LogInformation("��Github��ȡ�ֿ�Ŀ¼��ʼ��");

                    var responseStream = await Client.GetStreamAsync("https://api.github.com/repos/livebug/blogs/contents/md");
                    _githubReposContents = await JsonSerializer.DeserializeAsync<IEnumerable<GithubReposContents>>(responseStream);

                    _logger.LogInformation("��Github��ȡ�ֿ�Ŀ¼�ɹ���");
                }
                catch (System.Exception e)
                {
                    _logger.LogError("��Github��ȡ�ֿ�Ŀ¼ʧ�ܣ�\n" + e);
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
                    _logger.LogInformation($"{DateTime.Now} - ��ȡĿ¼�ṹ��");
                    var responseStream = await Client.GetStreamAsync("https://api.github.com/repos/livebug/blogs/git/trees/24a7e0b8893e2d61f98d66ab0a235772a78fcc77");
                    _githubTrees = await JsonSerializer.DeserializeAsync<GitTree>(responseStream);

                    _logger.LogInformation($"{DateTime.Now} - ��Github��ȡĿ¼�ɹ���");
                }
                catch (System.Exception e)
                {
                    _logger.LogError($"{DateTime.Now} - ��Github��ȡĿ¼ʧ�ܣ�\n" + e);
                    _githubTrees = null;
                    throw;
                }
            }

            return _githubTrees;
        }
    }

}
