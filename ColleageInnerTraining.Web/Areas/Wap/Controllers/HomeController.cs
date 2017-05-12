using Abp.Web.Mvc.Controllers;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Common;
using ColleageInnerTraining.Web.Areas.Wap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Wap.Controllers
{
    public class HomeController : AbpController
    {
        /// <summary>
        /// 轮播图
        /// </summary>
        public IBannerAppService _bannerAppService;
        /// <summary>
        /// 课程
        /// </summary>
        public ICourseInfoAppService _courseInfoAppService;
        /// <summary>
        /// 课程分类
        /// </summary>
        public ICourseCategoryAppService _courseCategoryAppService;
        public HomeController(IBannerAppService bannerAppService, ICourseInfoAppService courseInfoAppService, ICourseCategoryAppService courseCategoryAppService)
        {
            _bannerAppService = bannerAppService;
            _courseInfoAppService = courseInfoAppService;
            _courseCategoryAppService = courseCategoryAppService;
        }

        // GET: Wap/Home
        public ActionResult Index()
        {
            HomeViewModel wm = new HomeViewModel();
            wm.banners = _bannerAppService.GetBannerByClientType((int)Display.MPhone);
            wm.course3news = _courseInfoAppService.GetCourseInfo3News((int)Display.MPhone);
            List<CourseCategoryListDto> topCategorys = _courseCategoryAppService.GetCourseTopCategoryList();
            foreach (var category in topCategorys)
            {
                category.CourseInfoList = _courseInfoAppService.GetCourseInfoByCategory3News(category.CategoryId,(int)Display.MPhone);
            }
            wm.TopCategory = topCategorys;

            return View(wm);
        }


    }
}