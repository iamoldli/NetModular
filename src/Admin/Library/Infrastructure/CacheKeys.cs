using System.ComponentModel;

namespace NetModular.Module.Admin.Infrastructure
{
    public static class CacheKeys
    {
        /// <summary>
        /// 验证码 ADMIN:VERIFY:CODE:
        /// </summary>
        [Description("验证码")] public const string VerifyCodeKey = "ADMIN:VERIFY:CODE:{0}";

        /// <summary>
        /// 刷新令牌，缓存账户认证信息 ADMIN:AUTH:REFRESHTOKEN:刷新令牌
        /// </summary>
        [Description("刷新令牌")] public const string RefreshToken = "ADMIN:AUTH:REFRESHTOKEN:{0}";

        /// <summary>
        /// 账户信息缓存 ADMIN:ACCOUNT:账户编号
        /// </summary>
        [Description("账户信息缓存")] public const string Account = "ADMIN:ACCOUNT:{0}:INFO";

        /// <summary>
        /// 账户认证信息 ADMIN:ACCOUNT:账户编号:AUTHINFO:平台类型编号
        /// </summary>
        [Description("账户认证信息")] public const string AccountAuthInfo = "ADMIN:ACCOUNT:{0}:AUTHINFO:{1}";

        /// <summary>
        /// 账户权限列表 ADMIN:ACCOUNT:账户编号:PERMISSIONS:
        /// </summary>
        [Description("账户权限列表")] public const string AccountPermissions = "ADMIN:ACCOUNT:{0}:PERMISSIONS:{1}";

        /// <summary>
        /// 权限树 ADMIN:PERMISSION:TREE
        /// </summary>
        [Description("权限树缓存")] public const string PermissionTree = "ADMIN:PERMISSION:TREE";
    }
}
