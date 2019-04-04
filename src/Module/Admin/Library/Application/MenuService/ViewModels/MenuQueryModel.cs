using System;
using NetModular.Lib.Data.Query;

namespace NetModular.Module.Admin.Application.MenuService.ViewModels
{
    public class MenuQueryModel : QueryModel
    {
        /// <summary>
        /// 父节点编号
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路由名称
        /// </summary>
        public string RouteName { get; set; }
    }
}
