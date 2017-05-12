using Abp.Runtime.Validation;
using ColleageInnerTraining.Dto;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 班级成员查询Dto
    /// </summary>
    public class GetClassTrainingInfoInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数
        /// <summary>
        ///id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 班级id
        /// </summary>
        public int CId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 项目类型（1-课程、2-考试、3-问卷、4-线下培训）
        /// </summary>
        public int TrainingType { get; set; }

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
