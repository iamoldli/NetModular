using System.IO;
using System.Linq;
using Tm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.UI.App.src.views.components
{
    public partial class Add : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private ClassBuildModel _class;
        private readonly string _prefix;

        public Add(TemplateBuildModel model)
        {
            _model = model;
            _prefix = _model.Project.Prefix.ToLower();
        }

        public void Save()
        {
            if (_model.Project.ClassList != null && _model.Project.ClassList.Any())
            {
                foreach (var classModel in _model.Project.ClassList)
                {
                    _class = classModel;
                    
                    var dir = Path.Combine(_model.RootPath, _model.Project.Code, $"src/UI/{_model.Project.WebUIDicName}/src/views", _class.Name, "components/add");
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    //清空
                    GenerationEnvironment.Clear();

                    var content = TransformText();

                    var filePath = Path.Combine(dir, $"index.vue");
                    File.WriteAllText(filePath, content);
                }
            }
        }
    }
}
