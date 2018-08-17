using Util.Samples.Services.Models;
using System.Threading.Tasks;
using Util.Samples.Services.Datas.Context;
using Util.Datas.EFCore.Repositories;

namespace Util.Samples.Services.Datas.EFData
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public class UserRepository : BaseRepository<User, int>
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        EFContext Context = null;

        /// <summary>
        /// 初始化用户仓储
        /// </summary>
        /// <param name="context">工作单元</param>
        public UserRepository(EFContext context) : base(context)
        {
            Context = context;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public  async Task<bool> AddUsersAsync(User user)
        {
            await Context.AddAsync(user);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
