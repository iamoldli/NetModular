using System.ComponentModel;

namespace NetModular.Module.Admin.Infrastructure
{
    public static class CacheKeys
    {
        /// <summary>
        /// 验证码
        /// <para>ADMIN:AUTH:VERIFY_CODE:验证码编号</para>
        /// </summary>
        [Description("验证码")] 
        public const string AUTH_VERIFY_CODE = "ADMIN:AUTH:VERIFY_CODE";

        /// <summary>
        /// 刷新令牌 
        /// <para>ADMIN:AUTH:REFRESH_TOKEN:刷新令牌</para>
        /// </summary>
        [Description("刷新令牌")] 
        public const string AUTH_REFRESH_TOKEN = "ADMIN:AUTH:REFRESH_TOKEN";

        /// <summary>
        /// 账户信息
        /// <para>ADMIN:ACCOUNT:INFO:账户编号</para>
        /// </summary>
        [Description("账户信息")] 
        public const string ACCOUNT = "ADMIN:ACCOUNT:INFO";

        /// <summary>
        /// 账户认证信息
        /// <para>ADMIN:ACCOUNT:AUTH_INFO:账户编号:平台类型</para>
        /// </summary>
        [Description("账户认证信息")] 
        public const string ACCOUNT_AUTH_INFO = "ADMIN:ACCOUNT:AUTH_INFO";

        /// <summary>
        /// 账户权限列表
        /// <para>ADMIN:ACCOUNT:PERMISSIONS:账户编号:平台类型</para>
        /// </summary>
        [Description("账户权限列表")] 
        public const string ACCOUNT_PERMISSIONS = "ADMIN:ACCOUNT:PERMISSIONS";

        /// <summary>
        /// 权限树
        /// <para>ADMIN:PERMISSION:TREE</para>
        /// </summary>
        [Description("权限树")] 
        public const string PERMISSION_TREE = "ADMIN:PERMISSION:TREE";
    }
}
