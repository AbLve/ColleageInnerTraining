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
    /// 班级新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateClassTrainingInfoInput
    {
    /// <summary>
    /// 班级编辑Dto
    /// </summary>
		public ClassTrainingInfoEditDto ClassTrainingInfoEditDto { get;set;}
 
    }
}
