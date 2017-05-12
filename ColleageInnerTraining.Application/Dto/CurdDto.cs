using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColleageInnerTraining.Dto
{
    public class CurdDto
    {
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual bool IsDeleted { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }

        public virtual long? CreatorUserId { get; set; }
    }
}
