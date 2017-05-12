using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace ColleageInnerTraining.EntityFramework.Repositories
{
    public abstract class ColleageInnerTrainingRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<ColleageInnerTrainingDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ColleageInnerTrainingRepositoryBase(IDbContextProvider<ColleageInnerTrainingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class ColleageInnerTrainingRepositoryBase<TEntity> : ColleageInnerTrainingRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ColleageInnerTrainingRepositoryBase(IDbContextProvider<ColleageInnerTrainingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
