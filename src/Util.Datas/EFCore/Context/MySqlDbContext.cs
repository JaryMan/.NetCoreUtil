using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Util.Datas.EFCore.Options;
using Util.Extension;

namespace Util.Datas.EFCore.Context
{
    /// <summary>
    /// MySql数据上下文
    /// </summary>
    public class MySqlDbContext: BaseDbContext
    {
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="option">设置</param>
        //public MySqlDbContext(IOptions<DbContextOption> option) : base(option)
        //{
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        public MySqlDbContext(string connStr) : base(connStr)
        {

        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="optionsBuilder">配置生成器</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_option.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <param name="destinationTableName">数据表名称</param>
        public override void BulkInsert<T, TKey>(IList<T> entities, string destinationTableName = null)
        {
            if (entities == null || !entities.Any()) return;
            if (string.IsNullOrEmpty(destinationTableName))
            {
                var mappingTableName = typeof(T).GetCustomAttribute<TableAttribute>()?.Name;
                destinationTableName = string.IsNullOrEmpty(mappingTableName) ? typeof(T).Name : mappingTableName;
            }
            var tmpDir = Path.Combine(AppContext.BaseDirectory, "Temp");
            if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
            var csvFileName = Path.Combine(tmpDir, $"{DateTime.Now:yyyyMMddHHmmssfff}.csv");
            if (!File.Exists(csvFileName))
                File.Create(csvFileName);
            var separator = ",";
            entities.SaveToCsv(csvFileName, separator);
            using (var conn = Database.GetDbConnection() as MySqlConnection ?? new MySqlConnection(_option.ConnectionString))
            {
                conn.Open();
                var bulk = new MySqlBulkLoader(conn)
                {
                    NumberOfLinesToSkip = 0,
                    TableName = destinationTableName,
                    FieldTerminator = separator,
                    FieldQuotationCharacter = '"',
                    EscapeCharacter = '"',
                    LineTerminator = "\r\n"
                };
                bulk.LoadAsync();
                conn.Close();
            }
            File.Delete(csvFileName);
        }
    }
}
