using System.ComponentModel;

namespace Nm.Module.CodeGenerator.Domain.Class
{
    /// <summary>
    /// 基类实体类型
    /// </summary>
    public enum BaseEntityType
    {
        [Description("IEntity")]
        Normal,
        [Description("Entity<int>")]
        EntityInt,
        [Description("Entity<long>")]
        EntityLong,
        [Description("Entity<Guid>")]
        EntityGuid,
        [Description("EntityBase<int>")]
        EntityBaseInt,
        [Description("EntityBase<long>")]
        EntityBaseLong,
        [Description("EntityBase<Guid>")]
        EntityBaseGuid,
        [Description("EntityWithSoftDelete<int>")]
        EntityWithSoftDeleteInt,
        [Description("EntityWithSoftDelete<long>")]
        EntityWithSoftDeleteLong,
        [Description("EntityWithSoftDelete<Guid>")]
        EntityWithSoftDeleteGuid,
        [Description("EntityBaseWithSoftDelete<int>")]
        EntityBaseWithSoftDeleteInt,
        [Description("EntityBaseWithSoftDelete<long>")]
        EntityBaseWithSoftDeleteLong,
        [Description("EntityBaseWithSoftDelete<Guid>")]
        EntityBaseWithSoftDeleteGuid
    }
}
