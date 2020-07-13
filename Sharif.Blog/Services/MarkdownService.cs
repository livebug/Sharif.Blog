


using System;
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
        public string GetContent()
        {
            _looger.LogInformation($"{DateTime.Now} - get markdown article content.");
            return @"# [CSS滚动条实现步骤及美化小技巧](https://www.w3cschool.cn/css/css-scrollbar.html)\n\n## 了解 `overflow`\n\n处理一下滚动条，将滚动条修改为隐藏但可以正常滚动。\n但是只能应用在chrome浏览器，其他浏览器设置为滚动条一直显示。\n\n## Q: 做时间轴页面遇到了一个滚动条的问题：\n\n这里有个问题\n当页面过长是 container 会调整 margin\n\n### 实际问题是因为\n\n当页面过长时，这个时候进度条会出来，占据页面一点位置\n\n### 我自己觉得解决方法有这么几个\n\n1. 选择 container-fluid 不使用中心布局了\n2. 固定 margin-left 大小\n这两个都是改变原有的响应式布局\n\n### 网络搜的解决办法是\n\n1. 让垂直滚动条一直显示\n\n    ```css\n    body {\n        overflow:scroll;\n        overflow-x:hidden;\n    }\n    ```\n\n2. 直接隐藏滚动条，但是依然可以滚动，根据不同的浏览器进行设置（比较好）\n\n    ```css\n    body {\n        min-height: 75rem;\n        -ms-overflow-style: none; /* IE 10+ */\n        scrollbar-width: none; /* Firefox */\n    }\n\n    /* chrome 浏览器中滚动条消失 */\n        body::-webkit-scrollbar {\n            display: none;\n        }\n    ```\n\n";
        }
    }

}