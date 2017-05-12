// 项目展示地址:"http://www.ddxc.org/"
 // 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-04-24T17:14:28. All Rights Reserved.
//<生成时间>2017-04-24T17:14:28</生成时间>
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 所属类型配置业务管理
    /// </summary>
    public class CourseBoundConfigureTypeManage : IDomainService
    {
        private readonly IRepository<CourseBoundConfigureType,long> _courseBoundConfigureTypeRepository;

         /// <summary>
        /// 构造方法
        /// </summary>
        public CourseBoundConfigureTypeManage(IRepository<CourseBoundConfigureType,long> courseBoundConfigureTypeRepository  )
        {
            _courseBoundConfigureTypeRepository = courseBoundConfigureTypeRepository;
        }

		//TODO:编写领域业务代码


		/// <summary>
        ///     初始化
        /// </summary>
        private void Init()
        {


 

        }


		}

    
	
}
