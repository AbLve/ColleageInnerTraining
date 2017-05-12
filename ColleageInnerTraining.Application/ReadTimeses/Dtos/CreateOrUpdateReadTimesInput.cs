using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using ColleageInnerTraining.Application;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 阅读人数新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateReadTimesInput  
    {
    /// <summary>
    /// 阅读人数编辑Dto
    /// </summary>
		public ReadTimesEditDto  ReadTimesEditDto {get;set;}
 
    }
}
