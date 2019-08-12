using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tm.Module.CodeGenerator.Domain.ModelProperty;
using Tm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.Library.Domain.Models
{
    public partial class QueryModel : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private readonly string _prefix;
        private ClassBuildModel _class;
        private List<ModelPropertyBuildModel> _propertyList;

        public QueryModel(TemplateBuildModel model)
        {
            _model = model;
            _prefix = model.Project.Prefix;
        }

        public void Save()
        {
            if (_model.Project.ClassList != null && _model.Project.ClassList.Any())
            {
                foreach (var classModel in _model.Project.ClassList)
                {
                    _class = classModel;

                    var dir = Path.Combine(_model.RootPath, _model.Project.Code, "src/Library/Domain", _class.Name, "Models");
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    _propertyList = classModel.ModelPropertyList
                        .Where(m => m.ModelType == ModelType.Query).ToList();

                    //清空
                    GenerationEnvironment.Clear();

                    var content = TransformText();

                    var filePath = Path.Combine(dir, $"{_class.Name}QueryModel.cs");
                    File.WriteAllText(filePath, content);
                }
            }
        }
    }
}
