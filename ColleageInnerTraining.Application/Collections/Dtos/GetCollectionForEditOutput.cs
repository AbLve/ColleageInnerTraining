using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Extensions;
using ColleageInnerTraining.Application;

namespace ColleageInnerTraining.Application.Dtos
{
	/// <summary>
    /// 用于添加或编辑 收藏时使用的DTO
    /// </summary>
  
    public class GetCollectionForEditOutput 
    {
 

	      /// <summary>
         /// Collection编辑状态的DTO
        /// </summary>
    public CollectionEditDto Collection{get;set;}


    }
}
