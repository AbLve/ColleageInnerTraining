using Abp.Runtime.Validation;
using ColleageInnerTraining.Dto;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 班级成员查询Dto
    /// </summary>
    public class GetBannerInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数
        /// <summary>
        ///id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 图片id
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        /// 班级id
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 项目类型（1-pc、2-wap）
        /// </summary>
        public int ClientType { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 用于排序的默认值
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Sort";
            }
        }
    }
}
