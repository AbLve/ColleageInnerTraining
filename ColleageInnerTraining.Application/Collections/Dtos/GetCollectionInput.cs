using System;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Dto;

namespace ColleageInnerTraining.Application.Dtos
{
	/// <summary>
    /// 收藏查询Dto
    /// </summary>
    public class GetCollectionInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

        /// <summary>
        /// 模糊查询参数
        /// </summary>
        public string FilterText { get; set; }
        /// <summary>
        /// 收藏类型
        /// </summary>
        public string BizType { get; set; }

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
