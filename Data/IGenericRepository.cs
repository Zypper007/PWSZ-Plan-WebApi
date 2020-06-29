using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity 
    {
        Task<TOtherIntity> Get<TOtherIntity>(int id) where TOtherIntity : BaseEntity;
        Task<TOtherEntity> Create<TOtherEntity>(TOtherEntity obj) where TOtherEntity : BaseEntity;
        Task<TEntityOther> Get<TEntityOther>(Expression<Func<TEntityOther, bool>> predicate) where TEntityOther : BaseEntity;
        Task<List<TEntityOther>> GetAll<TEntityOther>(Expression<Func<TEntityOther, bool>> predicate) where TEntityOther : BaseEntity;
        Task<bool> IsExist<TEntityOther>(Expression<Func<TEntityOther, bool>> predicate) where TEntityOther : BaseEntity;
        Task<bool> IsExist(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Create(TEntity obj);
        Task<TEntity> Update(TEntity obj);
        Task<TEntity> Delete(TEntity obj);
    }
}
