using System;
using System.Collections.Generic;
using System.Text;
using NetModular.Lib.Utils.Core.Encrypt;
using Xunit;

namespace Utils.Core.Test.Encrypt
{
    public class MD5EncryptTests
    {
        [Fact]
        public void EncryptTest()
        {
            var str = "oldli";
            var encryptStr = Md5Encrypt.Encrypt(str, true);

            Assert.Equal("5798a32794f531e50c12828665b715ad", encryptStr);
        }
    }
}
