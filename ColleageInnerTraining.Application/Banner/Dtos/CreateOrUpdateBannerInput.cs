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
    /// 轮播图新增和编辑时用Dto
    /// </summary>

    public class CreateOrUpdateBannerInput
    {
        /// <summary>
        /// 轮播图编辑Dto
        /// </summary>
        public BannerEditDto BannerEditDto { get; set; }

    }
}
