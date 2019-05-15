using System.IO;
using System.Linq;
using NetModular.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.UI.App.src.api
{
    public partial class EntityApi : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private readonly string _prefix;
        private ClassBuildModel _class;

        public EntityApi(TemplateBuildModel model)
        {
            _model = model;
            _prefix = model.Project.Prefix.ToLower();
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code,
                $"src/UI/{_model.Project.WebUIDicName}/src/api");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (_model.Project.ClassList != null && _model.Project.ClassList.Any())
            {
                foreach (var classModel in _model.Project.ClassList)
                {
                    _class = classModel;

                    //清空
                    GenerationEnvironment.Clear();

                    var content = TransformText();

                    var filePath = Path.Combine(dir, $"{_class.Name}.js");
                    File.WriteAllText(filePath, content);
                }
            }
        }
    }
}