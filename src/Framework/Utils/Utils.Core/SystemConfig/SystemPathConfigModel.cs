namespace NetModular.Lib.Utils.Core.SystemConfig
{
    /// <summary>
    /// 系统路径配置
    /// </summary>
    public class SystemPathConfigModel
    {
        /// <summary>
        /// 文件上传存储根路径
        /// </summary>
        [ConfigDescription("sys_path_upload_path", "文件上传存储根路径")]
        public string UploadPath { get; set; }

        /// <summary>
        /// 临时文件存储根路径
        /// </summary>
        [ConfigDescription("sys_path_temp_path", "临时文件存储根路径")]
        public string TempPath { get; set; }
    }
}
