using System;
using System.ComponentModel.DataAnnotations;

namespace NetModular.Lib.Utils.Core.Validations
{
    /// <summary>
    /// GUID不能为空
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class NotEmptyAttribute : ValidationAttribute
    {
        public const string DefaultErrorMessage = "字段 {0} 不能为空";
        public NotEmptyAttribute() : base(DefaultErrorMessage) { }

        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return true;
            }

            switch (value)
            {
                case Guid guid:
                    return guid != Guid.Empty;
                default:
                    return true;
            }
        }
    }
}
