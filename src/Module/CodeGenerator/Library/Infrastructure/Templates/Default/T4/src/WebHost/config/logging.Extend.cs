using System.IO;
using NetModular.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.WebHost.config
{
   public partial class logging : ITemplateHandler
   {
       private readonly TemplateBuildModel _model;

       public logging(TemplateBuildModel model)
       {
           _model = model;
       }

       public void Save()
       {
           var dir = Path.Combine(_model.RootPath, _model.Project.Code, "src/WebHost/config");
           if (!Directory.Exists(dir))
               Directory.CreateDirectory(dir);

           var content = TransformText();
           var filePath = Path.Combine(dir, "logging.json");
           File.WriteAllText(filePath, content);
       }
   }
}
