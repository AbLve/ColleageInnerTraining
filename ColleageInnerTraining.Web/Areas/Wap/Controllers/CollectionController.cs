using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Wap.Controllers
{
    public class CollectionController : Controller
    {
        public ICollectionAppService _collectionAppService;
        private readonly ICourseInfoAppService _courseInfoAppService;

        public CollectionController(ICollectionAppService collectionAppService, ICourseInfoAppService courseInfoAppService)
        {
            _collectionAppService = collectionAppService;
            _courseInfoAppService = courseInfoAppService;
        }
        // GET: Wap/Collection
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult AddCollection(string type,string name,int bizId)
        {
            CollectionEditDto collection = new CollectionEditDto();
            CollectionListDto clist = new CollectionListDto();
            try
            {
                int userId = int.Parse(CookieHelper.GetCookieValue("UserId").ToString());
                clist = _collectionAppService.GetCollectionByTypeAndId(type, bizId, userId);
                if(clist==null)
                { 
                    collection.BizId = bizId;
                    collection.BizType = type;
                    collection.UserId = userId;
                    collection.BizName = name;
                    collection.CreatorUserId = userId;
                    collection = _collectionAppService.CreateCollection(collection);
                }
            }
            catch (Exception e)
            {

            }
            return Json(collection, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult Get(string type,int bizId)
        {
            CollectionListDto collection = new CollectionListDto();
            try
            {
                int userId = int.Parse(CookieHelper.GetCookieValue("UserId").ToString());
                collection = _collectionAppService.GetCollectionByTypeAndId(type, bizId, userId);
            }
            catch (Exception e)
            {

            }
            return Json(collection, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DelCollection(string type, string name, int bizId)
        {
            CollectionListDto collection = new CollectionListDto();
            try
            {
                int userId = int.Parse(CookieHelper.GetCookieValue("UserId").ToString());
                _collectionAppService.DeleteCollectionByBizId(type, bizId, userId);
            }
            catch (Exception e)
            {

            }
            return Json(collection, JsonRequestBehavior.AllowGet);
        }
    }
}