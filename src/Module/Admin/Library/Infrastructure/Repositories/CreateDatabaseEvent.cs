using System;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Utils.Core.Encrypt;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.AccountRole;
using Nm.Module.Admin.Domain.Role;

namespace Nm.Module.Admin.Infrastructure.Repositories
{
    public class CreateDatabaseEvent : ICreateDatabaseEvent
    {
        public Task Before(IDbContext dbContext)
        {
            return Task.CompletedTask;
        }

        public async Task After(IDbContext dbContext)
        {
            if (!await dbContext.Set<RoleEntity>().Find().ExistsAsync())
            {
                var roleId = await AddRole(dbContext);
                await AddAccount(dbContext, roleId);
            }
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        private async Task<Guid> AddRole(IDbContext dbContext)
        {
            var db = dbContext.Set<RoleEntity>();
            var role = new RoleEntity
            {
                Name = "超级管理员",
                Remarks = "超级管理员",
                IsSpecified = false,
                CreatedTime = DateTime.Now,
                CreatedBy = Guid.Empty,
                ModifiedTime = DateTime.Now,
                ModifiedBy = Guid.Empty,
                Deleted = false,
                DeletedTime = DateTime.Now,
                DeletedBy = Guid.Empty
            };

            await db.InsertAsync(role);

            return role.Id;
        }

        private async Task AddAccount(IDbContext dbContext, Guid roleId)
        {
            var db = dbContext.Set<AccountEntity>();
            var account = new AccountEntity
            {
                UserName = "admin",
                Password = Md5Encrypt.Encrypt("admin_admin"),
                Name = "管理员",
                CreatedTime = DateTime.Now,
                CreatedBy = Guid.Empty,
                ModifiedTime = DateTime.Now,
                ModifiedBy = Guid.Empty,
                Deleted = false,
                DeletedTime = DateTime.Now,
                DeletedBy = Guid.Empty
            };

            await db.InsertAsync(account);

            await AddAccountRole(dbContext, account.Id, roleId);
        }

        private Task AddAccountRole(IDbContext dbContext, Guid accountId, Guid roleId)
        {
            var db = dbContext.Set<AccountRoleEntity>();

            var accountRole = new AccountRoleEntity
            {
                AccountId = accountId,
                RoleId = roleId
            };

            return db.InsertAsync(accountRole);
        }
    }
}
