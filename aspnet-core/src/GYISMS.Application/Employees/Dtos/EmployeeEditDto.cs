

using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using GYISMS.Employees;
using GYISMS.GYEnums;

namespace GYISMS.Employees.Dtos
{
    [AutoMapTo(typeof(Employee))]
    public class EmployeeEditDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }


        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Mobile
        /// </summary>
        public string Mobile { get; set; }


        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Active
        /// </summary>
        public bool? Active { get; set; }


        /// <summary>
        /// IsAdmin
        /// </summary>
        public bool? IsAdmin { get; set; }


        /// <summary>
        /// IsBoss
        /// </summary>
        public bool? IsBoss { get; set; }


        /// <summary>
        /// Department
        /// </summary>
        public string Department { get; set; }
        public string DepartmentName { get; set; }


        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }


        /// <summary>
        /// Avatar
        /// </summary>
        public string Avatar { get; set; }


        /// <summary>
        /// HiredDate
        /// </summary>
        public string HiredDate { get; set; }


        /// <summary>
        /// Roles
        /// </summary>
        public string Roles { get; set; }


        /// <summary>
        /// RoleId
        /// </summary>
        public long? RoleId { get; set; }


        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }




        /// <summary>
        /// ����Code
        /// </summary>
        public AreaCodeEnum? AreaCode { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string Area { get; set; }

        public bool IsDeptArea { get; set; }

        //// custom codes

        //// custom codes end
    }
}