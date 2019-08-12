using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Utils.Core.Extensions;

namespace Tm.Module.CodeGenerator.Domain.Property
{
    public partial class PropertyEntity
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Ignore]
        public string TypeName
        {
            get
            {
                var name = Type.ToDescription();
                if (Type == PropertyType.String)
                {
                    name = $"{name}({Length})";
                }
                else if (Type == PropertyType.Decimal)
                {
                    name = $"{name}({Precision},{Scale})";
                }
                else if(Type == PropertyType.Double)
                {
                    name = $"{name}({Precision})";
                }
                else if (Type == PropertyType.Enum)
                {
                    name = $"{name}({EnumName})";
                }
                return name;
            }
        }

        /// <summary>
        /// 枚举名称
        /// </summary>
        [Ignore]
        public string EnumName { get; set; }
    }
}
