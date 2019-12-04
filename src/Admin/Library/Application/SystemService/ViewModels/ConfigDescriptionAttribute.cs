using System;

namespace NetModular.Module.Admin.Application.SystemService.ViewModels
{
    /// <summary>
    /// 配置说明特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigDescriptionAttribute : Attribute
    {
        public string Name { get; set; }

        public string Remarks { get; set; }

        public ConfigDescriptionAttribute(string name, string remarks)
        {
            Name = name;
            Remarks = remarks;
        }
    }
}
