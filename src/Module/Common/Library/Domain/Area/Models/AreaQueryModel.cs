using System;
using  Nm.Lib.Data.Query;

namespace  Nm.Module.Common.Domain.Area.Models
{
    public class AreaQueryModel : QueryModel
    {
        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string Pinyin { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 区号
        /// </summary>
        public string AreaCode { get; set; }

    }
}
