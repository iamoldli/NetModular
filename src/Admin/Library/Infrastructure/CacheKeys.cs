namespace NetModular.Module.Admin.Infrastructure
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
        /// 权限树缓存
        /// </summary>
        public const string PermissionTree = "ADMIN_PERMISSION_TREE";
    }
}
