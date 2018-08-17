using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using Util.Datas.EFCore.Context;

namespace Util.Datas.Dapper
{
    /// <summary>
    /// Dapper数据上下文
    /// </summary>
    public class DapperContextBase : IDisposable
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string connStr = string.Empty;

        /// <summary>
        /// 连接对象
        /// </summary>
        private MySqlConnection conn = null;


        #region 构造函数
        ///// <summary>
        ///// 构造函数（连接字符串）
        ///// </summary>
        ///// <param name="str">连接字符串</param>
        //public DapperContext(string str)
        //{
        //    connStr = str;
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据上下文</param>
        public DapperContextBase(IDbContextCore dbContext)
        {
            conn = dbContext.GetDatabase().GetDbConnection() as MySqlConnection;
            connStr = conn.ConnectionString;
        }
        #endregion


        /// <summary>
        /// 公共连接对象
        /// </summary>
        /// <returns></returns>
        public MySqlConnection Connection
        {
            get
            {
                if (conn == null) conn = new MySqlConnection(connStr);
                if (conn.State == ConnectionState.Closed) conn.Open();
                return conn;
            }
        }

        /// <summary>
        /// 创建连接对象
        /// </summary>
        /// <returns>数据库连接对象</returns>
        public MySqlConnection CreateConn()
        {
            return new MySqlConnection(connStr);
        }

        /// <summary>
        /// 创建事务对象
        /// </summary>
        /// <returns></returns>
        public DapperTransaction CreateTran()
        {
            return new DapperTransaction(connStr);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            };
        }
    }
}
