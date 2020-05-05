using System;
using Qiniu.Http;

namespace Qiniu.Storage
{
    class QiniuException :Exception
    {
        public string message;
        public HttpResult HttpResult;
        public QiniuException(HttpResult httpResult, string message)
        {
            this.HttpResult = httpResult == null ? new HttpResult() : httpResult;
            this.message = message;
        }

        public override string Message
        {
            get
            {
                return this.message;
            }
        }
    }
}
