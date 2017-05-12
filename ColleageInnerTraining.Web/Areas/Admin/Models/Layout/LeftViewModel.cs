using Abp.Application.Navigation;
using ColleageInnerTraining.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColleageInnerTraining.Web.Admin.Models
{
    public class LeftViewModel
    {
        public List<MenuListDto> Menu { get; set; }
        
        public int menuId { get; set; }

        public string MenuParentName { get; set; }

        public string CurrentPageName { get; set; }
    }
}