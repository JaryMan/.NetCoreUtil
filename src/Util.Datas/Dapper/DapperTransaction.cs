using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Util.Datas.Dapper
{
    /// <summary>
    /// Dapper事务操作
    /// </summary>
    public class DapperTransaction: IDisposable
    {
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public MySqlConnection conn = null;
        /// <summary>
        /// 事务对象
        /// </summary>
        public IDbTransaction transaction = null;

        /// <summary>
        /// Dapper事务操作
        /// </summary>
        /// <param name="connection">mysql连接对象</param>
        public DapperTransaction(MySqlConnection connection)
        {
            conn = connection;
            if (conn.State == ConnectionState.Closed) conn.Open();
            transaction = conn.BeginTransaction();
        }

        /// <summary>
        /// Dapper事务操作
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        public DapperTransaction(string connStr)
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            transaction = conn.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            try
            {                
                transaction.Commit();
            }
            catch (Exception ex)
            {
                //出现异常，事务Rollback
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
            //finally
            //{
            //    //释放资源
            //    Dispose();
            //}
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (transaction != null) transaction.Dispose();
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
