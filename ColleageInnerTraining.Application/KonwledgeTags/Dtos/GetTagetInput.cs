using Abp.Runtime.Validation;
using ColleageInnerTraining.Dto;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 班级信息查询Dto
    /// </summary>
    public class GetKonwledgeTagInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 知识库id
		/// </summary>
		public int KId { get; set; }
        /// <summary>
	    /// 标签id
		/// </summary>
		public int TId { get; set; }

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
