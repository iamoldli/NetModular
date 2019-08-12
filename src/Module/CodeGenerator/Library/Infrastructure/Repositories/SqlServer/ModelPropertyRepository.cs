using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.CodeGenerator.Domain.ModelProperty;
using Tm.Module.CodeGenerator.Domain.ModelProperty.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
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
