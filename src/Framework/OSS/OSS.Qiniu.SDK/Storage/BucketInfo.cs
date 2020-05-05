namespace Qiniu.Storage
{
    /// <summary>
    /// bucket info
    /// </summary>
    public class BucketInfo
    {
        /// <summary>
        /// bucket name
        /// </summary>
        public string tbl { get; set; }

        /// <summary>
        /// itbl
        /// </summary>
        public long itbl { get; set; }

        /// <summary>
        /// deprecated
        /// </summary>
        public string phy {get;set;}

        /// <summary>
        /// id
        /// </summary>
        public long uid { get; set; }

        /// <summary>
        /// zone
        /// </summary>
        public string zone { get; set; }

        /// <summary>
        /// region
        /// </summary>
        public string region { get; set; }

        /// <summary>
        /// isGlobal
        /// </summary>
        public bool global { get; set; }

        /// <summary>
        /// isLineStorage
        /// </summary>
        public bool line { get; set; }

        /// <summary>
        /// creationTime
        /// </summary>
        public long ctime { get; set; }
    }
}
