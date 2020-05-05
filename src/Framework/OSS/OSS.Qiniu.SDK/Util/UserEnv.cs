using System;

#if WINDOWS_UWP
using System.Threading.Tasks;
using Windows.Storage;
#else
using System.IO;
#endif

namespace Qiniu.Util
{
    /// <summary>
    /// 环境变量-用户路径
    /// </summary>
    public class UserEnv
    {

#if Net20 || Net35 || Net40 || Net45 || Net46

        /// <summary>
        /// 找到QHome目录（在用户目录下建立的"QHome"文件夹）
        /// </summary>
        /// <returns>QHOME路径</returns>
        public static string GetHomeFolder()
        {
            // Windows下Home目录 = %HOMEDRIVE% + %HOMEPATH%
            string homeDir = Environment.GetEnvironmentVariable("HOMEDRIVE") + Environment.GetEnvironmentVariable("HOMEPATH");

            if (string.IsNullOrEmpty(homeDir))
            {
                // 如果获取失败，就设置为当前路径
                homeDir = Path.GetFullPath(".");
            }

            string homeFolder = Path.Combine(homeDir, "QHome");

            if(!Directory.Exists(homeFolder))
            {
                Directory.CreateDirectory(homeFolder);
            }

            return homeFolder;
        }

#elif NetCore

        /// <summary>
        /// 找到QHome目录（在用户目录下建立的"QHome"文件夹）
        /// </summary>
        /// <returns>QHOME路径</returns>
        public static string GetHomeFolder()
        {
            // Windows下Home目录 = %HOMEDRIVE% + %HOMEPATH%
            string homeDir = Environment.GetEnvironmentVariable("HOMEDRIVE") + Environment.GetEnvironmentVariable("HOMEPATH");

            // OSX/Ubuntu下Home目录 = $HOME
            if (string.IsNullOrEmpty(homeDir))
            {
                homeDir = Environment.GetEnvironmentVariable("HOME");
            }

            if (string.IsNullOrEmpty(homeDir))
            {
                // 如果获取失败，就设置为当前路径
                homeDir = Path.GetFullPath(".");
            }

            string homeFolder = Path.Combine(homeDir, "QHome");

            if(!Directory.Exists(homeFolder))
            {
                Directory.CreateDirectory(homeFolder);
            }

            return homeFolder;
        }

#elif WINDOWS_UWP

        /// <summary>
        /// 找到QHome目录（在"应用缓存"目录下建立的"QHome"文件夹）
        /// </summary>
        /// <returns>QHOME路径</returns>
        public static async Task<StorageFolder> GetHomeFolderAsync()
        {
            return await ApplicationData.Current.LocalCacheFolder.CreateFolderAsync("QHome", CreationCollisionOption.OpenIfExists);
            //return await KnownFolders.DocumentsLibrary.CreateFolderAsync("QHome", CreationCollisionOption.OpenIfExists);
        }

#else

        /// <summary>
        /// 找到QHome目录（在当前目录下建立的"QHome"文件夹）
        /// </summary>
        /// <returns>QHOME路径</returns>
        public static string GetHomeFolder()
        {
            //当前路径
            string homeFolder = Path.GetFullPath("./QHome");

            if(!Directory.Exists(homeFolder))
            {
                Directory.CreateDirectory(homeFolder);
            }
            return homeFolder;
        }

#endif

    }
}
