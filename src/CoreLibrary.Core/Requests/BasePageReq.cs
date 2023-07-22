namespace CoreLibrary.Core
{
    public class BasePageReq
    {
        /// <summary>
        /// 当前页 默认从1 开始
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 显示的条数 不传递默认10条
        /// </summary>
        public int PageSize { get; set; }
    }
}
