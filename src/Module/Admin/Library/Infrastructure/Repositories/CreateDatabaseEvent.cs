using System;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Utils.Core.Encrypt;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.AccountRole;
using Nm.Module.Admin.Domain.Role;

namespace Nm.Module.Admin.Infrastructure.Repositories
{
    public class CreateDatabaseEvent : IDatabaseCreateEvents
    {
        public IDbContext DbContext { get; set; }

        public Task Before()
        {
            return Task.CompletedTask;
        }

        public async Task After()
        {
            if (!await DbContext.Set<RoleEntity>().Find().ExistsAsync())
            {
                var roleId = await AddRole();
                await AddAccount(roleId);
            }
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        private async Task<Guid> AddRole()
        {
            var db = DbContext.Set<RoleEntity>();
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

        /// <summary>
        /// 创建管理员账户
        /// </summary>
        private async Task AddAccount(Guid roleId)
        {
            var db = DbContext.Set<AccountEntity>();
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

            await AddAccountRole(account.Id, roleId);
        }

        /// <summary>
        /// 绑定管理员与角色
        /// </summary>
        private Task AddAccountRole(Guid accountId, Guid roleId)
        {
            var db = DbContext.Set<AccountRoleEntity>();

            var accountRole = new AccountRoleEntity
            {
                AccountId = accountId,
                RoleId = roleId
            };

            return db.InsertAsync(accountRole);
        }
    }
}
