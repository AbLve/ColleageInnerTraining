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
    /// 岗位新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateJobPostInput
    {
    /// <summary>
    /// 岗位编辑Dto
    /// </summary>
		public JobPostEditDto JobPostEditDto { get;set;}
 
    }
}
