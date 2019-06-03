using System;
using System.Collections.Generic;
using System.Text;
using Nm.Lib.Data.Abstractions;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.MySql
{
    public class ClassMethodRepository : SqlServer.ClassMethodRepository
    {
        public ClassMethodRepository(IDbContext context) : base(context)
        {
        }
    }
}
