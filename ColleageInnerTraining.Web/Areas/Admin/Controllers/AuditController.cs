using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 知识点审核
    /// </summary>
    public class AuditController : BaseController
    {
        private const string ex = "GZSXY.Pages.Manage.Konwledge.Audit.";

        #region 注入服务
        private IKonwledgeInfoAppService KonwledgeInfoservice;
        private ICourseCategoryAppService coreCategoryService;

        public AuditController(IKonwledgeInfoAppService _KonwledgeInfoservice, ICourseCategoryAppService _coreCategoryService)
        {
            KonwledgeInfoservice = _KonwledgeInfoservice;
            coreCategoryService = _coreCategoryService;
        }
        #endregion

        #region 操作
        /// <summary>
        /// 知识点审核index
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Index")]
        [Route("/Audit/Index")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("/Audit/GetDataList")]
        public ActionResult GetDataList(string keyword, int type = 0, int pIndex = 1)
        {
            ViewBag.pageName = "GetDataList";
            var input = new GetKonwledgeInfoInput() { FilterText = keyword, TypeId = type, SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize };
            var pagedata = KonwledgeInfoservice.GetPagedKonwledgeInfos(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/DataList", pagedata.Items);
        }
        /// <summary>
        /// 审核操作
        /// </summary>
        /// <param name="stauts"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Audited")]
        [Route("/Audit/Audited")]
        public ActionResult Audited(int stauts, int id)
        {
            var edit = KonwledgeInfoservice.GetKonwledgeInfoForEdit(new NullableIdDto<long> { Id = id }).KonwledgeInfo;
            edit.Stauts = stauts;
            edit.Auditor = 1;
            edit.ReviewedDate = DateTime.Now;
            KonwledgeInfoservice.UpdateKonwledgeInfoAsync(edit);
            return Success();
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