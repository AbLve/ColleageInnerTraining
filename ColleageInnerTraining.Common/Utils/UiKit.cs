using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ColleageInnerTraining.Common.Utils
{
    public static class UiKit
    {
        //string defaultTxt
        public static List<SelectListItem> PopulateDropdown<T>(string defaultTxt)
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "请选择", Value = "0", Selected = true });
            foreach (object s in Enum.GetValues(typeof(T)))
            {
                selectList.Add(new SelectListItem { Text = EnumDescription.GetFieldText(s), Value = ((int)s).ToString() });
            }
            return selectList;
        }
    }
}
