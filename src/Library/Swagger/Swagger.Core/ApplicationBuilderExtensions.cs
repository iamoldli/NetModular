using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tm.Lib.Module.Abstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Tm.Lib.Swagger.Core
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 自定义Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            var modules = app.ApplicationServices.GetService<IModuleCollection>();
            app.UseSwagger(c =>
            {
                #region ==配置显示控制器的注释说明==

                //c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                //{
                //    if (modules == null) return;

                //    swaggerDoc.Tags = new List<Tag>();

                //    var moduleId = httpReq.Path.Value.Split('/')[2];
                //    var module = modules.SingleOrDefault(m => m.Id.Equals(moduleId, StringComparison.OrdinalIgnoreCase));
                //    if (module != null)
                //    {
                //        var tags = GetTags(module.DocPath);
                //        foreach (var tag in tags)
                //        {
                //            swaggerDoc.Tags.Add(tag);
                //        }
                //    }
                //});

                #endregion
            });

            app.UseSwaggerUI(c =>
            {
                if (modules == null) return;

                foreach (var moduleInfo in modules)
                {
                    c.SwaggerEndpoint($"/swagger/{moduleInfo.Id}/swagger.json", moduleInfo.Name);
                }
            });

            return app;
        }

        /// <summary>
        /// 从注释文档中解析控制器的注释说明并返回Tag列表
        /// </summary>
        /// <param name="docPath"></param>
        /// <returns></returns>
        private static List<Tag> GetTags(string docPath)
        {
            var tags = new List<Tag>();
            if (!File.Exists(docPath))
                return tags;

            var doc = new XmlDocument();
            doc.Load(docPath);

            var memberNodes = doc.SelectNodes("//member");
            if (memberNodes == null || memberNodes.Count < 1)
                return tags;

            foreach (XmlNode memberNode in memberNodes)
            {
                var nameAttr = memberNode.Attributes?["name"];
                if (nameAttr == null)
                    continue;

                var name = memberNode.Attributes["name"].Value;
                if (string.IsNullOrWhiteSpace(name) || !name.StartsWith("T:") || !name.EndsWith("Controller"))
                    continue;

                var controllerName = name.Split('.').Last().Replace("Controller", "");
                if (controllerName.Equals("Base", StringComparison.OrdinalIgnoreCase) || tags.Any(t => t.Name.Equals(controllerName, StringComparison.OrdinalIgnoreCase)))
                    continue;

                var summaryNode = memberNode.SelectSingleNode("summary");
                if (summaryNode == null)
                    continue;

                var controllerDesc = summaryNode.InnerText.Replace("\r\n", "").Trim();
                tags.Add(new Tag
                {
                    Name = controllerName,
                    Description = controllerDesc
                });
            }

            return tags;
        }
    }
}
