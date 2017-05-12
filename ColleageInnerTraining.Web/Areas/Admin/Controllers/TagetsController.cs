using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{

    public class TagetsController : BaseController
    {

        private const string ex = "GZSXY.Pages.Manage.Knowledge.TagetManage.";

        private ITagetAppService Tagetservice;
        public TagetsController(ITagetAppService _Tagetservice)
        {
            Tagetservice = _Tagetservice;
        }
        /// <summary>
        /// 标签index
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex+ "Index")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDataList()
        {
            ViewBag.pageName = "GetDataList";
            var pagedata = Tagetservice.GetAllTagets();
            return PartialView("Shared/DataList", pagedata);
        }
        //public ActionResult Create(int id = 0)
        //{
        //    var model = new GetTagetForEditOutput();
        //    if (id > 0)
        //    {
        //        model =  Tagetservice.GetTagetForEdit(new NullableIdDto<long> { Id = id });
        //    }
        //    return View(model.Taget);
        //}

        [HttpPost]
        [Route("/Taget/Save")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TagetEditDto dto)
        {
            try
            {
                Tagetservice.CreateOrUpdateTagetAsync(new CreateOrUpdateTagetInput { TagetEditDto = dto });
                return Success();
            }
            catch (Exception e)
            {
                return Fail(e.InnerException.Message);
            }
        }
        [PermissionAuthorizeAttribute(ex + "Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                Tagetservice.DeleteTagetAsync(new EntityDto<long> { Id = id });
                return Success();
            }
            catch (Exception e)
            {
                return Fail(e.InnerException.Message);
            }
        }

    }
}