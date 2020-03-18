using NetModular.Lib.Utils.Core.Attributes;

namespace Utils.Core.Tests.Log
{
    [Singleton]
    public class TextLog : ILog
    {
        public string Debug()
        {
            return "TextLog";
        }
    }
}
