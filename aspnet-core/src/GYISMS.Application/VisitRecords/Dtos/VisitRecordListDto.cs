

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using GYISMS.VisitRecords;
using Abp.AutoMapper;
using GYISMS.TaskExamines;

namespace GYISMS.VisitRecords.Dtos
{
    public class VisitRecordListDto : EntityDto<Guid>
    {

        /// <summary>
        /// ScheduleDetailId
        /// </summary>
        [Required(ErrorMessage = "ScheduleDetailId不能为空")]
        public Guid ScheduleDetailId { get; set; }


        /// <summary>
        /// EmployeeId
        /// </summary>
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        /// <summary>
        /// GrowerId
        /// </summary>
        public int? GrowerId { get; set; }


        /// <summary>
        /// SignTime
        /// </summary>
        public DateTime? SignTime { get; set; }


        /// <summary>
        /// Location
        /// </summary>
        public string Location { get; set; }


        /// <summary>
        /// Longitude
        /// </summary>
        public decimal? Longitude { get; set; }


        /// <summary>
        /// Latitude
        /// </summary>
        public decimal? Latitude { get; set; }


        /// <summary>
        /// Desc
        /// </summary>
        public string Desc { get; set; }


        /// <summary>
        /// ImgPath
        /// </summary>
        public string ImgPath { get; set; }


        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime? CreationTime { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string ExaminesName { get; set; }
        public bool? HasExamine { get; set; }
    }


    [AutoMapTo(typeof(VisitRecord))]
    public class DingDingVisitRecordInputDto
    {
        public Guid ScheduleDetailId { get; set; }

        public string Location { get; set; }

        public decimal? Longitude { get; set; }


        public decimal? Latitude { get; set; }

        public string Desc { get; set; }

        public string ImgPath { get; set; }

        public int? GrowerId { get; set; }

        public string GrowerName { get; set; }

        public string EmployeeId { get; set; }

        public string TaskDesc { get; set; }

        
        public List<DingDingTaskExamineDto> Examines { get; set; }
    }

    [AutoMapFrom(typeof(TaskExamine))]
    public class DingDingTaskExamineDto : EntityDto
    {
        public string Name { get; set; }

        public string Desc { get; set; }

        public int Score { get; set; }
    }
}