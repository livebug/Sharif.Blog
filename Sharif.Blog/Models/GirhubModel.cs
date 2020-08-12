using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sharif.Blog.Models
{
    public class GirhubModel
    {
    }

    public class GitTree
    {
        [JsonPropertyName("sha")]
        public string Sha { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("truncated")]
        public bool Truncated { get; set; }
        [JsonPropertyName("tree")]
        public IEnumerable<GitTreeNode> Tree { get; set; }
    }

    /// <summary>
    ///   "blobs_url": "https://api.github.com/repos/livebug/blogs/git/blobs{/sha}",
    /// </summary>
    public class GitTreeNode
    {
        [JsonPropertyName("sha")]
        public string Sha { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("mode")]
        public string Mode { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("size")]
        public int Size { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    /// <summary>
    ///  "contents_url": "https://api.github.com/repos/livebug/blogs/contents/{+path}",
    /// </summary>
    public class GitBlob
    {
        [JsonPropertyName("sha")]
        public string Sha { get; set; }
        [JsonPropertyName("node_id")]
        public string NodeId { get; set; }
        public string Size { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("encoding")]
        public string Encoding { get; set; }
    }
    public enum EncodingType
    {
        Utf_8,
        Base64
    }


    /// <summary>
    /// https://api.github.com/repos/livebug/blogs/contents/md{/path}
    ///     {/file_path}
    ///     {/dir_path}
    /// </summary>
    public class GithubReposContents
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("sha")]
        public string Sha { get; set; }
        [JsonPropertyName("size")]
        public int Size { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; }
        [JsonPropertyName("git_url")]
        public string GitUrl { get; set; }
        /// <summary>
        /// type 为file时不为空
        /// 返回文章内容的api
        /// https://api.github.com/repos/livebug/blogs/contents/md {/file_path}
        /// </summary>
        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; }
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ContentsType ContentsType { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("encoding")]
        public EncodingType Encoding { get; set; }
    }

    public enum ContentsType
    {
        File,
        Dir
    }

}
