using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Extensions;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
	/// <summary>
    /// 用于添加或编辑 岗位时使用的DTO
    /// </summary>
  
    public class GetJobPostForEditOutput
    {


        /// <summary>
        /// JobPost编辑状态的DTO
        /// </summary>
        public JobPostEditDto JobPost { get;set;}


    }
}
