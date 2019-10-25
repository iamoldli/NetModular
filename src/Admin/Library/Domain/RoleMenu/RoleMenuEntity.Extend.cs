using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Module.Admin.Domain.Menu;

namespace NetModular.Module.Admin.Domain.RoleMenu
{
    public partial class RoleMenuEntity
    {
        [Ignore]
        public MenuType MenuType { get; set; }
    }
}
