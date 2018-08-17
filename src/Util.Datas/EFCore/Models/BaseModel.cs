using System;

namespace Util.Datas.EFCore.Models
{
    /// <summary>
    /// 所有数据表实体类都必须实现此接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    [Serializable]
    public abstract class BaseModel<TKey>
    {
        public abstract TKey Id { get; set; }
    }
}
