using CoreLibrary.Core.Consts;

namespace CoreLibrary.Core
{
    public class ApiResult
    {
        
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        
        public ApiResult() 
        {
            Message = MessageConst.Successful;
            Code = (int)ApiResultCodeEnum.OK;
        }
        /// <summary>
        /// 返回的Json构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ApiResult(int code = (int)ApiResultCodeEnum.OK, string message = ""):this()
        {
            if (string.IsNullOrWhiteSpace(message) && code == (int)ApiResultCodeEnum.OK)
                message = MessageConst.Successful;
            Code = code;
            Message = message;            
        }        

        /// <summary>
        /// 默认成功扩展
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResult Success(string message = "")
        {
            return new ApiResult(message: message);
        }
        /// <summary>
        /// 默认失败扩展
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResult Failed(ApiResultCodeEnum code, string message)
        {
            return new ApiResult((int)code, message);
        }        
    }
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// 数据集
        /// </summary>
        public T? Data { get; set; }
        /// <summary>
        /// 返回的构造函数
        /// </summary>
        public ApiResult() : base((int)ApiResultCodeEnum.OK) { }

        /// <summary>
        /// 返回的构造函数
        /// </summary>
        /// <param name="code">状态枚举值</param>
        /// <param name="data">数据</param>
        /// <param name="message">消息提示</param>
        public ApiResult(int code, T data, string message = "") : base(code, message)
        {
            Data = data;
        }

        /// <summary>
        /// 返回的构造函数
        /// </summary>
        /// <param name="code">状态枚举值</param>
        /// <param name="data">数据</param>
        /// <param name="message">消息提示</param>
        public ApiResult(int code, string message = "") : base(code, message){}

        /// <summary>
        /// 默认成功扩展
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息提示</param>
        /// <returns></returns>
        public static ApiResult<T> Success(T data, string message = "")
        {
            return new ApiResult<T>((int)ApiResultCodeEnum.OK, data, message);
        }
        /// <summary>
        /// 默认失败扩展
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">消息提示</param>
        /// <returns></returns>
        public new static ApiResult<T> Failed(ApiResultCodeEnum code, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = MessageConst.Fail;
            return new ApiResult<T>((int)code, message);
        }
    }
}
