namespace Qiniu.Storage
{
    /// <summary>
    /// 分片大小
    /// </summary>
    public enum ChunkUnit
    {
        /// <summary>
        /// 128KB
        /// </summary>
        U128K = 1,

        /// <summary>
        /// 256KB
        /// </summary>
        U256K = 2,

        /// <summary>
        /// 512KB
        /// </summary>
        U512K = 4,

        /// <summary>
        /// 1MB
        /// </summary>
        U1024K = 8,

        /// <summary>
        /// 2MB
        /// </summary>
        U2048K = 16,

        /// <summary>
        /// 4MB
        /// </summary>
        U4096K = 32
    };

    /// <summary>
    /// ChunkSize转换
    /// </summary>
    public class ResumeChunk
    {
        private static int N = 128 * 1024;

        /// <summary>
        /// 计算ChunkSize
        /// </summary>
        /// <param name="cu"></param>
        /// <returns></returns>
        public static int GetChunkSize(ChunkUnit cu)
        {
            int c = (int)cu;
            return c * N;
        }

        /// <summary>
        /// 计算ChunkUnit
        /// </summary>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static ChunkUnit GetChunkUnit(int chunkSize)
        {
            if (chunkSize < 128 * 1024 || chunkSize > 4 * 1024 * 1024)
            {
                return ChunkUnit.U2048K;
            }
            else
            {
                int u = chunkSize / N;
                int cu;

                if (u == 1)
                {
                    cu = 1;
                }
                else if (u < 4)
                {
                    cu = 2;
                }
                else if (u < 8)
                {
                    cu = 4;
                }
                else if (u < 16)
                {
                    cu = 8;
                }
                else if (u < 32)
                {
                    cu = 16;
                }
                else
                {
                    cu = 32;
                }

                return (ChunkUnit)cu;
            }
        }
    }
}
