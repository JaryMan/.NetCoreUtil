using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Datas.Dapper;
using Util.Samples.Services.Datas.Context;
using Util.Samples.Services.Models;

namespace Util.Samples.Services.Datas.DapperData
{
    /// <summary>
    /// 用户数据类
    /// </summary>
    public class UserData
    {
        DapperContext _context;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public UserData(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync()
        {
            var sql = "Select * from userinfos";
            return (await _context.Connection.GetListAsync<User>(sql)).ToList();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        public async Task<int> AddAsync(User user)
        {
            var sql = "";

            ////普通执行1
            //await DapperHelper.ExecuteAsync(_context.connStr, sql);

            //普通执行2
            //using(var conn = _context.CreateConn())
            //{
            //    await conn.ExecuteAsync(sql);
            //}

            ////普通执行3
            //_context.Execute(sql);

            //普通执行4
            return await _context.Connection.ExecuteAsync(sql);

            ////事务执行
            //using(var tran = _context.CreateTran())
            //{
            //    tran.Exceute(sql);
            //    tran.Commit();
            //}
        }
    }
}
