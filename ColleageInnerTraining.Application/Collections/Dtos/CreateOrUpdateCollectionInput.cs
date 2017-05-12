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
    /// 收藏新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateCollectionInput  
    {
    /// <summary>
    /// 收藏编辑Dto
    /// </summary>
		public CollectionEditDto  CollectionEditDto {get;set;}
 
    }
}
