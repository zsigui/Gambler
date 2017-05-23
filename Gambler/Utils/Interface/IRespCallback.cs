using Gambler.Model.XPJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils.Interface
{
    /// <summary>
    /// 执行网络请求之后的回调接口
    /// </summary>
    public interface IRespCallback<T> where T : RespBase
    {
        /// <summary>
        /// 网络请求成功，并过滤服务器错误后返回成功结果
        /// </summary>
        /// <param name="data">返回的 Data 数据不为空</param>
        void OnSuccess(T data);

        /// <summary>
        /// 网络请求过程出错，可能是 Http 请求错误，也可能是请求参数错误之类的服务器错误
        /// </summary>
        /// <param name="httpStatus">错误请求状态码，当为 >=200 && < 400 时表示服务器错误</param>
        /// <param name="errcode">错误状态码</param>
        /// <param name="errorMsg">错误信息</param>
        void OnFailed(int httpStatus, int errcode, String errorMsg);

        /// <summary>
        /// 请求执行过程出现的错误，一般为客户端错误
        /// </summary>
        /// <param name="e">执行错误抛出的异常</param>
        void OnError(Exception e);
    }
}
