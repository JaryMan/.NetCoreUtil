using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Util.Datas.EFCore.Models;

namespace Util.Datas.EFCore.Repositories
{
    public interface IRepository<T, in TKey>:IQueryable<T>, IDisposable where T : BaseModel<TKey>
    {
        #region Insert
        int Add(T entity);
        Task<int> AddAsync(T entity);
        int AddRange(ICollection<T> entities);
        Task<int> AddRangeAsync(ICollection<T> entities);
        void BulkInsert(IList<T> entities, string destinationTableName = null);
        #endregion

        #region Delete
        int Delete(TKey key);
        int Delete(Expression<Func<T, bool>> @where);
        Task<int> DeleteAsync(Expression<Func<T, bool>> @where);
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Edit(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int EditRange(ICollection<T> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateExp"></param>
        /// <returns></returns>
        int BatchUpdate(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateExp"></param>
        /// <returns></returns>
        Task<int> BatchUpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        int Update(T model, params string[] updateColumns);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
        int Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory);
        #endregion

        #region Query
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetSingle(TKey key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeFunc"></param>
        /// <returns></returns>
        T GetSingle(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(TKey key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T GetSingleOrDefault(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<List<T>> GetAsync(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        IEnumerable<T> GetByPagination(Expression<Func<T, bool>> @where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] @orderby);
        #endregion
    }
}
