using System.IO;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.UI.App.src.routes
{
    public partial class Routes : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private readonly string _prefix;

        public Routes(TemplateBuildModel model)
        {
            _model = model;
            _prefix = _model.Project.Prefix.ToLower();
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, $"src/UI/{_model.Project.WebUIDicName}/src/routes");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var content = TransformText();
            var filePath = Path.Combine(dir, "index.js");
            File.WriteAllText(filePath, content);
        }
    }
}
