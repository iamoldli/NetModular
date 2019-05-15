namespace NetModular.Module.CodeGenerator.Infrastructure.Options
{
    public class CodeGeneratorOptions
    {
        public CodeGeneratorOptions()
        {
            BuildCodePath = "CodeGenerator\\BuildCode";
        }

        /// <summary>
        /// 生成代码路径(相对于临时目录的路径)
        /// </summary>
        public string BuildCodePath { get; set; }
    }
}
