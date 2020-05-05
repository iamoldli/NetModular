using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Qiniu.Util;

namespace Qiniu.Http
{
    /// <summary>
    /// HttpManager for .NET 2.0/3.0/3.5/4.0
    /// </summary>
    public class HttpManager
    {
        private bool allowAutoRedirect;
        private string userAgent;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="allowAutoRedirect">是否允许HttpWebRequest的“重定向”，默认禁止</param>
        public HttpManager(bool allowAutoRedirect = false)
        {
            this.allowAutoRedirect = allowAutoRedirect;
            userAgent = GetUserAgent();            
        }

        /// <summary>
        /// 客户端标识(UserAgent)，示例："SepcifiedClient/1.1 (Universal)"
        /// </summary>
        /// <returns>客户端标识UA</returns>
        public static string GetUserAgent()
        {
            string osDesc = Environment.OSVersion.Platform + "; " + Environment.OSVersion.Version;
            return string.Format("{0}/{1} ({2}; {3})", QiniuCSharpSDK.ALIAS, QiniuCSharpSDK.VERSION, QiniuCSharpSDK.RTFX, osDesc);
        }

        /// <summary>
        /// 设置自定义的客户端标识(UserAgent)，示例："SepcifiedClient/1.1 (Universal)"
        /// 如果设置为空白或者不设置，SDK会自动使用默认的UserAgent
        /// </summary>
        /// <param name="userAgent">用户自定义的UserAgent</param>
        /// <returns>客户端标识UA</returns>
        public void SetUserAgent(string userAgent)
        {
            if(!string.IsNullOrEmpty(userAgent))
            {
                this.userAgent = userAgent;
            }
        }

        /// <summary>
        /// 多部分表单数据(multi-part form-data)的分界(boundary)标识
        /// </summary>
        /// <returns>分界(boundary)标识字符串</returns>
        public static string CreateFormDataBoundary()
        {
            string now = DateTime.UtcNow.Ticks.ToString();
            return string.Format("-------{0}Boundary{1}", QiniuCSharpSDK.ALIAS, Hashing.CalcMD5X(now));
        }

        /// <summary>
        /// HTTP-GET方法
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="token">令牌(凭证)[可选->设置为null]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-GET的响应结果</returns>
        public HttpResult Get(string url, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;

            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "GET";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }
                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = allowAutoRedirect;
                wReq.ServicePoint.Expect100Continue = false;

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-GET] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }

        /// <summary>
        /// HTTP-POST方法(不包含body数据)
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="token">令牌(凭证)[可选]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-POST的响应结果</returns>
        public HttpResult Post(string url, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;

            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "POST";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }
                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = allowAutoRedirect;
                wReq.ServicePoint.Expect100Continue = false;

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-POST] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }

        /// <summary>
        /// HTTP-POST方法(包含body数据)
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="data">主体数据(字节数据)</param>
        /// <param name="token">令牌(凭证)[可选->设置为null]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-POST的响应结果</returns>
        public HttpResult PostData(string url, byte[] data, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;

            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "POST";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }
                wReq.ContentType = ContentType.APPLICATION_OCTET_STREAM;
                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = allowAutoRedirect;
                wReq.ServicePoint.Expect100Continue = false;

                if (data != null)
                {
                    wReq.AllowWriteStreamBuffering = true;
                    using (Stream sReq = wReq.GetRequestStream())
                    {
                        sReq.Write(data, 0, data.Length);
                        sReq.Flush();
                    }
                }

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-POST-BIN] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }

        /// <summary>
        /// HTTP-POST方法(包含body数据)
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="data">主体数据(字节数据)</param>
        /// <param name="mimeType">主体数据内容类型</param>
        /// <param name="token">令牌(凭证)[可选]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-POST的响应结果</returns>
        public HttpResult PostData(string url, byte[] data, string mimeType, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;

            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "POST";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }
                wReq.ContentType = mimeType;
                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = allowAutoRedirect;
                wReq.ServicePoint.Expect100Continue = false;

                if (data != null)
                {
                    wReq.AllowWriteStreamBuffering = true;
                    using (Stream sReq = wReq.GetRequestStream())
                    {
                        sReq.Write(data, 0, data.Length);
                        sReq.Flush();
                    }
                }

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-POST-BIN] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }

        /// <summary>
        /// HTTP-POST方法(包含JSON文本的body数据)
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="data">主体数据(JSON文本)</param>
        /// <param name="token">令牌(凭证)[可选]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-POST的响应结果</returns>
        public HttpResult PostJson(string url, string data, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;

            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "POST";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }
                wReq.ContentType = ContentType.APPLICATION_JSON;
                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = allowAutoRedirect;
                wReq.ServicePoint.Expect100Continue = false;

                if (data != null)
                {
                    wReq.AllowWriteStreamBuffering = true;
                    using (Stream sReq = wReq.GetRequestStream())
                    {
                        sReq.Write(Encoding.UTF8.GetBytes(data), 0, data.Length);
                        sReq.Flush();
                    }
                }

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-POST-JSON] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }

        /// <summary>
        /// HTTP-POST方法(包含普通文本的body数据)
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="data">主体数据(普通文本)</param>
        /// <param name="token">令牌(凭证)[可选->设置为null]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-POST的响应结果</returns>
        public HttpResult PostText(string url, string data, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;

            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "POST";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }
                wReq.ContentType = ContentType.TEXT_PLAIN;
                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = allowAutoRedirect;
                wReq.ServicePoint.Expect100Continue = false;

                if (data != null)
                {
                    wReq.AllowWriteStreamBuffering = true;
                    using (Stream sReq = wReq.GetRequestStream())
                    {
                        sReq.Write(Encoding.UTF8.GetBytes(data), 0, data.Length);
                        sReq.Flush();
                    }
                }

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-POST-TEXT] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }

        /// <summary>
        /// HTTP-POST方法(包含表单数据)
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="kvData">键值对数据</param>
        /// <param name="token">令牌(凭证)[可选->设置为null]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-POST的响应结果</returns>
        public HttpResult PostForm(string url, Dictionary<string, string> kvData, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;

            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "POST";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }
                wReq.ContentType = ContentType.WWW_FORM_URLENC;
                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = allowAutoRedirect;
                wReq.ServicePoint.Expect100Continue = false;

                if (kvData != null)
                {
                    StringBuilder sbb = new StringBuilder();
                    foreach (var kv in kvData)
                    {
                        sbb.AppendFormat("{0}={1}&", Uri.EscapeDataString(kv.Key), Uri.EscapeDataString(kv.Value));
                    }

                    wReq.AllowWriteStreamBuffering = true;
                    using (Stream sReq = wReq.GetRequestStream())
                    {
                        sReq.Write(Encoding.UTF8.GetBytes(sbb.ToString()), 0, sbb.Length - 1);
                        sReq.Flush();
                    }
                }

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-POST-FORM] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }

        /// <summary>
        /// HTTP-POST方法(包含表单数据)
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="data">表单数据</param>
        /// <param name="token">令牌(凭证)[可选->设置为null]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-POST的响应结果</returns>
        public HttpResult PostForm(string url, string data, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;

            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "POST";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }
                wReq.ContentType = ContentType.WWW_FORM_URLENC;
                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = allowAutoRedirect;
                wReq.ServicePoint.Expect100Continue = false;

                if (!string.IsNullOrEmpty(data))
                {
                    wReq.AllowWriteStreamBuffering = true;
                    using (Stream sReq = wReq.GetRequestStream())
                    {
                        sReq.Write(Encoding.UTF8.GetBytes(data), 0, data.Length);
                        sReq.Flush();
                    }
                }

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-POST-FORM] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }

        /// <summary>
        /// HTTP-POST方法(包含表单数据)
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="data">表单数据</param>
        /// <param name="token">令牌(凭证)[可选->设置为null]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-POST的响应结果</returns>
        public HttpResult PostForm(string url, byte[] data, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;

            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "POST";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }
                wReq.ContentType = ContentType.WWW_FORM_URLENC;
                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = allowAutoRedirect;
                wReq.ServicePoint.Expect100Continue = false;

                if (data != null)
                {
                    wReq.AllowWriteStreamBuffering = true;
                    using (Stream sReq = wReq.GetRequestStream())
                    {
                        sReq.Write(data, 0, data.Length);
                        sReq.Flush();
                    }
                }

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-POST-FORM] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"),userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }

        /// <summary>
        /// HTTP-POST方法(包含多分部数据,multipart/form-data)
        /// </summary>
        /// <param name="url">请求目标URL</param>
        /// <param name="data">主体数据</param>
        /// <param name="boundary">分界标志</param>
        /// <param name="token">令牌(凭证)[可选->设置为null]</param>
        /// <param name="binaryMode">是否以二进制模式读取响应内容(默认:否，即表示以文本方式读取)</param>
        /// <returns>HTTP-POST的响应结果</returns>
        public HttpResult PostMultipart(string url, byte[] data, string boundary, string token, bool binaryMode = false)
        {
            HttpResult result = new HttpResult();

            HttpWebRequest wReq = null;

            try
            {
                wReq = WebRequest.Create(url) as HttpWebRequest;
                wReq.Method = "POST";
                if (!string.IsNullOrEmpty(token))
                {
                    wReq.Headers.Add("Authorization", token);
                }
                wReq.ContentType = string.Format("{0}; boundary={1}", ContentType.MULTIPART_FORM_DATA, boundary);
                wReq.UserAgent = userAgent;
                wReq.AllowAutoRedirect = allowAutoRedirect;
                wReq.ServicePoint.Expect100Continue = false;

                wReq.AllowWriteStreamBuffering = true;
                using (Stream sReq = wReq.GetRequestStream())
                {
                    sReq.Write(data, 0, data.Length);
                    sReq.Flush();
                }

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;

                if (wResp != null)
                {
                    result.Code = (int)wResp.StatusCode;
                    result.RefCode = (int)wResp.StatusCode;

                    getHeaders(ref result, wResp);

                    if (binaryMode)
                    {
                        int len = (int)wResp.ContentLength;
                        result.Data = new byte[len];
                        int bytesLeft = len;
                        int bytesRead = 0;

                        using (BinaryReader br = new BinaryReader(wResp.GetResponseStream()))
                        {
                            while (bytesLeft > 0)
                            {
                                bytesRead = br.Read(result.Data, len - bytesLeft, bytesLeft);
                                bytesLeft -= bytesRead;
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(wResp.GetResponseStream()))
                        {
                            result.Text = sr.ReadToEnd();
                        }
                    }

                    wResp.Close();
                }
            }
            catch (WebException wex)
            {
                HttpWebResponse xResp = wex.Response as HttpWebResponse;
                if (xResp != null)
                {
                    result.Code = (int)xResp.StatusCode;
                    result.RefCode = (int)xResp.StatusCode;

                    getHeaders(ref result, xResp);

                    using (StreamReader sr = new StreamReader(xResp.GetResponseStream()))
                    {
                        result.Text = sr.ReadToEnd();
                    }

                    xResp.Close();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [{1}] [HTTP-POST-MPART] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), userAgent);
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (wReq != null)
                {
                    wReq.Abort();
                }
            }

            return result;
        }

        /// <summary>
        /// 获取返回信息头
        /// </summary>
        /// <param name="hr">即将被HTTP请求封装函数返回的HttpResult变量</param>
        /// <param name="resp">正在被读取的HTTP响应</param>
        private void getHeaders(ref HttpResult hr, HttpWebResponse resp)
        {
            if (resp != null)
            {
                if (hr.RefInfo == null)
                {
                    hr.RefInfo = new Dictionary<string, string>();
                }

                hr.RefInfo.Add("ProtocolVersion", resp.ProtocolVersion.ToString());

                if (!string.IsNullOrEmpty(resp.CharacterSet))
                {
                    hr.RefInfo.Add("Characterset", resp.CharacterSet);
                }

                if (!string.IsNullOrEmpty(resp.ContentEncoding))
                {
                    hr.RefInfo.Add("ContentEncoding", resp.ContentEncoding);
                }

                if (!string.IsNullOrEmpty(resp.ContentType))
                {
                    hr.RefInfo.Add("ContentType", resp.ContentType);
                }

                hr.RefInfo.Add("ContentLength", resp.ContentLength.ToString());                

                var headers = resp.Headers;
                if (headers != null && headers.Count > 0)
                {
                    if (hr.RefInfo == null)
                    {
                        hr.RefInfo = new Dictionary<string, string>();
                    }
                    foreach (var key in headers.AllKeys)
                    {
                        hr.RefInfo.Add(key, headers[key]);
                    }
                }
            }
        }

    }
}