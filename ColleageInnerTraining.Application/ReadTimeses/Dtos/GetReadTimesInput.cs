﻿using System;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Dto;

namespace ColleageInnerTraining.Application.Dtos
{
	/// <summary>
    /// 阅读人数查询Dto
    /// </summary>
    public class GetReadTimesInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 模糊查询参数
		/// </summary>
		public string FilterText { get; set; }

		/// <summary>
	    /// 用于排序的默认值
		/// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
			
		
                Sorting = "Id";
            }
        }
    }
}
