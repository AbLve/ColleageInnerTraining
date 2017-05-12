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
    /// 菜单新增和编辑时用Dto
    /// </summary>

    public class CreateOrUpdateMenuInput
    {
        /// <summary>
        /// 菜单编辑Dto
        /// </summary>
        public MenuEditDto MenuEditDto { get; set; }

    }
}
