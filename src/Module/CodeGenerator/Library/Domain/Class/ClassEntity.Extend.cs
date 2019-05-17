using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Utils.Core.Extensions;

namespace Nm.Module.CodeGenerator.Domain.Class
{
    public partial class ClassEntity
    {
        /// <summary>
        /// 基类名称
        /// </summary>
        [Ignore]
        public string BaseEntityName => BaseEntityType.ToDescription();
    }
}
