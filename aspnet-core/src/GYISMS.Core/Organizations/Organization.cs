﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GYISMS.Organizations
{
    /// <summary>
    /// 组织架构
    /// </summary>
    [Table("Organizations")]
    public class Organization : Entity<long>
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public virtual string DepartmentName { get; set; }

        /// <summary>
        /// 父部门id
        /// </summary>
        public virtual long? ParentId { get; set; }

        /// <summary>
        /// 父部门中的次序值
        /// </summary>
        public virtual long? Order { get; set; }

        /// <summary>
        /// 是否隐藏部门

        /// </summary>
        public virtual bool? DeptHiding { get; set; }

        /// <summary>
        /// 企业群群主
        /// </summary>
        [StringLength(100)]
        public virtual string OrgDeptOwner { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreationTime { get; set; }
    }
}