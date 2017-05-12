using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Controllers;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Common;
using ColleageInnerTraining.Web.Areas.Wap.Models;
using ColleageInnerTraining.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Wap.Controllers
{
    public class CourseController : AbpController
    {
        #region 注入服务

        /// <summary>
        /// 课程信息注入对像
        /// </summary>
        ICourseInfoAppService _courseInfoAppService;
        /// <summary>
        /// 课程分类注入对像
        /// </summary>
        ICourseCategoryAppService _courseCategoryAppService;
        /// <summary>
        /// 内训师注入对像
        /// </summary>
        ITeachersAppService _teachersAppService;

        /// <summary>
        /// 课程关联关系注入对像
        /// </summary>
        ICourseBoundConfigureTypeAppService _courseBoundConfigureTypeAppService;
        /// <summary>
        /// 课程关联人员注入对像
        /// </summary>
        ICourseBoundPersonnelAppService _courseBoundPersonnelAppService;
        /// <summary>
        /// 班级关联人员注入对像
        /// </summary>
        IClassUserAppService _classUserAppService;
        /// <summary>
        /// 班级业务对像
        /// </summary>
        IClassesInfoAppService _classesInfoAppService;



        /// <summary>
        /// 用户注入对像
        /// </summary>
        IUserAccountAppService _userAccountService;

        public CourseController(ICourseInfoAppService courseInfoAppService, ICourseCategoryAppService courseCategoryAppService,
            ITeachersAppService teachersAppService, ICourseBoundConfigureTypeAppService courseBoundConfigureTypeAppService,
            ICourseBoundPersonnelAppService courseBoundPersonnelAppService, IUserAccountAppService userAccountService,
            IClassUserAppService classUserAppService, IClassesInfoAppService classesInfoAppService)
        {
            _courseInfoAppService = courseInfoAppService;
            _courseCategoryAppService = courseCategoryAppService;
            _teachersAppService = teachersAppService;
            _courseBoundConfigureTypeAppService = courseBoundConfigureTypeAppService;
            _courseBoundPersonnelAppService = courseBoundPersonnelAppService;
            _userAccountService = userAccountService;
            _classUserAppService = classUserAppService;
            _classesInfoAppService = classesInfoAppService;
        }
        #endregion

        // GET: Wap/Home
        [HttpGet]
        public ActionResult Index()
        {
            CourseViewModel vm = new CourseViewModel();
            vm.CourseCategorys = _courseCategoryAppService.GetTopCourseCategory();
            foreach (CourseCategoryListDto c in vm.CourseCategorys)
            {
                c.CourseCategorys = _courseCategoryAppService.GetCourseCategorysByParentId(c.CategoryId);
            }

            return View(vm);
        }

        // GET: Wap/Home
        [HttpGet]
        public ActionResult CourseDetail(int courseId)
        {
            CourseViewModel vm = new CourseViewModel();
            //课程
            CourseInfoListDto courseInfo = _courseInfoAppService.GetCourseInfoById(new EntityDto<long>() { Id = courseId });
            courseInfo.Content = System.Web.HttpUtility.HtmlDecode(courseInfo.Content);
            //内训师
            int teacherId = courseInfo.TeacherId;
            var teacher = _teachersAppService.GetTeachersById(new EntityDto<long>() { Id = teacherId });
            int userId = int.Parse(CookieHelper.GetCookieValue("UserId").ToString());

            //是否已报名
            var boundData = _courseBoundPersonnelAppService.GetCourseBoundByUserIdOrCourseId(userId, courseId);//绑定数据
            if (boundData != null)
                vm.IsSingleUp = true;
            else
                vm.IsSingleUp = false;
            if (courseInfo.Status == 4)
                vm.IsComplete = true;
            else
                vm.IsComplete = false;
            vm.teacher = teacher;
            vm.CourseInfo = courseInfo;
            vm.CourseId = courseId;
            return View(vm);
        }


        [HttpGet]
        public JsonResult GetCourseList(GetCourseInfoInput inpt)
        {
            var course = _courseInfoAppService.GetPagedCourseInfoForWap(inpt);            
            return Json(course, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SingleUpCourse(int courseId)
        {
            //课程对像
            var courseDate = _courseInfoAppService.GetCourseInfoById(new EntityDto<long>() { Id = courseId });//课程数据
            if (courseDate != null)
            {

                int userId = int.Parse(CookieHelper.GetCookieValue("UserId").ToString());
                var user = _userAccountService.GetUserAccountBySysNo(userId);//查询用户数据
                var boundData = _courseBoundPersonnelAppService.GetCourseBoundByUserIdOrCourseId(user.SysNO, Convert.ToInt32(courseDate.Id));//绑定数据
                if (boundData == null)
                {
                    //增加人员
                    var create = new CourseBoundPersonnelEditDto();
                    create.AccountSysNo = user.SysNO;
                    create.AccountUserName = user.DisplayName;
                    create.CourseId = Convert.ToInt32(courseDate.Id);
                    create.CourseName = courseDate.CourseName;
                    _courseBoundPersonnelAppService.CreateCourseBoundPersonnel(create);

                }


                var configu = _courseBoundConfigureTypeAppService.GetCTypeByCIdOrType((int)courseDate.Id, (int)ConfigureType.Personal, user.SysNO);
                //增加关系
                if (configu == null)
                {
                    var createConfigure = new CourseBoundConfigureTypeEditDto();
                    createConfigure.CourseId = (int)courseDate.Id;
                    createConfigure.CourseName = courseDate.CourseName;
                    createConfigure.type = (int)ConfigureType.Personal;
                    createConfigure.BusinessId = user.SysNO;
                    createConfigure.BusinessName = user.DisplayName;
                    _courseBoundConfigureTypeAppService.CreateCourseBoundConfigureType(createConfigure);
                }
                //如果是线个考试则把人员配置到班级中
                if (courseDate.Type == 4)
                {
                    //查到线下培训课程所对就的班级
                    configu = _courseBoundConfigureTypeAppService.GetCTypeByCIdOrByType((int)courseDate.Id, (int)ConfigureType.Class);
                    if (configu != null)
                    {
                        //班级人员关联关系数据添加
                        var classUser = new ClassUserEditDto();                        
                        classUser.ClassId = configu.BusinessId;
                        classUser.UserId = userId;
                        _classUserAppService.CreateClassUser(classUser);
                        //把人员添加到班级中
                        var classInfo = _classesInfoAppService.GetClassesInfoForEdit(new NullableIdDto<long>() { Id = configu.BusinessId });
                        classInfo.ClassesInfo.MemberCount++;
                        _classesInfoAppService.UpdateClassesInfoAsync(classInfo.ClassesInfo);
                    }
                }
            }
            return Json(courseDate, JsonRequestBehavior.AllowGet);
        }


        


    }
}