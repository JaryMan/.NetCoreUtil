using Util.Datas.EFCore.Models;

namespace Util.Samples.Services.Models
{
    /// <summary>
    /// 用户模型对象
    /// </summary>
    public class User : BaseModel<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// QQ号码
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 第一等级
        /// </summary>
        public string FirstLevel { get; set; }

        /// <summary>
        /// 第二等级
        /// </summary>
        public string SecondLevel { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
    }
}
