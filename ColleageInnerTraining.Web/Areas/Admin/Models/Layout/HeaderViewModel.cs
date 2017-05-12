using Abp.Application.Navigation;
using ColleageInnerTraining.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColleageInnerTraining.Web.Admin.Models
{
    /// <summary>
    /// 顶部Model文件
    /// </summary>
    public class HeaderViewModel
    {
        public List<MenuListDto> Menu { get; set; }

        public string CurrentPageName { get; set; }

        public string UserName { get; set; }

        public string PageName { get; set; }
    }
}