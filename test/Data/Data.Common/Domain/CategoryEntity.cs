using System;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Core.Entities;

namespace Data.Common.Domain
{
    public class CategoryEntity : Entity<Guid>, ITenant
    {
        public Guid? TenantId { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }
    }
}
