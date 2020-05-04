using System;
using System.Timers;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using Qiniu.Storage;
using Qiniu.Util;

namespace NetModular.Lib.OSS.Qiniu
{
    /// <summary>
    /// 七牛Token管理器
    /// </summary>
    [Singleton]
    public class QiniuTokenManager
    {
        private string _token;
        private Timer _timer;
        private readonly QiniuConfig _config;
        public QiniuTokenManager(OSSConfig config)
        {
            _config = config.Qiniu;
            RefreshTimer();
        }

        /// <summary>
        /// 刷新定时器
        /// </summary>
        public void RefreshTimer()
        {
            if (_config.Check())
            {
                _timer?.Dispose();

                var interval = _config.TokenExpires < 120 ? 120 : _config.TokenExpires - 120;
                _timer = new Timer(interval);
                _timer.Elapsed += OnTimedEvent;
                _timer.AutoReset = true;
            }
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            RefreshToken();
        }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        private void RefreshToken()
        {
            if (_config.Check())
            {
                var mac = new Mac(_config.AccessKey, _config.SecretKey);
                var putPolicy = new PutPolicy
                {
                    Scope = _config.Bucket
                };
                var expires = _config.TokenExpires < 120 ? 300 : _config.TokenExpires - 120;
                putPolicy.SetExpires(expires);
                _token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            }

        }

        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            if (_token.IsNull())
            {
                RefreshToken();
            }
            return _token;
        }
    }
}
