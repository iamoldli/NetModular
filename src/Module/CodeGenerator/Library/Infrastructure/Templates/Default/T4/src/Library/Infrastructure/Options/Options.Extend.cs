using System.IO;
using NetModular.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.Library.Infrastructure.Options
{
    public partial class Options : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private readonly string _prefix;

        public Options(TemplateBuildModel model)
        {
            _model = model;
            _prefix = model.Project.Prefix;
        }


        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, "src/Library/Infrastructure/Options");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var content = TransformText();
            var filePath = Path.Combine(dir, $"{_model.Project.Code}Options.cs");
            File.WriteAllText(filePath, content);
        }
    }
}
