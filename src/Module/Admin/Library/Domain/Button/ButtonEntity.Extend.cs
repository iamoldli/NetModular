using System;
using Tm.Lib.Data.Abstractions.Attributes;

namespace Tm.Module.Admin.Domain.Button
{
    public partial class ButtonEntity
    {
        [Ignore]
        public Guid RoleId { get; set; }
    }
}
