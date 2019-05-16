using System.IO;
using NetModular.Module.CodeGenerator.Domain.Property;
using NetModular.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Templates.Default.T4.data
{
    public partial class SchemaMySql : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public SchemaMySql(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, "data\\MySql");
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
                case PropertyType.Bool: return "TINYINT(1)";
                case PropertyType.Byte: return "TINYINT(3)";
                case PropertyType.DateTime: return "DATETIME";
                case PropertyType.Decimal: return $"DECIMAL({p.Precision},{p.Scale})";
                case PropertyType.Double: return $"DOUBLE";
                case PropertyType.Enum: return "TINYINT(3)";
                case PropertyType.Guid: return "CHAR(36)";
                case PropertyType.Int: return "INT";
                case PropertyType.Long: return "BIGINT";
                case PropertyType.Short: return "SMALLINT";
                default: return p.Length > 0 ? $"VARCHAR({p.Length})" : "TEXT";
            }
        }
    }
}
