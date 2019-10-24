namespace Nm.Module.Admin.Infrastructure
{
    public static class CacheKeys
    {
        /// <summary>
        /// 验证码缓存Key
        /// </summary>
        public const string VerifyCodeKey = "ADMIN_VERIFY_CODE_";

        /// <summary>
        /// 账户权限列表缓存Key
        /// </summary>
        public const string AccountPermissionListKey = "ADMIN_ACCOUNT_PERMISSION_LIST_";

        /// <summary>
        /// 系统配置
        /// </summary>
        public const string SystemConfigCacheKey = "ADMIN_SYSTEM_CONFIG";
    }
}
