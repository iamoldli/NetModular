using System.Collections.Generic;

namespace Tm.Lib.Utils.Core.Result
{
    public class TreeResultModel<T> where T : class, new()
    {
        /// <summary>
        /// 子节点
        /// </summary>
        public List<T> Children { get; set; } = new List<T>();
    }
}
