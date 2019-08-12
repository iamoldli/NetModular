using System.IO;
using Tm.Module.CodeGenerator.Domain.Property;
using Tm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.data
{
    public partial class SchemaSQLite : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public SchemaSQLite(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, "data\\SQLite");
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
                case PropertyType.Bool: return "integer";
                case PropertyType.Byte: return "integer";
                case PropertyType.Decimal: return $"DECIMAL({p.Precision},{p.Scale})";
                case PropertyType.Double: return $"integer";
                case PropertyType.Enum: return "integer";
                case PropertyType.Guid: return "UNIQUEIDENTIFIER";
                case PropertyType.Int: return "integer";
                case PropertyType.Long: return "integer";
                case PropertyType.Short: return "integer";
                default: return "text";
            }
        }
    }
}
