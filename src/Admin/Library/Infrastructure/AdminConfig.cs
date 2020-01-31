using System;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Config.Abstraction;

namespace NetModular.Module.Admin.Infrastructure
{
    public class AdminConfig : IConfig
    {
        [ConfigDefinition("姓名")]
        public string Name { get; set; }

        [ConfigDefinition("年龄")]
        public int Age { get; set; }

        [ConfigDefinition("分数")]
        public double Score { get; set; }

        [ConfigDefinition("价格")]
        public decimal Price { get; set; }

        [ConfigDefinition("生日")]
        public DateTime Birthday { get; set; }

        [ConfigDefinition("平台")]
        public Platform Platform { get; set; }
    }
}
