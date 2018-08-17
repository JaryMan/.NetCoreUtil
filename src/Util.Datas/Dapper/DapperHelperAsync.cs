using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Util.Datas.Dapper
{
    /// <summary>
    /// Dapper帮助类(异步)
    /// </summary>
    public static partial class DapperHelper
    {
        #region Dapper事务扩展
        /// <summary>
        /// 执行脚本并返IDataReader对象（事务）(异步)
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static async Task<IDataReader> ExecuteReaderAsync(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await tran.conn.ExecuteReaderAsync(sql, param, tran.transaction, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返回行（事务）
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>影响行数</returns>
        public static async Task<int> ExecuteAsync(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await tran.conn.ExecuteAsync(sql, param, tran.transaction, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返object对象
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static async Task<object> ExecuteScalarAsync(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await tran.conn.ExecuteScalarAsync(sql, param, tran.transaction, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返结果集合
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>结果集合</returns>
        public static async Task<GridReader> GetMultipleAsync(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await tran.conn.QueryMultipleAsync(sql, param, tran.transaction, commandType: type);
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型</returns>
        public static async Task<T> GetAsync<T>(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await tran.conn.QueryFirstOrDefaultAsync<T>(sql, param, tran.transaction, commandType: type);
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型列表</returns>
        public static async Task<IEnumerable<T>> GetListAsync<T>(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await tran.conn.QueryAsync<T>(sql, param, tran.transaction, commandType: type);
        }
        #endregion

        #region 连接字符串
        /// <summary>
        /// 执行脚本并返IDataReader对象
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static async Task<IDataReader> ExecuteReaderAsync(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return await conn.ExecuteReaderAsync(sql, param, commandType: type);
            }
        }

        /// <summary>
        /// 执行脚本并返回行
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>影响行数</returns>
        public static async Task<int> ExecuteAsync(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return await conn.ExecuteAsync(sql, param, commandType: type);
            }
        }

        /// <summary>
        /// 执行脚本并返object对象
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static async Task<object> ExecuteScalarAsync(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return await conn.ExecuteScalarAsync(sql, param, commandType: type);
            }
        }

        /// <summary>
        /// 执行脚本并返结果集合
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>结果集合</returns>
        public static async Task<GridReader> GetMultipleAsync(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return await conn.QueryMultipleAsync(sql, param, commandType: type);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型</returns>
        public static async Task<T> GetAsync<T>(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return await conn.QueryFirstOrDefaultAsync<T>(sql, param, commandType: type);
            }
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型列表</returns>
        public static async Task<IEnumerable<T>> GetListAsync<T>(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return await conn.QueryAsync<T>(sql, param, commandType: type);
            }
        }

        #endregion

        #region Dapper数据上下文扩展
        /// <summary>
        /// 执行脚本并返IDataReader对象
        /// </summary>
        /// <param name="context">Dapper数据上下文</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static async Task<IDataReader> ExecuteReaderAsync(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return await conn.ExecuteReaderAsync(sql, param, commandType: type);
            }
        }

        /// <summary>
        /// 执行脚本并返回行
        /// </summary>
        /// <param name="context">Dapper数据上下文</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>影响行数</returns>
        public static async Task<int> ExecuteAsync(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return await conn.ExecuteAsync(sql, param, commandType: type);
            }
        }

        /// <summary>
        /// 执行脚本并返object对象
        /// </summary>
        /// <param name="context">Dapper数据上下文</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static async Task<object> ExecuteScalarAsync(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return await conn.ExecuteScalarAsync(sql, param, commandType: type);
            }
        }

        /// <summary>
        /// 执行脚本并返结果集合
        /// </summary>
        /// <param name="context">Dapper数据上下文</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>结果集合</returns>
        public static async Task<GridReader> GetMultipleAsync(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return await conn.QueryMultipleAsync(sql, param, commandType: type);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="context">Dapper数据上下文</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型</returns>
        public static async Task<T> GetAsync<T>(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return await conn.QueryFirstOrDefaultAsync<T>(sql, param, commandType: type);
            }
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="context">Dapper数据上下文</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型列表</returns>
        public static async Task<IEnumerable<T>> GetListAsync<T>(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return await conn.QueryAsync<T>(sql, param, commandType: type);
            }
        }

        #endregion

        #region MySqlConnection扩展

        /// <summary>
        /// 执行脚本并返IDataReader对象
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static async Task<IDataReader> ExecuteReaderAsync(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await conn.ExecuteReaderAsync(sql, param, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返回行
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>影响行数</returns>
        public static async Task<int> ExecuteAsync(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await conn.ExecuteAsync(sql, param, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返object对象
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static async Task<object> ExecuteScalarAsync(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await conn.ExecuteScalarAsync(sql, param, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返结果集合
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>结果集合</returns>
        public static async Task<GridReader> GetMultipleAsync(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await conn.QueryMultipleAsync(sql, param, commandType: type);
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型</returns>
        public static async Task<T> GetAsync<T>(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await conn.QueryFirstOrDefaultAsync<T>(sql, param, commandType: type);
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型列表</returns>
        public static async Task<IEnumerable<T>> GetListAsync<T>(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return await conn.QueryAsync<T>(sql, param, commandType: type);
        }
        #endregion
    }
}
