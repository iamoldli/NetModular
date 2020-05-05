using System;
using System.IO;
using Qiniu.Util;
using Newtonsoft.Json;

namespace Qiniu.Storage
{
    /// <summary>
    /// 断点续上传辅助函数Load/Save
    /// </summary>
    public class ResumeHelper
    {

        /// <summary>
        /// 生成默认的断点记录文件名称
        /// </summary>
        /// <param name="localFile">待上传的本地文件</param>
        /// <param name="key">要保存的目标key</param>
        /// <returns>用于记录断点信息的文件名</returns>
        public static string GetDefaultRecordKey(string localFile, string key)
        {
            string tempDir = System.Environment.GetEnvironmentVariable("TEMP");
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(localFile);
            string uniqueKey = string.Format("{0}:{1}:{2}", localFile, key, fileInfo.LastWriteTime.ToFileTime());
            return string.Format("{0}\\{1}",tempDir,"QiniuResume_" + Hashing.CalcMD5X(uniqueKey));
        }

        /// <summary>
        /// 尝试从从文件载入断点信息
        /// </summary>
        /// <param name="recordFile">断点记录文件</param>
        /// <returns>断点信息</returns>
        public static ResumeInfo Load(string recordFile)
        {
            ResumeInfo resumeInfo = null;

            try
            {
                using (FileStream fs = new FileStream(recordFile, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string jsonStr = sr.ReadToEnd();
                        resumeInfo=JsonConvert.DeserializeObject<ResumeInfo>(jsonStr);
                    }
                }
            }
            catch (Exception)
            {
                resumeInfo = null;
            }

            return resumeInfo;
        }

        /// <summary>
        /// 保存断点信息到文件
        /// </summary>
        /// <param name="resumeInfo">断点信息</param>
        /// <param name="recordFile">断点记录文件</param>
        public static void Save(ResumeInfo resumeInfo, string recordFile)
        {
            string jsonStr = resumeInfo.ToJsonStr();

            using (FileStream fs = new FileStream(recordFile, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(jsonStr);
                }
            }
        }
    }
}
