using System.Collections.Generic;
using System.Threading.Tasks;
using Util.Samples.Services.Datas.DapperData;
using Util.Samples.Services.Datas.EFData;
using Util.Samples.Services.Models;

namespace Util.Samples.Services.Services
{
    /// <summary>
    /// 用户服务层
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// 用户仓储
        /// </summary>
        public UserRepository UserRepository { get; set; }

        /// <summary>
        /// 用户数据层
        /// </summary>
        public UserData UserData { get; set; }

        /// <summary>
        /// 初始化用户服务
        /// </summary>
        /// <param name="userRepository">用户仓储</param>
        /// <param name="userData">用户数据层</param>
        public UserService(UserRepository userRepository, UserData userData)
        {
            UserRepository = userRepository;
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

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddAsync()
        {
            var user = new User()
            {
                Name = "Jack",
                Email = "xxxx@qq.com",
                QQ = "535353",
                Position = "测试1",
                FirstLevel = "等级1",
                SecondLevel = "等级2",
                Mobile = "132423423"
            };
            //Dapper方法1
            //await UserData.AddAsync(user);
            //EF方法2
            await UserRepository.AddUsersAsync(user);
            //await UserRepository.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 更改用户
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateAsync()
        {
            var user = await UserRepository.UpdateAsync(x => x.Id == 2, y => new User() { Name = "Jassce" });
            //if (user == null) return false;
            //user.Name = "Jack1";
            //await UserRepository.UpdateAsync(user);
            //await UserRepository.SaveChangesAsync();
            return true;
        }
    }
}
