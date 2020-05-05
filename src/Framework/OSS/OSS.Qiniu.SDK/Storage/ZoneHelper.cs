using System;
using System.Text;
using System.Collections.Generic;
using Qiniu.Http;
using Newtonsoft.Json;

namespace Qiniu.Storage
{
    /// <summary>
    /// Zone辅助类，查询及配置Zone
    /// </summary>
    public class ZoneHelper
    {
        private static Dictionary<string, Zone> zoneCache = new Dictionary<string, Zone>();
        private static object rwLock = new object();

        /// <summary>
        /// 从uc.qbox.me查询得到回复后，解析出upHost,然后根据upHost确定Zone
        /// </summary>
        /// <param name="accessKey">AccessKek</param>
        /// <param name="bucket">空间名称</param>
        public static Zone QueryZone(string accessKey, string bucket)
        {
            Zone zone = null;

            string cacheKey = string.Format("{0}:{1}", accessKey, bucket);

            //check from cache
            lock (rwLock)
            {
                if (zoneCache.ContainsKey(cacheKey))
                {
                    zone = zoneCache[cacheKey];
                }
            }

            if (zone != null)
            {
                return zone;
            }

            //query from uc api
            HttpResult hr = null;
            try
            {
                string queryUrl = string.Format("https://uc.qbox.me/v2/query?ak={0}&bucket={1}", accessKey, bucket);
                HttpManager httpManager = new HttpManager();
                hr = httpManager.Get(queryUrl, null);
                if (hr.Code == (int)HttpCode.OK)
                {
                    ZoneInfo zInfo = JsonConvert.DeserializeObject<ZoneInfo>(hr.Text);
                    if (zInfo != null)
                    {
                        zone = new Zone();
                        zone.SrcUpHosts = zInfo.Up.Src.Main;
                        zone.CdnUpHosts = zInfo.Up.Acc.Main;
                        zone.IovipHost = zInfo.Io.Src.Main[0];
                        if (zone.IovipHost.Contains("z1"))
                        {
                            zone.ApiHost = "api-z1.qiniu.com";
                            zone.RsHost = "rs-z1.qiniu.com";
                            zone.RsfHost = "rsf-z1.qiniu.com";
                        }
                        else if (zone.IovipHost.Contains("z1"))
                        {
                            zone.ApiHost = "api-z2.qiniu.com";
                            zone.RsHost = "rs-z2.qiniu.com";
                            zone.RsfHost = "rsf-z2.qiniu.com";
                        }
                        else if (zone.IovipHost.Contains("na0"))
                        {
                            zone.ApiHost = "api-na0.qiniu.com";
                            zone.RsHost = "rs-na0.qiniu.com";
                            zone.RsfHost = "rsf-na0.qiniu.com";
                        }
                        else
                        {
                            zone.ApiHost = "api.qiniu.com";
                            zone.RsHost = "rs.qiniu.com";
                            zone.RsfHost = "rsf.qiniu.com";
                        }

                        lock (rwLock)
                        {
                            zoneCache[cacheKey] = zone;
                        }
                    }
                    else
                    {
                        throw new Exception("JSON Deserialize failed: " + hr.Text);
                    }
                }
                else
                {
                    throw new Exception("code: " + hr.Code + ", text: " + hr.Text + ", ref-text:" + hr.RefText);
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] QueryZone Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                throw new QiniuException(hr, sb.ToString());
            }

            return zone;
        }
    }

}
