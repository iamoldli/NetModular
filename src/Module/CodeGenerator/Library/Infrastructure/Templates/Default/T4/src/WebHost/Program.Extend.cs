using System.IO;
using Tm.Module.CodeGenerator.Infrastructure.Templates.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Templates.Default.T4.src.WebHost
{
   public partial class Program : ITemplateHandler
   {
       private readonly TemplateBuildModel _model;
       private readonly string _prefix;

       public Program(TemplateBuildModel model)
       {
           _model = model;
           _prefix = model.Project.Prefix;
       }

       public void Save()
       {
           var dir = Path.Combine(_model.RootPath, _model.Project.Code, "src/WebHost");
           if (!Directory.Exists(dir))
               Directory.CreateDirectory(dir);

           var content = TransformText();
           var filePath = Path.Combine(dir, "Program.cs");
           File.WriteAllText(filePath, content);
       }
   }
}
