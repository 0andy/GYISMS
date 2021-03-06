

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using GYISMS.VisitRecords;

namespace  GYISMS.VisitRecords.Dtos
{
    public class VisitRecordEditDto
    {
/// <summary>
/// Id
/// </summary>
public Guid? Id { get; set; }


/// <summary>
/// ScheduleDetailId
/// </summary>
[Required(ErrorMessage="ScheduleDetailId不能为空")]
public Guid ScheduleDetailId { get; set; }


/// <summary>
/// EmployeeId
/// </summary>
public long? EmployeeId { get; set; }


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






		//// custom codes
 
        //// custom codes end
    }
}