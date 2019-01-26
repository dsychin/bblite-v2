using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BBLite.Models;
using Newtonsoft.Json;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace BBLite.Data
{
    public class NewsRepository : INewsRepository
    {
        private readonly Uri apiUrl = new Uri("https://borneobulletin.com.bn/wp-admin/admin-ajax.php");
        private readonly Uri baseUrl = new Uri("https://borneobulletin.com.bn");

        public async Task<List<ArticleReference>> GetAll(int page = 1)
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            // container for the post request's response
            var resultObject = new ApiResponse();
            using (var client = new HttpClient())
            {
                // Create form data content
                var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(
                    "action", "td_ajax_loop"),
                    new KeyValuePair<string, string>(
                    "loopState[moduleId]", "5"),
                    new KeyValuePair<string, string>(
                    "loopState[currentPage]", page.ToString()),
                    new KeyValuePair<string, string>(
                    "loopState[atts][category_id]", "4") // National news category
                };
                var formContent = new FormUrlEncodedContent(formData);

                // Submit post request to api
                using (var response = await client.PostAsync(apiUrl, formContent))
                {
                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();
                    resultObject = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                }
            }

            // Parse the resulting html
            var html = new HtmlDocument();
            html.LoadHtml(resultObject.HtmlData);

            var newsNodes = html.DocumentNode.SelectNodes("//*[contains(@class, 'td_module_wrap')]");

            var result = new List<ArticleReference>();
            foreach (var news in newsNodes)
            {
                // breakdown html content into object property
                var article = new ArticleReference
                {
                    ArticleUrl = new Uri(
                        news.SelectSingleNode("//h3[contains(@class, 'td-module-title')]/a")
                        .Attributes["href"].Value),
                    Title = news.SelectSingleNode("//h3[contains(@class, 'td-module-title')]/a").InnerText,
                    Excerpt = news.SelectSingleNode("//div[contains(@class, 'td-excerpt')]").InnerText.Trim(),
                    ThumbnailUrl = new Uri(
                        news.SelectSingleNode("//div[contains(@class, 'td-module-thumb')]/a/img")
                        .Attributes["src"].Value),
                    PublishedDate = DateTime.Parse(
                        news.SelectSingleNode("//time").Attributes["datetime"].Value)
                };

                result.Add(article);
            }

            return result;
        }

        public async Task<Article> GetArticle(string path)
        {
            // form the url
            Uri targetUrl = new Uri(baseUrl, path);

            // get the html
            string responseContent;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(targetUrl))
                {
                    response.EnsureSuccessStatusCode();
                    responseContent = await response.Content.ReadAsStringAsync();
                }
            }

            // parse the html
            var html = new HtmlDocument();
            html.LoadHtml(responseContent);

            var articleNode = html.DocumentNode.SelectSingleNode("//article");

            // remove escapes from html
            var htmlContent = articleNode
                    .SelectSingleNode("//div[contains(@class, 'td-post-content')]").InnerHtml;
            htmlContent = Regex.Unescape(htmlContent).Trim();

            var article = new Article
            {
                Title = articleNode.SelectSingleNode("//h1").InnerText,
                HtmlContent = htmlContent,
                OriginalUrl = targetUrl,
                PublishedDate = DateTime.Parse(
                    articleNode.SelectSingleNode("//time").Attributes["datetime"].Value)
            };

            return article;
        }
    }

    public class ApiResponse
    {
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }
        [JsonProperty("server_reply_html_data")]
        public string HtmlData { get; set; }
    }
}
