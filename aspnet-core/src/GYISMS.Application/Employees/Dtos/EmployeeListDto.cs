

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using GYISMS.Employees;
using GYISMS.GYEnums;
using Abp.AutoMapper;
using System.Text.RegularExpressions;

namespace GYISMS.Employees.Dtos
{
    [AutoMapFrom(typeof(Employee))]
    public class EmployeeListDto : EntityDto<string>
    {

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
        /// 区县Code
        /// </summary>
        public AreaCodeEnum? AreaCode { get; set; }

        /// <summary>
        /// 区县名称
        /// </summary>
        public string Area { get; set; }

        public bool IsDeptArea { get; set; }

        public AreaCodeEnum? DeptAreaCode { get; set; }

        public string DeptArea { get; set; }


        /// <summary>
        /// 资料库角色
        /// </summary>
        public string DocRole { get; set; }
    }
    public class NzTreeNode
    {
        public virtual string title { get; set; }
        public virtual string key { get; set; }
        public virtual bool IsLeaf { get; set; }

        public virtual bool selected { get; set; }

        public virtual List<NzTreeNode> children { get; set; }
    }

    public class EmployeeNzTreeNode : NzTreeNode
    {

        public new List<EmployeeNzTreeNode> children { get; set; }
        //// custom codes
        public bool IsDept { get; set; }
        //// custom codes end
    }

    public class DocNzTreeNode : NzTreeNode
    {
        public override bool IsLeaf
        {
            get
            {
                if (children.Count == 0)
                {
                    return true;
                }
                return false;
            }
        }
        public new List<DocNzTreeNode> children { get; set; }
    }

    public class DingDingUserDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Avatar { get; set; }

        public string Area { get; set; }

        public AreaCodeEnum AreaCode { get; set; }
    }

    [AutoMapFrom(typeof(Employee))]
    public class APPEmployeeListDto : EntityDto<string>
    {

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Department
        /// </summary>
        public string Department { get; set; }


        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 区县Code
        /// </summary>
        public AreaCodeEnum? AreaCode { get; set; }

        /// <summary>
        /// 区县名称
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 离线APP登录密码
        /// </summary>
        public string Pwd
        {
            get
            {
                string result = Regex.Replace(Id, @"[^0-9]+", "");
                var pwd = Convert.ToUInt64(result) * 92794;
                while (pwd.ToString().Length < 8)
                {
                    pwd = pwd * 92794;
                }
                return pwd.ToString().Substring(2, 8);
                //var pwd = Math.Abs(Id.GetHashCode()) * 92794;
                //while (pwd.ToString().Length < 8)
                //{
                //    pwd = pwd * 92794;
                //}
                //return pwd.ToString().Substring(2, 8);

            }
        }
    }
}