using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 部门新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateDepartmentInfoInput  
    {
    /// <summary>
    /// 部门编辑Dto
    /// </summary>
		public DepartmentInfoEditDto  DepartmentInfoEditDto {get;set;}
 
    }
}
