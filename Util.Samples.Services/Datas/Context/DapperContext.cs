using System;
using System.Collections.Generic;
using System.Text;
using Util.Datas.Dapper;

namespace Util.Samples.Services.Datas.Context
{
    /// <summary>
    /// Dapper数据上下文
    /// </summary>
    public class DapperContext:DapperContextBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context">数据上下文</param>
        public DapperContext(EFContext context) : base(context)
        {

        }
    }
}
