using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum StatusCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        OK = 8200,

        /// <summary>
        /// 内部错误
        /// </summary>
        InternalServerError = 8500,


        /// <summary>
        /// 未验证
        /// </summary>
        UnAuthenticate = 8401,


        /// <summary>
        /// 未授权
        /// </summary>
        UnAuthorize = 8402
    }
}
