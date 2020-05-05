using System;
using System.IO;

namespace Qiniu.Util
{
    /// <summary>
    /// QINIU ETAG(文件hash)
    /// </summary>
    public class ETag
    {
        // 块大小(固定为4MB)
        private const int BLOCK_SIZE = 4 * 1024 * 1024;

        // 计算时以20B为单位
        private static int BLOCK_SHA1_SIZE = 20;        

        /// <summary>
        /// 计算文件hash(ETAG)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>文件hash</returns>
        public static string CalcHash(string filePath)
        {
            string qetag = "";

            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    long fileLength = stream.Length;
                    byte[] buffer = new byte[BLOCK_SIZE];
                    byte[] finalBuffer = new byte[BLOCK_SHA1_SIZE + 1];
                    if (fileLength <= BLOCK_SIZE)
                    {
                        int readByteCount = stream.Read(buffer, 0, BLOCK_SIZE);
                        byte[] readBuffer = new byte[readByteCount];
                        Array.Copy(buffer, readBuffer, readByteCount);

                        byte[] sha1Buffer = Hashing.CalcSHA1(readBuffer);

                        finalBuffer[0] = 0x16;
                        Array.Copy(sha1Buffer, 0, finalBuffer, 1, sha1Buffer.Length);
                    }
                    else
                    {
                        long blockCount = (fileLength % BLOCK_SIZE == 0) ? (fileLength / BLOCK_SIZE) : (fileLength / BLOCK_SIZE + 1);
                        byte[] sha1AllBuffer = new byte[BLOCK_SHA1_SIZE * blockCount];

                        for (int i = 0; i < blockCount; i++)
                        {
                            int readByteCount = stream.Read(buffer, 0, BLOCK_SIZE);
                            byte[] readBuffer = new byte[readByteCount];
                            Array.Copy(buffer, readBuffer, readByteCount);

                            byte[] sha1Buffer = Hashing.CalcSHA1(readBuffer);
                            Array.Copy(sha1Buffer, 0, sha1AllBuffer, i * BLOCK_SHA1_SIZE, sha1Buffer.Length);
                        }

                        byte[] sha1AllBufferSha1 = Hashing.CalcSHA1(sha1AllBuffer);

                        finalBuffer[0] = 0x96;
                        Array.Copy(sha1AllBufferSha1, 0, finalBuffer, 1, sha1AllBufferSha1.Length);

                    }
                    qetag = Base64.UrlSafeBase64Encode(finalBuffer);
                }
            }
            catch (Exception) { }

            return qetag;
        }
    }
}