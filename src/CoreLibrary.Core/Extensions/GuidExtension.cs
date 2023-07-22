namespace CoreLibrary.Core.Extensions
{
    public static class GuidExtension
    {
        /// <summary>
        /// 转guid
        /// </summary>
        /// <param name="obj">源</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public static Guid ToGuid(this object obj, Guid defaultVal = default)
        {
            if (obj == null)
                return defaultVal;
            if (!Guid.TryParse(obj.ToStr(), out var retVal))
                retVal = defaultVal;
            return retVal;
        }
        /// <summary>
        ///判断
        /// </summary>
        /// <param name="id">源</param>
        /// <returns></returns>
        public static bool IsNotNull(this Guid? id)
        {
            return id.HasValue && id.Value != Guid.Empty;
        }
    }
}
