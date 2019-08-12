﻿using System.IO;
using System.Linq;
using Tm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.Library.Infrastructure.Repositories
{
    public partial class MySqlRepository : ITemplateHandler
    {
        private readonly TemplateBuildModel _model;
        private readonly string _prefix;
        private ClassBuildModel _class;

        public MySqlRepository(TemplateBuildModel model)
        {
            _model = model;
            _prefix = model.Project.Prefix;
        }


        public void Save()
        {
            var dir = Path.Combine(_model.RootPath, _model.Project.Code, "src/Library/Infrastructure/Repositories/MySql");
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

                    var filePath = Path.Combine(dir, $"{_class.Name}Repository.cs");
                    File.WriteAllText(filePath, content);
                }
            }
        }
    }
}
