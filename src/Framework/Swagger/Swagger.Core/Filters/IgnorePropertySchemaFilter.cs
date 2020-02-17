using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using NetModular.Lib.Swagger.Abstractions.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetModular.Lib.Swagger.Core.Filters
{
    public class IgnorePropertySchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }
            
            var ignoreProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<IgnorePropertyAttribute>() != null);

            foreach (var ignorePropertyInfo in ignoreProperties)
            {
                var propertyToRemove = schema.Properties.Keys.SingleOrDefault(x => x.ToLower() == ignorePropertyInfo.Name.ToLower());

                if (propertyToRemove != null)
                {
                    schema.Properties.Remove(propertyToRemove);
                }
            }
        }
    }
}
