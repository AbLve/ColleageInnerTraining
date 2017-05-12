using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace ColleageInnerTraining.Web.Infrustructure
{
    /// <summary>
    /// 自定义访问日志信息
    /// </summary>
    public class CustomeAuditingStore : IAuditingStore
    {
        public Task SaveAsync(AuditInfo auditInfo)
        {
            return Task.Factory.StartNew(()=> {

                //ToDo:记录Auditing info.
            }); 
        }
    }
}