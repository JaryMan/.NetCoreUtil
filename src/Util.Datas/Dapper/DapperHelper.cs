using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static Dapper.SqlMapper;

namespace Util.Datas.Dapper
{
    /// <summary>
    /// Dapper帮助类(同步)
    /// </summary>
    public static partial class DapperHelper
    {
        #region Dapper事务扩展
        /// <summary>
        /// 执行脚本并返IDataReader对象（事务）
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return tran.conn.ExecuteReader(sql, param, tran.transaction, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返回行（事务）
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>影响行数</returns>
        public static int Exceute(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return tran.conn.Execute(sql, param, tran.transaction, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返object对象
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static object ExecuteScalar(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return tran.conn.ExecuteScalar(sql, param, tran.transaction, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返结果集合
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>结果集合</returns>
        public static GridReader GetMultiple(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return tran.conn.QueryMultiple(sql, param, tran.transaction, commandType: type);
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型</returns>
        public static T Get<T>(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return tran.conn.QueryFirstOrDefault<T>(sql, param, tran.transaction, commandType: type);
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="tran">Dapper事务对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型列表</returns>
        public static IEnumerable<T> GetList<T>(this DapperTransaction tran, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return tran.conn.Query<T>(sql, param, tran.transaction, commandType: type);
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
        public static IDataReader ExecuteReader(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return conn.ExecuteReader(sql, param, commandType: type);
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
        public static int Execute(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return conn.Execute(sql, param, commandType: type);
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
        public static object ExecuteScalar(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return conn.ExecuteScalar(sql, param, commandType: type);
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
        public static GridReader GetMultiple(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return conn.QueryMultiple(sql, param, commandType: type);
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
        public static T Get<T>(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return conn.QueryFirstOrDefault<T>(sql, param, commandType: type);
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
        public static IEnumerable<T> GetList<T>(string connStr, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                return conn.Query<T>(sql, param, commandType: type);
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
        public static IDataReader ExecuteReader(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return conn.ExecuteReader(sql, param, commandType: type);
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
        public static int Execute(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return conn.Execute(sql, param, commandType: type);
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
        public static object ExecuteScalar(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return conn.ExecuteScalar(sql, param, commandType: type);
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
        public static GridReader GetMultiple(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return conn.QueryMultiple(sql, param, commandType: type);
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
        public static T Get<T>(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return conn.QueryFirstOrDefault<T>(sql, param, commandType: type);
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
        public static IEnumerable<T> GetList<T>(this DapperContextBase context, string sql, object param = null, CommandType type = CommandType.Text)
        {
            using (MySqlConnection conn = new MySqlConnection(context.connStr))
            {
                return conn.Query<T>(sql, param, commandType: type);
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
        public static IDataReader ExecuteReader(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return conn.ExecuteReader(sql, param, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返回行
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>影响行数</returns>
        public static int Execute(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return conn.Execute(sql, param, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返object对象
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns></returns>
        public static object ExecuteScalar(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return conn.ExecuteScalar(sql, param, commandType: type);
        }

        /// <summary>
        /// 执行脚本并返结果集合
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>结果集合</returns>
        public static GridReader GetMultiple(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return conn.QueryMultiple(sql, param, commandType: type);
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型</returns>
        public static T Get<T>(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
             return conn.QueryFirstOrDefault<T>(sql, param, commandType: type);
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="conn">Mysql连接对象</param>
        /// <param name="sql">数据库脚本</param>
        /// <param name="param">参数列表</param>
        /// <param name="type">执行类型</param>
        /// <returns>数据模型列表</returns>
        public static IEnumerable<T> GetList<T>(this MySqlConnection conn, string sql, object param = null, CommandType type = CommandType.Text)
        {
            return conn.Query<T>(sql, param, commandType: type);
        }
        #endregion
    }
}
