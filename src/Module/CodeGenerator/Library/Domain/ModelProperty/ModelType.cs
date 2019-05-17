using System.ComponentModel;

namespace Nm.Module.CodeGenerator.Domain.ModelProperty
{
    /// <summary>
    /// 模型类型
    /// </summary>
    public enum ModelType
    {
        [Description("未知")]
        UnKnown,
        [Description("查询")]
        Query,
        [Description("添加")]
        Add,
        [Description("编辑")]
        Edit
    }
}
