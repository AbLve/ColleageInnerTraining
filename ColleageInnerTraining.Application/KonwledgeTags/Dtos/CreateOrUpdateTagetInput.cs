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
    /// 新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateKonwledgeTagInput
    {
    /// <summary>
    /// 标签编辑Dto
    /// </summary>
		public KonwledgeTagEditDto KonwledgeTagEditDto { get;set;}
 
    }
}
