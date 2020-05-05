namespace Qiniu.Util
{
    /// <summary>
    /// MD5算法的3rdParty实现
    /// 参考https://github.com/Dozer74/MD5
    /// </summary>
    public class LabMD5
    {
        #region Helper

        private sealed class Digest
        {
            public uint A;
            public uint B;
            public uint C;
            public uint D;

            public Digest()
            {
                A = 0x67452301;
                B = 0xEFCDAB89;
                C = 0x98BADCFE;
                D = 0X10325476;
            }

            public override string ToString()
            {
                string st;
                st = BitHelper.ReverseByte(A).ToString("x8") +
                     BitHelper.ReverseByte(B).ToString("x8") +
                     BitHelper.ReverseByte(C).ToString("x8") +
                     BitHelper.ReverseByte(D).ToString("x8");
                return st;
            }
        }

        private static class BitHelper
        {
            /// <summary>
            /// rotate
            /// </summary>
            /// <param name="num">num</param>
            /// <param name="shift">shift</param>
            /// <returns></returns>
            public static uint RotateLeft(uint num, ushort shift)
            {
                return (num >> (32 - shift)) | (num << shift);
            }

            /// <summary>
            /// reverse
            /// </summary>
            public static uint ReverseByte(uint num)
            {
                return ((num & 0x000000ff) << 24) |
                       (num >> 24) |
                       ((num & 0x00ff0000) >> 8) |
                       ((num & 0x0000ff00) << 8);
            }
        }

        #endregion Helper

        #region Table

        /// <summary>
        /// table 4294967296*sin(i)
        /// </summary>
        private static readonly uint[] T =
        {
            0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
            0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
            0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
            0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
            0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
            0xd62f105d, 0x2441453, 0xd8a1e681, 0xe7d3fbc8,
            0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
            0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
            0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
            0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
            0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x4881d05,
            0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
            0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
            0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
            0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
            0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
        };

        #endregion Table

        private uint[] X = new uint[16];

        /// <summary>
        /// ComputeHash
        /// </summary>
        public string ComputeHash(byte[] bytes)
        {
            var dg = new Digest();
            var bMsg = CreatePaddedBuffer(bytes);

            var mesLength = (uint)(bMsg.Length * 8) / 32;

            for (uint i = 0; i < mesLength / 16; i++)
            {
                CopyBlock(bMsg, i);
                Transform(ref dg.A, ref dg.B, ref dg.C, ref dg.D);
            }
            return dg.ToString();
        }

        private void Transform(ref uint A, ref uint B, ref uint C, ref uint D)
        {
            var AA = A;
            var BB = B;
            var CC = C;
            var DD = D;

            /* Round 1 */
            F(ref A, B, C, D, 0, 7, 1);
            F(ref D, A, B, C, 1, 12, 2);
            F(ref C, D, A, B, 2, 17, 3);
            F(ref B, C, D, A, 3, 22, 4);
            F(ref A, B, C, D, 4, 7, 5);
            F(ref D, A, B, C, 5, 12, 6);
            F(ref C, D, A, B, 6, 17, 7);
            F(ref B, C, D, A, 7, 22, 8);
            F(ref A, B, C, D, 8, 7, 9);
            F(ref D, A, B, C, 9, 12, 10);
            F(ref C, D, A, B, 10, 17, 11);
            F(ref B, C, D, A, 11, 22, 12);
            F(ref A, B, C, D, 12, 7, 13);
            F(ref D, A, B, C, 13, 12, 14);
            F(ref C, D, A, B, 14, 17, 15);
            F(ref B, C, D, A, 15, 22, 16);

            /* Round 2 */
            G(ref A, B, C, D, 1, 5, 17);
            G(ref D, A, B, C, 6, 9, 18);
            G(ref C, D, A, B, 11, 14, 19);
            G(ref B, C, D, A, 0, 20, 20);
            G(ref A, B, C, D, 5, 5, 21);
            G(ref D, A, B, C, 10, 9, 22);
            G(ref C, D, A, B, 15, 14, 23);
            G(ref B, C, D, A, 4, 20, 24);
            G(ref A, B, C, D, 9, 5, 25);
            G(ref D, A, B, C, 14, 9, 26);
            G(ref C, D, A, B, 3, 14, 27);
            G(ref B, C, D, A, 8, 20, 28);
            G(ref A, B, C, D, 13, 5, 29);
            G(ref D, A, B, C, 2, 9, 30);
            G(ref C, D, A, B, 7, 14, 31);
            G(ref B, C, D, A, 12, 20, 32);

            /* Round 3 */
            H(ref A, B, C, D, 5, 4, 33);
            H(ref D, A, B, C, 8, 11, 34);
            H(ref C, D, A, B, 11, 16, 35);
            H(ref B, C, D, A, 14, 23, 36);
            H(ref A, B, C, D, 1, 4, 37);
            H(ref D, A, B, C, 4, 11, 38);
            H(ref C, D, A, B, 7, 16, 39);
            H(ref B, C, D, A, 10, 23, 40);
            H(ref A, B, C, D, 13, 4, 41);
            H(ref D, A, B, C, 0, 11, 42);
            H(ref C, D, A, B, 3, 16, 43);
            H(ref B, C, D, A, 6, 23, 44);
            H(ref A, B, C, D, 9, 4, 45);
            H(ref D, A, B, C, 12, 11, 46);
            H(ref C, D, A, B, 15, 16, 47);
            H(ref B, C, D, A, 2, 23, 48);

            /* Round 4 */
            I(ref A, B, C, D, 0, 6, 49);
            I(ref D, A, B, C, 7, 10, 50);
            I(ref C, D, A, B, 14, 15, 51);
            I(ref B, C, D, A, 5, 21, 52);
            I(ref A, B, C, D, 12, 6, 53);
            I(ref D, A, B, C, 3, 10, 54);
            I(ref C, D, A, B, 10, 15, 55);
            I(ref B, C, D, A, 1, 21, 56);
            I(ref A, B, C, D, 8, 6, 57);
            I(ref D, A, B, C, 15, 10, 58);
            I(ref C, D, A, B, 6, 15, 59);
            I(ref B, C, D, A, 13, 21, 60);
            I(ref A, B, C, D, 4, 6, 61);
            I(ref D, A, B, C, 11, 10, 62);
            I(ref C, D, A, B, 2, 15, 63);
            I(ref B, C, D, A, 9, 21, 64);

            A = A + AA;
            B = B + BB;
            C = C + CC;
            D = D + DD;
        }

        private byte[] CreatePaddedBuffer(byte[] mes)
        {
            var padSize = 448 - mes.Length * 8 % 512;


            var pad = (uint)((padSize + 512) % 512);
            if (pad == 0) pad = 512;

            var sizeMsgBuff = (uint)(mes.Length + pad / 8 + 8);
            var sizeMsg = (ulong)mes.Length * 8;
            var bMsg = new byte[sizeMsgBuff];

            for (var i = 0; i < mes.Length; i++)
                bMsg[i] = mes[i];

            bMsg[mes.Length] |= 0x80; 

            for (var i = 8; i > 0; i--)
                bMsg[sizeMsgBuff - i] = (byte)((sizeMsg >> ((8 - i) * 8)) & 0x00000000000000ff); 
            return bMsg;
        }

        private void CopyBlock(byte[] bMsg, uint block)
        {
            block = block << 6;
            for (uint j = 0; j < 61; j += 4)
                X[j >> 2] = ((uint)bMsg[block + j + 3] << 24) |
                            ((uint)bMsg[block + j + 2] << 16) |
                            ((uint)bMsg[block + j + 1] << 8) |
                            bMsg[block + j];
        }

        #region Transform

        private void F(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + BitHelper.RotateLeft(a + ((b & c) | (~b & d)) + X[k] + T[i - 1], s);
        }

        private void G(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + BitHelper.RotateLeft(a + ((b & d) | (c & ~d)) + X[k] + T[i - 1], s);
        }

        private void H(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + BitHelper.RotateLeft(a + (b ^ c ^ d) + X[k] + T[i - 1], s);
        }

        private void I(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + BitHelper.RotateLeft(a + (c ^ (b | ~d)) + X[k] + T[i - 1], s);
        }

        #endregion Transform

    }
}