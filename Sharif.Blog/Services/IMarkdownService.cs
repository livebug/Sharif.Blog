using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharif.Blog.Services
{
    public interface IMarkdownService
    {
        public Task<string> GetContentAsync();
    }
}
