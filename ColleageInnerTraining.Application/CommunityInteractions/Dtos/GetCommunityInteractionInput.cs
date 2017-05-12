using Abp.Runtime.Validation;
using ColleageInnerTraining.Dto;
using System;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 互动社区查询Dto
    /// </summary>
    public class GetCommunityInteractionInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

        /// <summary>
        /// 是什么数据
        /// </summary>
        public int IdType { get; set; }

        /// <summary>
        /// 模糊查询参数用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 模糊查询参数问题
        /// </summary>
        public string Quest { get; set; }

        /// <summary>
        /// 查询参数开始时间
        /// </summary>
        public DateTime? BTIme { get; set; }

        /// <summary>
        /// 查询参数结束时间
        /// </summary>
        public DateTime? ETime { get; set; }

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
