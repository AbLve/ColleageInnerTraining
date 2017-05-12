using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColleageInnerTraining.Core
{
    /// <summary>
    /// 分页对象
    /// </summary>
    public class PageQuery
    {
        public int pageCurentIndex { get; set; }//当前页
        public int pageSize { get; set; }//每页行数
        public string keyword { get; set; }//查询条件
        public string type { get; set; }//查询条件类型
        public string langtype { get; set; }//查询条件语言

        public int records { get; set; }//总行数
        public int pageRange { get; set; }//页数范围

        //private int _pageCount { get; set; }
        public int pageCount { get { return records % pageSize == 0 ? records / pageSize : records / pageSize + 1; } }//总页数
        public int startPage { get { return pageCurentIndex > pageRange ? pageCurentIndex - (pageCurentIndex % pageRange == 0 ? pageRange : pageCurentIndex % pageRange) + 1 : 1; } }//开始页
        // private int _endPage { get; set; }
        public int endPage { get { return pageCount > startPage + pageRange - 1 ? startPage + pageRange - 1 : pageCount; } }//结束页
        // private int _StartNum { get; set; }
        public int startNum { get { return records != 0 ? ((pageCurentIndex - 1) * pageSize + 1) : 0; } }//开始行数
        // private int _EndNum { get; set; }
        public int endNum { get { return startNum + pageSize - 1 > records ? records : startNum + pageSize - 1; } }//结束行数

        public string url { get; set; }
        public int getstartPage(int currentIndex, int pageRange)
        {
            for (var i = 1000; i >= 1; i--)
            {
                if (i > 2)
                {
                    if (i % 2 == 1)
                        return i;
                }
            }
            return 1;
        }
    }
}
