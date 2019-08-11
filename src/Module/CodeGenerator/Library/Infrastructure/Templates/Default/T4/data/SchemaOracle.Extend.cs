using System.IO;
using Nm.Module.CodeGenerator.Domain.Property;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.data
{
    public partial class SchemaOracle : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public SchemaOracle(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, "data\\Oracle");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var content = TransformText();

            var filePath = Path.Combine(dir, "Schema.sql");
            File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// 属性转换为列
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public string Property2Column(PropertyBuildModel p)
        {
            switch (p.Type)
            {
                case PropertyType.Bool: return "[BIT]";
                case PropertyType.Byte: return "[TINYINT]";
                case PropertyType.DateTime: return "[DATETIME]";
                case PropertyType.Decimal: return $"[DECIMAL]({p.Precision},{p.Scale})";
                case PropertyType.Double: return $"[FLOAT]({p.Precision})";
                case PropertyType.Enum: return "[SMALLINT]";
                case PropertyType.Guid: return "[UNIQUEIDENTIFIER]";
                case PropertyType.Int: return "[INT]";
                case PropertyType.Long: return "[BIGINT]";
                case PropertyType.Short: return "[SMALLINT]";
                default: return $"[NVARCHAR]({(p.Length > 0 ? p.Length.ToString() : "MAX")})";
            }
        }
    }
}
