using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Utils.Core.Extensions;

namespace Tm.Module.CodeGenerator.Domain.Class
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
