using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Util.Datas.EFCore.Context;
using Util.Datas.EFCore.Models;
using Util.Extension;

namespace Util.Datas.EFCore.Repositories
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class BaseRepository<T, TKey>:IRepository<T, TKey> where T : BaseModel<TKey>
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        protected readonly IDbContextCore DbContext;

        /// <summary>
        /// DbSet对象
        /// </summary>
        protected DbSet<T> DbSet => DbContext.GetDbSet<T>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        protected BaseRepository(IDbContextCore dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbContext.EnsureCreatedAsync();
        }

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual int Add(T entity)
        {
            return DbContext.Add(entity);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual async Task<int> AddAsync(T entity)
        {
            return await DbContext.AddAsync(entity);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public virtual int AddRange(ICollection<T> entities)
        {
            return DbContext.AddRange(entities);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public virtual async Task<int> AddRangeAsync(ICollection<T> entities)
        {
            return await DbContext.AddRangeAsync(entities);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <param name="destinationTableName">表名称</param>
        public virtual void BulkInsert(IList<T> entities, string destinationTableName = null)
        {
            DbContext.BulkInsert<T, TKey>(entities, destinationTableName);
        }
        #endregion

        #region 更新

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual int Edit(T entity)
        {
            return DbContext.Edit<T,TKey>(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public virtual int EditRange(ICollection<T> entities)
        {
            return DbContext.EditRange(entities);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <param name="updateExp">更新内容</param>
        /// <returns></returns>
        public virtual int BatchUpdate(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp)
        {
            return DbContext.Update(where, updateExp);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <param name="updateExp">更新内容</param>
        /// <returns></returns>
        public virtual async Task<int> BatchUpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp)
        {
            return await DbContext.UpdateAsync(@where, updateExp);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="updateColumns">字段名称列表</param>
        /// <returns></returns>
        public virtual int Update(T model, params string[] updateColumns)
        {
            DbContext.Update(model,updateColumns);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <param name="updateFactory">更新内容</param>
        /// <returns></returns>
        public virtual int Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory)
        {
            return DbContext.Update(where, updateFactory);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <param name="updateFactory">更新内容</param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory)
        {
            return await DbContext.UpdateAsync(where, updateFactory);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public virtual int Delete(TKey key)
        {
            return DbContext.Delete<T,TKey>(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual int Delete(Expression<Func<T, bool>> @where)
        {
            return DbContext.Delete(where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> @where)
        {
            return await DbContext.DeleteAsync(where);
        }
        #endregion

        #region 查询

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.Count(where);
        }

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.CountAsync(where);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual bool Exist(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.Exist(where);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<bool> ExistAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.ExistAsync(where);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public virtual T GetSingle(TKey key)
        {
            return DbSet.Find(key);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="includeFunc">关联表达式</param>
        /// <returns></returns>
        public T GetSingle(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc)
        {
            if (includeFunc == null) return GetSingle(key);
            return includeFunc(DbSet.Where(m => m.Id.Equal(key))).AsNoTracking().FirstOrDefault();
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleAsync(TKey key)
        {
            return await DbContext.FindAsync<T,TKey>(key);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual T GetSingleOrDefault(Expression<Func<T, bool>> @where = null)
        {
            return DbContext.GetSingleOrDefault(@where);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbContext.GetSingleOrDefaultAsync(where);
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> @where = null)
        {
            return (@where != null ? DbSet.Where(@where).AsNoTracking() : DbSet.AsNoTracking());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> @where = null)
        {
            return await DbSet.Where(where).ToListAsync();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="asc">是否正序排序</param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetByPagination(Expression<Func<T, bool>> @where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] @orderby)
        {
            var filter = Get(where).AsEnumerable();
            if (orderby != null)
            {
                foreach (var func in orderby)
                {
                    filter = asc ? filter.OrderBy(func) : filter.OrderByDescending(func);
                }
            }
            return filter.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }

        #endregion

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return DbSet.AsQueryable().GetEnumerator();
        }

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 表达式元素类型
        /// </summary>
        public Type ElementType => DbSet.AsQueryable().ElementType;

        /// <summary>
        /// 表达式
        /// </summary>
        public Expression Expression => DbSet.AsQueryable().Expression;
        
        /// <summary>
        /// 关联查询对象
        /// </summary>

        public IQueryProvider Provider => DbSet.AsQueryable().Provider;

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    DbContext?.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~BaseRepository() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

