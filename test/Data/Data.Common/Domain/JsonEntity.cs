using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Common.Domain
{
    public class JsonEntity : Entity<Guid>
    {
        [Column("body", typeName:"jsonb")]
        public string Body { get; set; }
    }
}
