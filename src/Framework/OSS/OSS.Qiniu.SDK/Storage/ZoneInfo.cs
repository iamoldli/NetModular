namespace Qiniu.Storage
{
    /// <summary>
    /// 从uc.qbox.me返回的消息
    /// </summary>
    internal class ZoneInfo
    {
        public int Ttl { get; set; }
        public Io Io { set; get; }
        public Up Up { set; get; }
    }

    internal class Io
    {
        public Src Src { set; get; }
    }

    internal class Src
    {
        public string[] Main { set; get; }
    }

    internal class Up
    {
        public UpDomain Acc { set; get; }
        public UpDomain OldAcc { set; get; }
        public UpDomain Src { set; get; }
        public UpDomain OldSrc { set; get; }
    }
    internal class UpDomain
    {
        public string[] Main { set; get; }
        public string[] Backup { set; get; }
        public string Info { set; get; }
    }
}
