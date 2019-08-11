using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.CodeGenerator.Domain.ModelProperty;
using Nm.Module.CodeGenerator.Domain.ModelProperty.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
{
    public class ModelPropertyRepository : RepositoryAbstract<ModelPropertyEntity>, IModelPropertyRepository
    {
        public ModelPropertyRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<ModelPropertyEntity>> Query(ModelPropertyQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find(m => m.ClassId == model.ClassId && m.ModelType == model.ModelType);
            if (!paging.OrderBy.Any())
            {
                query.OrderBy(m => m.Sort);
            }

            var list = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .LeftJoin<ModelPropertyEntity>((x, y, z) => x.EnumId == z.Id)
                .Select((x, y, z) => new { x, Creator = y.Name, EnumName = z.Name })
                .PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<IList<ModelPropertyEntity>> QueryAll(ModelPropertyQueryModel model)
        {
            return Db.Find(m => m.ClassId == model.ClassId && m.ModelType == model.ModelType).ToListAsync();
        }

        public Task<IList<ModelPropertyEntity>> QueryByClass(Guid classId)
        {
            return Db.Find(m => m.ClassId == classId).ToListAsync();
        }

        public Task<bool> Exists(ModelPropertyEntity entity)
        {
            var query = Db.Find(m => m.ClassId == entity.ClassId && m.ModelType == entity.ModelType && m.Name == entity.Name);
            query.WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id);
            return query.ExistsAsync();
        }

        public Task<bool> DeleteByProject(Guid projectId, IDbTransaction transaction)
        {
            return Db.Find(m => m.ProjectId == projectId).UseTran(transaction).DeleteAsync();
        }
    }
}
