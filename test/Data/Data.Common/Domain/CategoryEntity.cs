using System;
using NetModular.Lib.Data.Core.Entities;

namespace Data.Common.Domain
{
    public class CategoryEntity : Entity<Guid>
    {
        public string Name { get; set; }

        public int Count { get; set; }
    }
}
