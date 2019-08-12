using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Module.Admin.Domain.Menu;

namespace Tm.Module.Admin.Domain.RoleMenu
{
    public partial class RoleMenuEntity
    {
        [Ignore]
        public MenuType MenuType { get; set; }
    }
}
