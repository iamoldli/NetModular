using System;
using NetModular.Lib.Auth.Abstractions;

namespace Data.Common
{
    public class LoginInfo : ILoginInfo
    {
        public Guid? TenantId { get; }

        public Guid AccountId => Guid.Parse("39F08CFD-8E0D-771B-A2F3-2639A62CA2FA");

        public string AccountName { get; }

        public AccountType AccountType { get; }

        public Platform Platform { get; }

        public string IP { get; }

        public string IPv4 { get; }

        public string IPv6 { get; }

        public long LoginTime { get; }

        public string UserAgent { get; }
    }
}
