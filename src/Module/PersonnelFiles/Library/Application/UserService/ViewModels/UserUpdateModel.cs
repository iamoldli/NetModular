using System;
using System.Collections.Generic;
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
        /// 所属部门
        /// </summary>
        [Required(ErrorMessage = "请选择部门")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 岗位编号
        /// </summary>
        [Required(ErrorMessage = "请选择岗位")]
        public Guid PositionId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "请输入姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string Native { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 照片关联的附件编号
        /// </summary>
        public Guid? Picture { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 绑定角色列表
        /// </summary>
        public List<Guid> Roles { get; set; }

        /// <summary>
        /// 部门完整路径
        /// </summary>
        public string DeptFullPath { get; set; }
    }
}
