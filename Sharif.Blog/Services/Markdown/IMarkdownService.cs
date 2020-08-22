using Sharif.Blog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharif.Blog.Services
{
    public interface IMarkdownService
    {
        public Task<string> GetMDArticleAsync();
        public Task<string> GetMDArticleAsync(string name);
        public Task<IEnumerable<ContentsViewModel>> GetMDContentsAsync();
    }
}
