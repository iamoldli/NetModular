using System.ComponentModel;

namespace NetModular.Module.CodeGenerator.Domain.Class
{
    /// <summary>
    /// 基类实体类型
    /// </summary>
    public enum BaseEntityType
    {
        [Description("IEntity")]
        Normal,
        [Description("Entity<Int>")]
        EntityInt,
        [Description("Entity<Long>")]
        EntityLong,
        [Description("Entity<Guid>")]
        EntityGuid,
        [Description("EntityBase<Int>")]
        EntityBaseInt,
        [Description("EntityBase<Long>")]
        EntityBaseLong,
        [Description("EntityBase<Guid>")]
        EntityBaseGuid,
        [Description("EntityWithSoftDelete<Int>")]
        EntityWithSoftDeleteInt,
        [Description("EntityWithSoftDelete<Long>")]
        EntityWithSoftDeleteLong,
        [Description("EntityWithSoftDelete<Guid>")]
        EntityWithSoftDeleteGuid,
        [Description("EntityBaseWithSoftDelete<Int>")]
        EntityBaseWithSoftDeleteInt,
        [Description("EntityBaseWithSoftDelete<Long>")]
        EntityBaseWithSoftDeleteLong,
        [Description("EntityBaseWithSoftDelete<Guid>")]
        EntityBaseWithSoftDeleteGuid
    }
}
