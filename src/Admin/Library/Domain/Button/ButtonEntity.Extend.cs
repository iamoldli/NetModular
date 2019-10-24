using System;
using Nm.Lib.Data.Abstractions.Attributes;

namespace Nm.Module.Admin.Domain.Button
{
    public partial class ButtonEntity
    {
        [Ignore]
        public Guid RoleId { get; set; }
    }
}
