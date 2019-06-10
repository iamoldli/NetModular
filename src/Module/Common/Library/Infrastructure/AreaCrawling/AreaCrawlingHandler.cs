using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.International.Converters.PinYinConverter;
using Newtonsoft.Json;
using Nm.Lib.Utils.Core.Extensions;

namespace Nm.Module.Common.Infrastructure.AreaCrawling
{
    /// <summary>
    /// 从 http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2018/index.html 爬取数据
    /// </summary>
    public class AreaCrawlingHandler : IAreaCrawlingHandler
    {
        private const string BaseUrl = "http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2018/";
        private HttpClient _httpClient;
        private readonly ILogger _logger;

        public AreaCrawlingHandler(ILogger<AreaCrawlingHandler> logger)
        {
            _logger = logger;
        }

        public async Task<IList<AreaCrawlingModel>> Crawling(int i)
        {
            using (_httpClient = new HttpClient())
            {
                return await CrawlingProvinces(i);
            }
        }

        /// <summary>
        /// 爬取省份
        /// </summary>
        /// <returns></returns>
        private async Task<IList<AreaCrawlingModel>> CrawlingProvinces(int i)
        {
            var list = new List<AreaCrawlingModel>();

            var url = BaseUrl + "index.html";
            var html = await GetResponse(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodeList = doc.DocumentNode.SelectNodes("//tr[@class='provincetr']//a");

            //foreach (var node in nodeList)
            {
                var node = nodeList[i];
                var href = node.Attributes["href"].Value;
                var code = href.Split('.')[0];
                var model = new AreaCrawlingModel
                {
                    Code = CompleteCode(code),
                    Name = node.InnerText,
                    FullName = node.InnerText
                };

                SetPinyin(model);

                await CrawlingCoord(model);

                CrawlingCities(model, href, code);

                list.Add(model);

            }

            return list;
        }

        /// <summary>
        /// 爬取城市
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private void CrawlingCities(AreaCrawlingModel parent, string url, string provinceCode)
        {
            var html = GetResponse(BaseUrl + url).Result;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodeList = doc.DocumentNode.SelectNodes("//tr[@class='citytr']");
            foreach (var node in nodeList)
            {
                var codeNode = node.SelectSingleNode("td[1]/a");
                var nameNode = node.SelectSingleNode("td[2]/a");
                var model = new AreaCrawlingModel
                {
                    Code = codeNode.InnerText,
                    Name = nameNode.InnerText,
                    FullName = parent.FullName + nameNode.InnerText
                };

                SetPinyin(model);
                var href = codeNode.Attributes["href"].Value;

                CrawlingCounty(model, href, provinceCode);
                CrawlingCoord(model).ConfigureAwait(false);

                parent.Children.Add(model);
            }
        }

        /// <summary>
        /// 爬取区县
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private void CrawlingCounty(AreaCrawlingModel parent, string url, string provinceCode)
        {
            try
            {
                var isTown = false;
                var html = GetResponse(BaseUrl + url).Result;
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var nodeList = doc.DocumentNode.SelectNodes("//tr[@class='countytr']");
                if (nodeList == null)
                {
                    nodeList = doc.DocumentNode.SelectNodes("//tr[@class='towntr']");
                    isTown = true;
                }

                if (nodeList == null)
                {
                    _logger.LogDebug("没有数据");
                    return;
                }
                foreach (var node in nodeList)
                {
                    var codeNode = node.SelectSingleNode("td[1]/a");
                    var nameNode = node.SelectSingleNode("td[2]/a");
                    if (codeNode == null)
                    {
                        codeNode = node.SelectSingleNode("td[1]");
                        nameNode = node.SelectSingleNode("td[2]");
                    }

                    if (codeNode == null || nameNode == null || nameNode.InnerText == "市辖区")
                    {
                        continue;
                    }

                    var model = new AreaCrawlingModel
                    {
                        Code = codeNode.InnerText,
                        Name = nameNode.InnerText,
                        FullName = parent.FullName + nameNode.InnerText
                    };

                    SetPinyin(model);
                    CrawlingCoord(model).ConfigureAwait(false);

                    if (!isTown)
                    {
                        var hrefAttribute = codeNode.Attributes["href"];
                        if (hrefAttribute != null)
                        {
                            CrawlingTown(model, hrefAttribute.Value, provinceCode);
                        }
                    }

                    parent.Children.Add(model);
                }
            }
            catch (Exception ex)
            {
                Thread.Sleep(10000);
                parent.Children = new List<AreaCrawlingModel>();
                _logger.LogError($"爬取{parent.Name}下的区县失败");
                _logger.LogError(ex.Message);
                CrawlingCounty(parent, url, provinceCode);
            }
        }

        /// <summary>
        /// 爬取镇
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private void CrawlingTown(AreaCrawlingModel parent, string url, string provinceCode)
        {
            try
            {
                var html = GetResponse(BaseUrl + provinceCode + "/" + url).Result;
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var nodeList = doc.DocumentNode.SelectNodes("//tr[@class='towntr']");
                foreach (var node in nodeList)
                {
                    var codeNode = node.SelectSingleNode("td[1]/a");
                    var nameNode = node.SelectSingleNode("td[2]/a");

                    var model = new AreaCrawlingModel
                    {
                        Code = codeNode.InnerText,
                        Name = nameNode.InnerText,
                        FullName = parent.FullName + nameNode.InnerText
                    };

                    SetPinyin(model);

                    CrawlingCoord(model).ConfigureAwait(false);

                    parent.Children.Add(model);
                    _logger.LogDebug(model.FullName);
                }
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                _logger.LogError($"爬取{parent.FullName}城镇失败");
                _logger.LogDebug(ex.Message);
                parent.Children = new List<AreaCrawlingModel>();

                Thread.Sleep(3000);
                CrawlingTown(parent, url, provinceCode);
            }
        }

        /// <summary>
        /// 爬取坐标
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task CrawlingCoord(AreaCrawlingModel entity)
        {
            var url = "https://restapi.amap.com/v3/place/text?key=8325164e247e15eea68b59e89200988b&keywords=" + entity.Name;
            var json = await _httpClient.GetStringAsync(url);

            var model = JsonConvert.DeserializeAnonymousType(json, new { pois = new[] { new { location = "" } } });
            if (model.pois.Any())
            {
                var location = model.pois.First().location;
                if (location.NotNull())
                {
                    var arr = location.Split(',');
                    entity.Longitude = arr[0];
                    entity.Latitude = arr[1];
                }
            }
        }

        /// <summary>
        /// 设置拼音
        /// </summary>
        /// <param name="entity"></param>
        private void SetPinyin(AreaCrawlingModel entity)
        {
            foreach (var c in entity.Name)
            {
                if (c == '辖')
                {
                    entity.Pinyin += "Xia";
                    continue;
                }

                if (ChineseChar.IsValidChar(c))
                {
                    var cc = new ChineseChar(c);
                    var py = cc.Pinyins[0];
                    py = py.Substring(0, py.Length - 1);

                    for (var t = 0; t < py.Length; t++)
                    {
                        if (t > 0)
                        {
                            entity.Pinyin += py[t].ToString().ToLower();
                        }
                        else
                        {
                            entity.Pinyin += py[t];
                        }
                    }

                    entity.Pinyin += " ";
                }
            }

            entity.Pinyin = entity.Pinyin.Trim();
        }

        /// <summary>
        /// 补全编码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string CompleteCode(string code)
        {
            var n = 12 - code.Length;
            for (var i = 0; i < n; i++)
            {
                code += '0';
            }

            return code;
        }

        /// <summary>
        /// 获取网页数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<string> GetResponse(string url)
        {
            var res = await _httpClient.PostAsync(url, null);

            var stream = await res.Content.ReadAsStreamAsync();
            var sr = new StreamReader(stream, Encoding.GetEncoding("GB2312"));
            var html = await sr.ReadToEndAsync();
            sr.Close();
            stream.Close();
            return html;
        }
    }
}
