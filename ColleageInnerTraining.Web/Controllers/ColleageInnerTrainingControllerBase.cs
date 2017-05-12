using Abp.UI;
using Abp.Web.Mvc.Controllers;

namespace ColleageInnerTraining.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class ColleageInnerTrainingControllerBase : AbpController
    {    
        protected ColleageInnerTrainingControllerBase()
        {
            //LocalizationSourceName = ColleageInnerTrainingConsts.LocalizationSourceName;
        }
        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }    
    }
}