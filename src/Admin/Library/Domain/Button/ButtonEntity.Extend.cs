using System;
using NetModular.Lib.Data.Abstractions.Attributes;

namespace NetModular.Module.Admin.Domain.Button
{
    public partial class ButtonEntity
    {
        [Ignore]
        public Guid RoleId { get; set; }
    }
}
