using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Util.Datas.EFCore.Models;
using Util.Datas.EFCore.Options;
using Z.EntityFramework.Plus;

namespace Util.Datas.EFCore.Context
{
    /// <summary>
    /// 基础数据上下文
    /// </summary>
    public abstract class BaseDbContext : DbContext, IDbContextCore
    {
        /// <summary>
        /// 配置
        /// </summary>
        protected readonly DbContextOption _option;

        #region 初始化
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="option"></param>
        //protected BaseDbContext(IOptions<DbContextOption> option)
        //{
        //    if (option == null) throw new ArgumentNullException(nameof(option));
        //    if(string.IsNullOrEmpty(option.Value.ConnectionString))
        //        throw new ArgumentNullException(nameof(option.Value.ConnectionString));
        //    _option = option.Value;
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        protected BaseDbContext(string connStr)
        {
            _option = new DbContextOption();
            _option.ConnectionString = connStr;
        }

        /// <summary>
        /// 模型创建
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MappingEntityTypes(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region 新增

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public new virtual int Add<T>(T entity) where T : class
        {
            base.Add(entity);
            return SaveChanges();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual async Task<int> AddAsync<T>(T entity) where T : class
        {
            await base.AddAsync(entity);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public virtual int AddRange<T>(ICollection<T> entities) where T : class
        {
            base.AddRange(entities);
            return SaveChanges();
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T">新增</typeparam>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public virtual async Task<int> AddRangeAsync<T>(ICollection<T> entities) where T : class
        {
            await base.AddRangeAsync(entities);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="entities"></param>
        /// <param name="destinationTableName"></param>
        public virtual void BulkInsert<T, TKey>(IList<T> entities, string destinationTableName = null) where T : BaseModel<TKey>
        {
            if (!Database.IsSqlServer() && !Database.IsMySql())
                throw new NotSupportedException("This method only supports for SQL Server or MySql.");
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual int Edit<T, TKey>(T entity) where T : BaseModel<TKey>
        {
            var model = Find<T>(entity.Id);
            Entry(model).CurrentValues.SetValues(entity);
            return SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        /// <returns></returns>
        public virtual int EditRange<T>(ICollection<T> entities) where T : class
        {
            Set<T>().AttachRange(entities.ToArray());
            return SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">实体</param>
        /// <param name="updateColumns">更新字段</param>g 
        /// <returns></returns>
        public virtual int Update<T>(T model, params string[] updateColumns) where T : class
        {
            if (updateColumns != null && updateColumns.Length > 0)
            {
                if (Entry(model).State == EntityState.Added ||
                    Entry(model).State == EntityState.Detached) Set<T>().Attach(model);
                foreach (var propertyName in updateColumns)
                {
                    Entry(model).Property(propertyName).IsModified = true;
                }
            }
            else
            {
                Entry(model).State = EntityState.Modified;
            }
            return SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <param name="updateFactory">更新内容</param>
        /// <returns></returns>
        public virtual int Update<T>(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory) where T : class
        {
            return Set<T>().Where(where).Update(updateFactory);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <param name="updateFactory">更新内容</param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync<T>(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory) where T : class
        {
            return await Set<T>().Where(where).UpdateAsync(updateFactory);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="TKey">主键类型</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual int Delete<T, TKey>(TKey key) where T : class
        {
            var entity = Find<T>(key);
            Remove(entity);
            return SaveChanges();
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual int Delete<T>(Expression<Func<T, bool>> @where) where T : class
        {
            return Set<T>().Where(@where).Delete();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync<T>(Expression<Func<T, bool>> @where) where T : class
        {
            return await Set<T>().Where(@where).DeleteAsync();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual int Count<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return where == null ? Set<T>().Count() : Set<T>().Count(@where);
        }

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return await (where == null ? Set<T>().CountAsync() : Set<T>().CountAsync(@where));
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual bool Exist<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return @where == null ? Set<T>().Any() : Set<T>().Any(@where);
        }

        /// <summary>
        /// 关联查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="include">关联表达式</param>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual IQueryable<T> FilterWithInclude<T>(Func<IQueryable<T>, IQueryable<T>> include, Expression<Func<T, bool>> @where) where T : class
        {
            var result = GetDbSet<T>().AsQueryable();
            if (where != null)
                result = GetDbSet<T>().Where(where);
            if (include != null)
                result = include(result);
            return result;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<bool> ExistAsync<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return await (@where == null ? Set<T>().AnyAsync() : Set<T>().AnyAsync(@where));
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public virtual T Find<T, TKey>(TKey key) where T : class
        {
            return base.Find<T>(key);
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync<T, TKey>(TKey key) where T : class
        {
            return await base.FindAsync<T>(key);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <param name="asNoTracking">是否跟踪（false会提升查询速度）</param>
        /// <returns></returns>
        public virtual IQueryable<T> Get<T>(Expression<Func<T, bool>> @where = null, bool asNoTracking = false) where T : class
        {
            var query = Set<T>().AsQueryable();
            if (where != null)
                query = query.Where(where);
            if (asNoTracking)
                query = query.AsNoTracking();
            return query;
        }

        /// <summary>
        /// 查询单个
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual T GetSingleOrDefault<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return where == null ? Set<T>().SingleOrDefault() : Set<T>().SingleOrDefault(where);
        }

        /// <summary>
        /// 查询单个
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<T> GetSingleOrDefaultAsync<T>(Expression<Func<T, bool>> @where = null) where T : class
        {
            return await (where == null ? Set<T>().SingleOrDefaultAsync() : Set<T>().SingleOrDefaultAsync(where));
        }
        #endregion

        #region 其他
        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns></returns>
        public DatabaseFacade GetDatabase() => Database;

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public virtual DbSet<T> GetDbSet<T>() where T : class
        {
            return Set<T>();
        }
        public virtual bool EnsureCreated()
        {
            return Database.EnsureCreated();
        }

        public virtual async Task<bool> EnsureCreatedAsync()
        {
            return await Database.EnsureCreatedAsync();
        }
        #endregion

        ///// <summary>
        ///// ExecuteSqlWithNonQuery
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <param name="parameters"></param>
        ///// <returns></returns>
        //public virtual int ExecuteSqlWithNonQuery(string sql, params object[] parameters)
        //{
        //    return Database.ExecuteSqlCommand(sql,
        //        CancellationToken.None,
        //        parameters);
        //}

        //public virtual async Task<int> ExecuteSqlWithNonQueryAsync(string sql, params object[] parameters)
        //{
        //    return await Database.ExecuteSqlCommandAsync(sql,
        //        CancellationToken.None,
        //        parameters);
        //}

        //public virtual List<TView> SqlQuery<T,TView>(string sql, params object[] parameters) 
        //    where T : class
        //    where TView : class
        //{
        //    return Set<T>().FromSql(sql, parameters).Cast<TView>().ToList();
        //}

        //public virtual async Task<List<TView>> SqlQueryAsync<T,TView>(string sql, params object[] parameters) 
        //    where T : class
        //    where TView : class
        //{
        //    return await Set<T>().FromSql(sql, parameters).Cast<TView>().ToListAsync();
        //}
    }
}