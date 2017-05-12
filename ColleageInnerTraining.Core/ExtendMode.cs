using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Session;
namespace ColleageInnerTraining.Core
{
    public static class ExtendMode
    {
        public static void Create(this object o) {
            var flag = o is FullAuditedEntity<long>;
            if (flag)
            {
                var entity = o as FullAuditedEntity<long>;
                entity.CreationTime = DateTime.Now;
                entity.CreatorUserId = 1;
            }
           
        }
        public static void Update(this object o) {
            var flag = o is FullAuditedEntity<long>;
            if (flag)
            {
                var entity = o as FullAuditedEntity<long>;
                entity.LastModificationTime = DateTime.Now;
                entity.LastModifierUserId = 1;
            }
        }
        public static void Delete(this object o)
        {
            var flag = o is FullAuditedEntity<long>;
            if (flag)
            {
                var entity = o as FullAuditedEntity<long>;
                entity.DeletionTime = DateTime.Now;
                entity.DeleterUserId = 1;
            }
        }
    }
}
