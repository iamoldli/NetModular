using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetModular.Lib.Swagger.Core.Filters
{
    /// <summary>
    /// 控制器和方法的描述信息处理
    /// </summary>
    public class DescriptionDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            SetControllerDescription(swaggerDoc, context);
            SetActionDescription(swaggerDoc, context);
            SetModelDescription(swaggerDoc, context);
        }

        /// <summary>
        /// 设置控制器的描述信息
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        private void SetControllerDescription(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc.Tags == null)
                swaggerDoc.Tags = new List<OpenApiTag>();

            foreach (var apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) && methodInfo.DeclaringType != null)
                {
                    var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(methodInfo.DeclaringType, typeof(DescriptionAttribute));
                    if (descAttr != null && descAttr.Description.NotNull())
                    {
                        var controllerName = methodInfo.DeclaringType.Name;
                        controllerName = controllerName.Remove(controllerName.Length - 10);
                        if (swaggerDoc.Tags.All(t => t.Name != controllerName))
                        {
                            swaggerDoc.Tags.Add(new OpenApiTag
                            {
                                Name = controllerName,
                                Description = descAttr.Description
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置方法的说明
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        private void SetActionDescription(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var path in swaggerDoc.Paths)
            {
                if (TryGetActionDescription(path.Key, context, out string description))
                {
                    if (path.Value != null && path.Value.Operations != null && path.Value.Operations.Any())
                    {
                        var operation = path.Value.Operations.FirstOrDefault();
                        operation.Value.Description = description;
                        operation.Value.Summary = description;
                    }
                }
            }
        }

        /// <summary>
        /// 设置模型属性描述信息
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        private void SetModelDescription(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var pro = typeof(SchemaRepository).GetField("_reservedIds", BindingFlags.NonPublic | BindingFlags.Instance);
            if (pro == null)
                return;

            var schemaTypes = (Dictionary<Type, string>)pro.GetValue(context.SchemaRepository);

            foreach (var schema in context.SchemaRepository.Schemas)
            {
                var type = schemaTypes.FirstOrDefault(m => m.Value.EqualsIgnoreCase(schema.Key)).Key;
                if (type == null || !type.IsClass)
                    continue;

                var properties = type.GetProperties();
                foreach (var propertyInfo in properties)
                {
                    var propertySchema = schema.Value.Properties.FirstOrDefault(m => m.Key.EqualsIgnoreCase(propertyInfo.Name)).Value;
                    if (propertySchema != null)
                    {
                        var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DescriptionAttribute));
                        if (descAttr != null && descAttr.Description.NotNull())
                        {
                            propertySchema.Title = descAttr.Description;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取说明
        /// </summary>
        private bool TryGetActionDescription(string path, DocumentFilterContext context, out string description)
        {
            foreach (var apiDescription in context.ApiDescriptions)
            {
                var apiPath = "/" + apiDescription.RelativePath.ToLower();
                if (apiPath.Equals(path) && apiDescription.TryGetMethodInfo(out MethodInfo methodInfo))
                {
                    var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(DescriptionAttribute));
                    if (descAttr != null && descAttr.Description.NotNull())
                    {
                        description = descAttr.Description;
                        return true;
                    }
                }
            }

            description = "";
            return false;
        }
    }
}
