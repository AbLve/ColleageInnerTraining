using ColleageInnerTraining.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColleageInnerTraining.Web.Areas.Wap.Models
{
    public class HomeViewModel
    {
        public List<BannerListDto> banners { get; set; }
        public List<CourseInfoListDto> course3news { get; set; }
        public List<CourseCategoryListDto> TopCategory { get; set; }
    }
}