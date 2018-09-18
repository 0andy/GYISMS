﻿using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYISMS.ScheduleDetails
{
    /// <summary>
    /// 任务明细
    /// </summary>
    [Table("ScheduleDetails")]
    public class ScheduleDetail : Entity<Guid>
    {
        /// <summary>
        /// 任务Id 外键
        /// </summary>
        [Required]
        public virtual int TaskId { get; set; }

        /// <summary>
        /// 计划Id 外键
        /// </summary>
        [Required]
        public virtual Guid ScheduleId { get; set; }

        /// <summary>
        /// 烟技员Id外键
        /// </summary>
        [Required]
        public virtual string EmployeeId { get; set; }

        /// <summary>
        /// 烟农Id 外键
        /// </summary>
        [Required]
        public virtual int GrowerId { get; set; }

        /// <summary>
        /// 拜访次数
        /// </summary>
        public virtual int? VisitNum { get; set; }

        /// <summary>
        /// 完成次数
        /// </summary>
        public virtual int? CompleteNum { get; set; }

        /// <summary>
        /// CreationTime
        /// </summary>
        public virtual DateTime? CreationTime { get; set; }
    }
}