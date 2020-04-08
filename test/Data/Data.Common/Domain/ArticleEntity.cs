using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;

namespace Data.Common.Domain
{
    public class ArticleEntity : EntityBaseWithSoftDelete<int>
    {
        public Guid CategoryId { get; set; }

        [Length(200)]
        public string Title { get; set; }

        public MediaType MediaType { get; set; }

        [Max]
        public string Body { get; set; }

        public bool Published { get; set; }

        public int ReadCount { get; set; }

        [Ignore]
        public string CategoryName { get; set; }
    }

    public enum MediaType
    {
        Picture,
        Video
    }
}
