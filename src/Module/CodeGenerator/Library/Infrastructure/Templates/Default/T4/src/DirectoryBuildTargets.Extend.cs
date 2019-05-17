using System.IO;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src
{
    public partial class DirectoryBuildTargets : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public DirectoryBuildTargets(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, "src");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var content = TransformText();
            var filePath = Path.Combine(dir, "Directory.Build.targets");
            File.WriteAllText(filePath, content);
        }
    }
}
