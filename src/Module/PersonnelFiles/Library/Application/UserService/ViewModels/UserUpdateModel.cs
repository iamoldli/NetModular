using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.PersonnelFiles.Domain.User;

namespace Nm.Module.PersonnelFiles.Application.UserService.ViewModels
{
    /// <summary>
    /// 用户信息添加模型
    /// </summary>
    public class UserUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的用户信息")]
        public Guid Id { get; set; }

            /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

            /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

            /// <summary>
        /// 籍贯
        /// </summary>
        public string Native { get; set; }

            /// <summary>
        /// 所属部门
        /// </summary>
        public Guid DepartmentId { get; set; }

            /// <summary>
        /// 职位编号
        /// </summary>
        public int PositionId { get; set; }

            /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

            /// <summary>
        /// 工号
        /// </summary>
        public string Number { get; set; }

            /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

            /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }

            /// <summary>
        /// 照片
        /// </summary>
        public string Picture { get; set; }

    
    }
}
