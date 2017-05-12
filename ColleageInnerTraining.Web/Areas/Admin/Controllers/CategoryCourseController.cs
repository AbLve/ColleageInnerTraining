using Abp.Application.Services.Dto;
using Abp.Web.Security.AntiForgery;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Controllers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    public class CategoryCourseController : BaseController
    {
        private ICourseCategoryAppService _corecategoryService;
        private ISqlExecuter _sqlExecuter;
        private ICourseInfoAppService _courseInfoService;

        public CategoryCourseController(ICourseCategoryAppService corecategoryService, ISqlExecuter sqlExecuter,
            ICourseInfoAppService courseInfoService)
        {
            _corecategoryService = corecategoryService;
            _sqlExecuter = sqlExecuter;
            _courseInfoService = courseInfoService;
        }
        
        // 课程分类
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.CategoryCourse.Index")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        public ActionResult GetdepartList()
        {
            var ss = _corecategoryService.GetPagedCourseCategorys(new GetCourseCategoryInput { SkipCount = 0, MaxResultCount = 1000 }).Items;
            var relist = ss.ToList();
            var datalist = new List<TreeView>();
            relist.ForEach(t => datalist.Add(t.CToTreeView()));
            return Json(new { data = datalist }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存部门
        /// </summary>
        /// <param name="strlist"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAbpAntiForgeryTokenValidation]
        [Route("/CategoryCourse/saveOrUpdate")]
        public ActionResult saveOrUpdate(string strlist)
        {
            try
            {
                //因为页面操作是修改删除添加全由一个保存按钮来实现的
                //所以这里的保存要原来的数据全部删掉然后再重新添加最简单
                //但是如果部门表有关联表且关联的是id可能会有问题,可以改为关联名称，且名称不重复就能避免
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var treelist = jss.Deserialize<List<TreeView>>(strlist);
                var dtolist = treelist.TreeViewToCDto();
                foreach (var item in dtolist)
                {
                    if (item.IsNew)
                    {
                        if (item.ParentId > 0)
                        {
                            var date = _corecategoryService.GetCourseCategoryList().FirstOrDefault(t => t.CategoryId == item.ParentId);
                            if (date != null)
                            {
                                item.Path = date.Path  + item.CategoryId + "/";
                            }
                        }
                        else
                        {
                            item.Path = item.CategoryId + "/";
                        }
                                   
                        item.IsNew = false;
                        _corecategoryService.CreateCourseCategory(item);
                    }
                    else
                    {
                        string sqlstr = "update px_course_category set name=@name ,update_time=@update_time, updater=@updater where category_id=@category_id";
                        var i = _sqlExecuter.Execute(sqlstr,
                                                        new MySqlParameter("@name", item.CourseCategoryName),
                                                        new MySqlParameter("@update_time", DateTime.Now),
                                                        new MySqlParameter("@updater", 1),
                                                        new MySqlParameter("@category_id", item.CategoryId));
                    }
                    
                }
                return Content("添加成功！");
            }
            catch (Exception e)
            {
                return Content("添加失败！");
            }
        }

        public ActionResult getLastId()
        {
            var dto = _corecategoryService.GetCourseCategoryList().OrderByDescending(t => t.CategoryId).FirstOrDefault();
            if (dto == null)
            {
                return Content("101");
            }
            return Content(dto.CategoryId + "");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var model = _corecategoryService.GetCourseCategoryList().Where(t => t.CategoryId == id).FirstOrDefault();
                if (model == null)
                {
                    return Json(new { code = 200, msg = "删除成功！" }, JsonRequestBehavior.AllowGet);
                }
                
                if (model != null)
                {
                    var courseDate = _courseInfoService.GetAll().Where(c => c.CategoryType == model.CategoryId);
                    return Json(new { code = 500, msg = "删除失败,课程正在使用当前分类，不允许删除" }, JsonRequestBehavior.AllowGet);
                }

                _corecategoryService.DeleteCourseCategorybyCategoryId(model.CategoryId);
                return Json(new { code = 200, msg = "删除成功！" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "删除失败！" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}