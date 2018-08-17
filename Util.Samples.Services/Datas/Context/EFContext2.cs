using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Datas.EFCore.Context;
using Util.Datas.EFCore.Options;
using Util.Samples.Services.Models;

namespace Util.Samples.Services.Datas.Context
{
    /// <summary>
    /// EF数据上下文
    /// </summary>
    public class EFContext2: MySqlDbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        public EFContext2(string connStr) : base(connStr)
        {

        }

        /// <summary>
        /// 模型映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("userinfos").HasKey(x => x.Id);
        }
    }
}
