using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.Quartz.Abstractions
{
    /// <summary>
    /// Quartz配置
    /// </summary>
    public class QuartzConfig : IConfig
    {
        /// <summary>
        /// 开启
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 启用日志
        /// </summary>
        public bool Logger { get; set; } = false;

        /// <summary>
        /// 实例名称
        /// </summary>
        [Required(ErrorMessage = "实例名称不能为空")]
        public string InstanceName { get; set; } = "QuartzServer";

        /// <summary>
        /// 表前缀
        /// </summary>
        public string TablePrefix { get; set; } = "QRTZ_";

        /// <summary>
        /// 序列化方式
        /// </summary>
        public QuartzSerializerType SerializerType { get; set; } = QuartzSerializerType.Json;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public QuartzProvider Provider { get; set; } = QuartzProvider.SqlServer;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        [Required(ErrorMessage = "数据库连接字符串不能为空")]
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource { get; set; } = "default";

        public NameValueCollection ToProps()
        {
            return new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = InstanceName,
                ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX,Quartz",
                ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.StdAdoDelegate,Quartz",
                ["quartz.jobStore.tablePrefix"] = TablePrefix,
                ["quartz.jobStore.dataSource"] = DataSource,
                ["quartz.dataSource.default.connectionString"] = ConnectionString,
                ["quartz.dataSource.default.provider"] = Provider.ToDescription(),
                ["quartz.serializer.type"] = SerializerType.ToDescription()
            };
        }
    }
}
