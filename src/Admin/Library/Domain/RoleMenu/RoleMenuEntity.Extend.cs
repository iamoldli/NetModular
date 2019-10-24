using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Module.Admin.Domain.Menu;

namespace Nm.Module.Admin.Domain.RoleMenu
{
    public partial class RoleMenuEntity
    {
        [Ignore]
        public MenuType MenuType { get; set; }
    }
}
