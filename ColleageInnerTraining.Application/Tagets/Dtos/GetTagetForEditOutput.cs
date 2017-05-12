using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Extensions;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
	/// <summary>
    /// 用于添加或编辑时使用的DTO
    /// </summary>
  
    public class GetTagetForEditOutput
    {
 

	      /// <summary>
         /// 编辑状态的DTO
        /// </summary>
    public TagetEditDto Taget { get;set;}


    }
}
