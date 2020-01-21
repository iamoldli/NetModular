namespace NetModular.Module.Admin.Infrastructure.PasswordHandler
{
    /// <summary>
    /// 密码处理器接口
    /// </summary>
    public interface IPasswordHandler
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string Encrypt(string userName, string password);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptPassword"></param>
        /// <returns></returns>
        string Decrypt(string encryptPassword);
    }
}
