

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using GYISMS.Schedules;
using GYISMS.GYEnums;
using Abp.AutoMapper;

namespace GYISMS.Schedules.Dtos
{
    [AutoMapFrom(typeof(Schedule))]
    public class ScheduleListDto : EntityDto<Guid>
    {

        /// <summary>
        /// Desc
        /// </summary>
        public string Desc { get; set; }


        /// <summary>
        /// Type
        /// </summary>
        [Required(ErrorMessage = "Type不能为空")]
        public ScheduleType Type { get; set; }
        public string TypeName
        {
            get
            {
                return Type.ToString();
            }
        }

        /// <summary>
        /// BeginTime
        /// </summary>
        public DateTime? BeginTime { get; set; }

        public string BeginTimeFormat
        {
            get
            {
                if (BeginTime.HasValue)
                {
                    return BeginTime.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }


        /// <summary>
        /// EndTime
        /// </summary>
        public DateTime? EndTime { get; set; }

        public string EndTimeFormat
        {
            get
            {
                if (EndTime.HasValue)
                {
                    return EndTime.Value.ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }


        /// <summary>
        /// Status
        /// </summary>
        public ScheduleMasterStatusEnum Status { get; set; }


        /// <summary>
        /// PublishTime
        /// </summary>
        public DateTime? PublishTime { get; set; }


        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool? IsDeleted { get; set; }


        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime? CreationTime { get; set; }


        /// <summary>
        /// CreatorUserId
        /// </summary>
        public long? CreatorUserId { get; set; }


        /// <summary>
        /// LastModificationTime
        /// </summary>
        public DateTime? LastModificationTime { get; set; }


        /// <summary>
        /// LastModifierUserId
        /// </summary>
        public long? LastModifierUserId { get; set; }


        /// <summary>
        /// DeletionTime
        /// </summary>
        public DateTime? DeletionTime { get; set; }


        /// <summary>
        /// DeleterUserId
        /// </summary>
        public long? DeleterUserId { get; set; }

        [Required(ErrorMessage = "Name不能为空")]
        public string Name { get; set; }
        public string CreateUserName { get; set; }
        public string Area { get; set; }

        public int? CompleteCount { get; set; }

        public int? VisitCount { get; set; }
        public int Percentage
        {
            get
            {
                if (VisitCount > 0)
                {
                    return (int)((CompleteCount.Value / ((decimal)VisitCount.Value)) * 100);
                }
                return 0;
            }
        }
    }
}