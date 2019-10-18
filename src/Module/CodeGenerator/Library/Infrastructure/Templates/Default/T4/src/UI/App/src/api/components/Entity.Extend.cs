using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.UI.App.src.api.components
{
    public partial class Entity : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private ClassBuildModel _class;

        public Entity(TemplateBuildModel model)
        {
            _model = model;
        }

        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code,
                $"src/UI/{_model.Project.WebUIDicName}/src/api/components");

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

                    var filePath = Path.Combine(dir, $"{_class.Name.FirstCharToLower()}.js");
                    File.WriteAllText(filePath, content);
                }
            }
        }
    }
}
