using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 编辑用Dto
    /// </summary>
    [AutoMap(typeof(KnowledgeDepJob))]
    public class KnowledgeDepJobEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 知识点Id
        /// </summary>
        public int KnoledgeId { get; set; }

        /// <summary>
        /// 知识点标题
        /// </summary>
        public string KnoledgeTitle { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 业务Id
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        [MaxLength(255)]
        public string BusinessName { get; set; }

    }
}
