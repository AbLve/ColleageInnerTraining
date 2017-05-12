using Abp.Web.Mvc.Views;

namespace ColleageInnerTraining.Web.Views
{
    public abstract class ColleageInnerTrainingWebViewPageBase : ColleageInnerTrainingWebViewPageBase<dynamic>
    {

    }

    public abstract class ColleageInnerTrainingWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
    }
}