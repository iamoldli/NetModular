using System.IO;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Default.T4
{
    public partial class Solution : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;

        public Solution(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var content = TransformText();
            var dir = Path.Combine(_model.RootPath, _model.Project.Code);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var filePath = Path.Combine(dir, _model.Project.Code + ".sln");
            File.WriteAllText(filePath, content);
        }
    }
}
