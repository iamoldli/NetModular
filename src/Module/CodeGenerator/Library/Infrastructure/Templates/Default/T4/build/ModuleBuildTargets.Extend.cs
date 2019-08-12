using System.IO;
using Tm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.build
{
    public partial class ModuleBuildTargets : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public ModuleBuildTargets(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, "build");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var content = TransformText();
            
            var filePath = Path.Combine(dir, "module.build.targets");
            File.WriteAllText(filePath, content);
        }
    }
}
