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
    public class UserData2
    {
        DapperContext2 _context;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public UserData2(DapperContext2 context)
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
    }
}
