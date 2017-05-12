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
    /// 公告新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateNoticeInput
    {
    /// <summary>
    /// 公告编辑Dto
    /// </summary>
		public NoticeEditDto NoticeEditDto { get;set;}
 
    }
}
