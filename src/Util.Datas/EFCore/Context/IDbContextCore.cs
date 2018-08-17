using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Util.Datas.EFCore.Models;

namespace Util.Datas.EFCore.Context
{
    /// <summary>
    /// 数据上下文接口
    /// </summary>
    public interface IDbContextCore:IDisposable
    {
        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        int Add<T>(T entity) where T : class;

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<int> AddAsync<T>(T entity) where T : class;

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        int AddRange<T>(ICollection<T> entities) where T : class;

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        Task<int> AddRangeAsync<T>(ICollection<T> entities) where T : class;

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <param name="destinationTableName">表名称</param>
        void BulkInsert<T, TKey>(IList<T> entities, string destinationTableName = null) where T : BaseModel<TKey>;
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="key">主键</param>
        /// <returns></returns>
        int Delete<T,TKey>(TKey key) where T : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        int Delete<T>(Expression<Func<T, bool>> @where) where T : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> @where) where T : class;
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        int Edit<T, TKey>(T entity) where T : BaseModel<TKey>;

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        int EditRange<T>(ICollection<T> entities) where T : class;

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">条件表达式</param>
        /// <param name="updateColumns">更新字段名称列表</param>
        /// <returns></returns>
        int Update<T>(T model, params string[] updateColumns) where T : class;

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <param name="updateFactory">更新内容</param>
        /// <returns></returns>
        int Update<T>(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory) where T : class;

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <param name="updateFactory">更新内容</param>
        /// <returns></returns>
        Task<int> UpdateAsync<T>(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory) where T : class;
        #endregion


        #region 查询
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        int Count<T>(Expression<Func<T, bool>> @where = null) where T : class;

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        Task<int> CountAsync<T>(Expression<Func<T, bool>> @where = null) where T : class;

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        bool Exist<T>(Expression<Func<T, bool>> @where = null) where T : class;

        /// <summary>
        /// 关联查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="include">关联表达式</param>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        IQueryable<T> FilterWithInclude<T>(Func<IQueryable<T>, IQueryable<T>> include, Expression<Func<T, bool>> where) where T : class;

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        Task<bool> ExistAsync<T>(Expression<Func<T, bool>> @where = null) where T : class;

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="key">主键</param>
        /// <returns></returns>
        T Find<T, TKey>(TKey key) where T : class;

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="key">主键</param>
        /// <returns></returns>
        Task<T> FindAsync<T, TKey>(TKey key) where T : class;

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="where">条件表达式</param>
        /// <param name="asNoTracking">是否只读</param>
        /// <returns></returns>
        IQueryable<T> Get<T>(Expression<Func<T, bool>> @where = null, bool asNoTracking = false) where T : class;

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        T GetSingleOrDefault<T>(Expression<Func<T, bool>> @where = null) where T : class;

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        Task<T> GetSingleOrDefaultAsync<T>(Expression<Func<T, bool>> @where = null) where T : class;
        #endregion

        #region other

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns></returns>
        DatabaseFacade GetDatabase();
        /// <summary>
        /// 获取DbSet对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        DbSet<T> GetDbSet<T>() where T : class;

        /// <summary>
        /// 保存变更
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 保存变更
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">是否全部成功才变更</param>
        /// <returns></returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 保存变更
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 保存变更
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">是否全部成功才变更</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));

        bool EnsureCreated();
        Task<bool> EnsureCreatedAsync();
        #endregion
        //int ExecuteSqlWithNonQuery(string sql, params object[] parameters);

        //Task<int> ExecuteSqlWithNonQueryAsync(string sql, params object[] parameters);

        //List<TView> SqlQuery<T, TView>(string sql, params object[] parameters)  where T : class  where TView : class;
        //Task<List<TView>> SqlQueryAsync<T, TView>(string sql, params object[] parameters) where T : class where TView : class;

    }
}