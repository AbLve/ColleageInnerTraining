using Abp.Application.Services.Dto;
using Abp.Web.Security.AntiForgery;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Controllers;
using System;
using System.Collections.Generic;
using ColleageInnerTraining.Core;
using System.Linq;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 知识点管理
    /// </summary>
    public class KnowledgeInfoController : BaseController
    {
        private const string ex = "GZSXY.Pages.Manage.Knowledge.InfoManage.";

        #region 注入服务
        private IKonwledgeInfoAppService KonwledgeInfoservice;
        private ITagetAppService ts;
        private ICourseCategoryAppService coreCategoryService;

        public KnowledgeInfoController(IKonwledgeInfoAppService _KonwledgeInfoservice, ITagetAppService _ts,ICourseCategoryAppService _coreCategoryService,
                                       IJobPostAppService _jobService, IDepartmentInfoAppService _departmentService,
                                       IUserAccountAppService _userService, ISqlExecuter _sqlExecuter) :
                                       base(_departmentService, _jobService, _sqlExecuter, _userService)
        {
            KonwledgeInfoservice = _KonwledgeInfoservice;
            ts = _ts;
            coreCategoryService = _coreCategoryService;
        }
        #endregion

        #region 操作

        /// <summary>
        ///  知识库index
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Index")]
        [Route("/KnowledgeInfo/Index")]
        public ActionResult Index()
        {
            GetTsel();
            return View();
        }
        [Route("/KnowledgeInfo/GetDataList")]
        public ActionResult GetDataList(string keyword, int taget = 0, int type = 0, int pIndex = 1)
        {
            ViewBag.pageName = "GetDataList";
            var input = new GetKonwledgeInfoInput() { FilterText = keyword, TypeId = type, TagetId = taget, SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize };
            var pagedata = KonwledgeInfoservice.GetPagedKonwledgeInfos(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/DataList", pagedata.Items);
        }
        /// <summary>
        /// 知识点表单页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Create")]
        [Route("/KnowledgeInfo/Create")]
        public ActionResult Create(int id = 0)
        {
            GetTsel();
            var model = new GetKonwledgeInfoForEditOutput();
            if (id > 0)
            {
                model = KonwledgeInfoservice.GetKonwledgeInfoForEdit(new NullableIdDto<long> { Id = id });
            }
            return View(model.KonwledgeInfo);
        }

        [HttpPost]
        [Route("/KnowledgeInfo/Save")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(KonwledgeInfoEditDto dto)
        {
            try
            {
                KonwledgeInfoservice.CreateOrUpdateKonwledgeInfoAsync(new CreateOrUpdateKonwledgeInfoInput { KonwledgeInfoEditDto = dto });
                return RedirectToAction("/Index");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// 知识点删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Delete")]
        [Route("/KnowledgeInfo/Delete")]
        public ActionResult Delete(int id)
        {         
                KonwledgeInfoservice.DeleteKonwledgeInfoAsync(new EntityDto<long> { Id = id });
                return Success();        
        }

        /// <summary>
        /// 上传资料
        /// </summary>
        /// <returns></returns>
        [DisableAbpAntiForgeryTokenValidation]
        [Route("/KnowledgeInfo/UploadFile")]
        public JsonResult UploadFile()
        {
            var file = Request.Files["knowledgeImg"];
            var path = "/uploads/KnowledgeImg/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            return Upload(file, path);
        }

        /// <summary>
        /// 获取标签下拉数据
        /// </summary>
        protected void GetTsel()
        {
            //岗位下拉框
            var tList = ts.GetAllTagets();
            var tItem = new List<SelectListItem>();
            if (tList != null && tList.Any())
            {
                tList.ForEach(t => tItem.Add(new SelectListItem { Text = t.Name, Value = t.Id.ToString() }));
            }
            ViewBag.Tagets = tItem;
        }

        /// <summary>
        /// 课程分类树状下拉
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCTreeSelectJson()
        {
            var data = coreCategoryService.GetCourseCategoryList();
            var treeList = new List<TreeSelectModel>();
            foreach (CourseCategoryListDto item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.CategoryId + "";
                treeModel.text = item.CourseCategoryName;
                treeModel.parentId = item.ParentId + "";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        #endregion
    }
}