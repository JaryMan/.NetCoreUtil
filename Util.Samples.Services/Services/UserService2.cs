using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Samples.Services.Datas.DapperData;
using Util.Samples.Services.Models;

namespace Util.Samples.Services.Services
{
    /// <summary>
    /// 用户服务层
    /// </summary>
    public class UserService2
    {
        ///// <summary>
        ///// 用户仓储
        ///// </summary>
        //public UserRepository UserRepository { get; set; }

        /// <summary>
        /// 用户数据层
        /// </summary>
        public UserData2 UserData { get; set; }

        /// <summary>
        /// 初始化用户服务
        /// </summary>
        /// <param name="userRepository">用户仓储</param>
        /// <param name="userData">用户数据层</param>
        public UserService2(UserData2 userData)
        {
            //UserRepository = userRepository;
            UserData = userData;
        }
        
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync()
        {
            //return await UserRepository.FindAllAsync();
            return await UserData.GetUsersAsync();
        }
    }
}
