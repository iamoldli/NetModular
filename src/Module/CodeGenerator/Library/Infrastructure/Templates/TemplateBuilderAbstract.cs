using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NetModular.Lib.Utils.Core;
using NetModular.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Templates
{
    public class TemplateBuilderAbstract : ITemplateBuilder
    {
        public string Name { get; }

        protected TemplateBuilderAbstract(string name)
        {
            Name = name;
        }

        public virtual void Build(TemplateBuildModel model)
        {
            Check.NotNull(model, nameof(TemplateBuildModel), "模板生成模型不能为空");

            var dir = Path.Combine(model.RootPath, model.Project.Code);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var handlerTypeList = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
                  typeof(ITemplateHandler) != t && typeof(ITemplateHandler).IsAssignableFrom(t)
                  && t.FullName.Contains($"Infrastructure.Templates.{Name}")).ToList();

            foreach (var type in handlerTypeList)
            {
                var instance = (ITemplateHandler)Activator.CreateInstance(type, model);
                instance.Save();
            }
        }
    }
}
