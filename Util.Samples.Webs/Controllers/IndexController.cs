using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Util.Datas.Dapper;
using Util.Samples.Services.Datas.DapperData;
using Util.Samples.Services.Datas.EFData;
using Util.Samples.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Util.Webs.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace Util.Samples.Webs.Controllers
{
    
    /// <summary>
    /// 首页控制器
    /// </summary>
    public class IndexController : WebApiControllerBase
    {

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        [AllowAnonymous]
        [Route("api/index/getusers")]
        public async Task<IActionResult> GetUsers()
        {
            ////获取注入的服务
            //UserService service = this.GetService<UserService>();
            //var users = await service.GetUsersAsync();

            //DB2操作
            UserService2 service = this.GetService<UserService2>();
            var users = await service.GetUsersAsync();
            return Ok(users);
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        [AllowAnonymous]
        [Route("api/index/adduser")]
        public async Task<IActionResult> AddUser()
        {
            //获取注入的服务
            UserService service = this.GetService<UserService>();
            var isSuccess = await service.AddAsync();
            return Ok(isSuccess);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UpdateUser()
        {
            UserService service = this.GetService<UserService>();
            var isSuccess = await service.UpdateAsync();
            return Ok(isSuccess);
        }
    }
}
