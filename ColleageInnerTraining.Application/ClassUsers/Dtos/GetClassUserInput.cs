using Abp.Runtime.Validation;
using ColleageInnerTraining.Dto;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 班级成员查询Dto
    /// </summary>
    public class GetClassUserInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 班级id
		/// </summary>
		public int CId { get; set; }

        /// <summary>
        /// 成员id
        /// </summary>
        public int UId { get; set; }

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
