using System.IO;
using NetModular.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Templates.Default.T4.build
{
    public partial class WebBuildTargets : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public WebBuildTargets(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, "build");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var content = TransformText();
            
            var filePath = Path.Combine(dir, "web.build.targets");
            File.WriteAllText(filePath, content);
        }
    }
}
