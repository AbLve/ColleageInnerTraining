using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Common;
using ColleageInnerTraining.Web.Areas.Wap.Models;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Wap.Controllers
{
    public class MemberController : Controller
    {
        private ICourseBoundPersonnelAppService courseBoundService;
        /// <summary>
        /// 课程服务对像
        /// </summary>
        private ICourseInfoAppService courseService;
        /// <summary>
        /// 收藏服务对像
        /// </summary>
        private ICollectionAppService collectionAppService;

        /// <summary>
        /// 班级人员服务
        /// </summary>
        private IClassUserAppService classuserService;
        /// <summary>
        /// 课程绑定班级对象
        /// </summary>
        private ICourseBoundConfigureTypeAppService cbtService;
        /// <summary>
        /// 课程关联关系注入对像
        /// </summary>
        ICourseBoundConfigureTypeAppService _courseBoundConfigureTypeAppService;

        /// <summary>
        /// 班级业务对像
        /// </summary>
        IClassesInfoAppService _classesInfoAppService;

        public MemberController(ICourseBoundPersonnelAppService _service, ICourseInfoAppService _courseService,
            ICollectionAppService _collectionAppService, IClassUserAppService _classuserService,
            ICourseBoundConfigureTypeAppService _cbtService, ICourseBoundConfigureTypeAppService courseBoundConfigureTypeAppService
            , IClassesInfoAppService classesInfoAppService)
        {
            courseBoundService = _service;
            courseService = _courseService;
            collectionAppService = _collectionAppService;

            classuserService = _classuserService;
            cbtService = _cbtService;
            _courseBoundConfigureTypeAppService = courseBoundConfigureTypeAppService;
            _classesInfoAppService = classesInfoAppService;



        }
        // GET: Wap/Member
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Wap/Member/CheckIn")]
        public ActionResult CheckIn(int CourseId)
        {
            int uId = int.Parse(CookieHelper.GetCookieValue("UserId").ToString());
            string UserName = CookieHelper.GetCookieValue("DisplayName").ToString();
            var dto = courseBoundService.GetCourseBoundUserByCourseId(CourseId).FirstOrDefault(t => t.AccountSysNo == uId).MapTo<CourseBoundPersonnelEditDto>();
            ViewBag.Stauts = 0;
            if (dto != null)
            {
                if (dto.CheckIN == true)
                {
                    ViewBag.Stauts = 1;
                }
                else
                {
                    dto.CheckIN = true;
                    courseBoundService.UpdateCourseBoundPersonnel(dto);
                    //更新课程表签到人数
                    var cdto = courseService.GetCourseInfoEditById(new EntityDto<long> { Id = dto.CourseId });
                    cdto.CheckinNum++;
                    courseService.UpdateCourseInfo(cdto);
                    ViewBag.Stauts = 2;
                }
            }
            else//不是已绑定到课程的人员
            {
                courseBoundService.CreateCourseBoundPersonnel(new CourseBoundPersonnelEditDto { CourseId = CourseId, AccountSysNo = uId, CheckIN = true, IsBound = false,AccountUserName = UserName });                
                ViewBag.Stauts = 2;
            }
            //查到线下培训课程所对就的班级
            var configu = _courseBoundConfigureTypeAppService.GetCTypeByCIdOrByType(CourseId, (int)ConfigureType.Class);
            if (configu != null)
            {
                //班级人员关联关系数据添加
                var classUser = new ClassUserEditDto();
                classUser.ClassId = configu.BusinessId;
                classUser.UserId = uId;
                classuserService.CreateClassUser(classUser);
                //把人员添加到班级中
                var classInfo = _classesInfoAppService.GetClassesInfoForEdit(new NullableIdDto<long>() { Id = configu.BusinessId });
                classInfo.ClassesInfo.MemberCount++;
                _classesInfoAppService.UpdateClassesInfoAsync(classInfo.ClassesInfo);
            }            
            return View();
        }

        [HttpGet]
        [Route("/Wap/Member/Collection")]
        public ActionResult Collection()
        {
            return View();
        }

        [HttpGet]
        [Route("/Wap/Member/CollectionList")]
        public JsonResult CollectionList(GetCollectionInput input)
        {
            var collectionListDtos = collectionAppService.GetPagedCollections(input);
            return Json(collectionListDtos, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Route("/Wap/Member/Training")]
        public ActionResult Training()
        {
            return View();
        }

        [HttpGet]
        [Route("/Wap/Member/TrainingList")]
        public JsonResult TrainingList(int status)
        {

            int userId = int.Parse(CookieHelper.GetCookieValue("UserId").ToString());
            var courseInfoListDtos = courseService.GetCourseInfoByUserId(userId, (int)CourseType.Line, status);
            return Json(courseInfoListDtos, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Route("/Wap/Member/Courseing")]
        public ActionResult Courseing()
        {
            return View();
        }

        [HttpGet]
        [Route("/Wap/Member/CourseingList")]
        public JsonResult CourseingList(int status)
        {

            int userId = int.Parse(CookieHelper.GetCookieValue("UserId").ToString());
            var courseInfoListDtos = courseService.GetCourseInfoByUserId(userId, 0, status);
            return Json(courseInfoListDtos, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Route("/Wap/Member/UserInfo")]
        public ActionResult UserInfo()
        {
            return View();
        }


        [HttpGet]
        [Route("/Wap/Member/UserPwd")]
        public ActionResult UserPwd()
        {
            ChangeViewModel viewModel = new ChangeViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UserPwd(ChangeViewModel viewModel)
        {
            string userName = CookieHelper.GetCookieValue("UserName").ToString();
            if (AuthorizeManager.ChangePassword(userName, viewModel.OldPassword, viewModel.Password))
            {
                viewModel.IsSuccess = true;
                viewModel.SuccessMessage = "密码修改成功！";
            }
            else
            {
                viewModel.IsSuccess = false;
                viewModel.SuccessMessage = "密码修改失败,请重试！";
            }
            return View(viewModel);

        }
    }
}