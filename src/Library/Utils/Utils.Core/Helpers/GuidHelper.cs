using System;
using System.Security.Cryptography;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Utils.Core.Helpers
{
    /// <summary>
    /// 有序Guid类型
    /// </summary>
    public enum SequentialGuidType
    {
        /// <summary>
        /// MySQL、PostgreSQL、SQLite使用
        /// </summary>
        SequentialAsString,
        /// <summary>
        /// Oracle使用
        /// </summary>
        SequentialAsBinary,
        /// <summary>
        /// SQL Server使用
        /// </summary>
        SequentialAtEnd
    }


    /// <summary>
    /// Guid帮助类
    /// </summary>
    [Singleton]
    public class GuidHelper
    {
        private readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

        /*
         * Database 	GUID Column 	  SequentialGuidType  Value 
         * SQL Server   uniqueidentifier 	SequentialAtEnd
         * MySQL 	    char(36) 	        SequentialAsString 
         * Oracle 	    raw(16) 	        SequentialAsBinary 
         * PostgreSQL 	uuid 	            SequentialAsString 
         * SQLite  	    varies  	        SequentialAsString 
         */

        /// <summary>
        /// 生成有序Guid
        /// </summary>
        /// <param name="guidType"></param>
        /// <returns></returns>
        public Guid NewSequentialGuid(SequentialGuidType guidType)
        {
            var randomBytes = new byte[10];
            _rng.GetBytes(randomBytes);

            var timestamp = DateTime.UtcNow.Ticks / 10000L;
            var timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            var guidBytes = new byte[16];

            switch (guidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    // If formatting as a string, we have to reverse the order
                    // of the Data1 and Data2 blocks on little-endian systems.
                    if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }
                    break;

                case SequentialGuidType.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }
    }
}
