using System.Text.RegularExpressions;

namespace CoreLibrary.Core
{
    /// <summary>
    /// 正则表达式
    /// </summary>
    public static class RegexHelper
    {
        private const string CheckIsNumberUpperLowerPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[\s\S]{3,}$";
        private const string CheckIsNumberUpperLowerSpecialPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{4,}";
        private const string CheckIsNumberPattern = @"^[0-9]+$";
        private const string CheckUpperPattern = @"^[A-Z]+$";
        private const string CheckLowerberPattern = @"^[a-z]+$";

        /// <summary>
        /// 至少一个大写字母，一个小写字母和一个数字 其它为任意字符
        /// </summary>
        /// <returns></returns>
        public static bool CheckIsNumberUpperLower(this string source)
        {
            return Regex.IsMatch(source, CheckIsNumberUpperLowerPattern);
        }

        /// <summary>
        /// 校验数字
        /// </summary>
        /// <returns></returns>
        public static bool CheckIsNumber(this string source)
        {
            return Regex.IsMatch(source, CheckIsNumberPattern);
        }
        /// <summary>
        /// 校验大写字符
        /// </summary>
        /// <returns></returns>
        public static bool CheckUpper(this string source)
        {
            return Regex.IsMatch(source, CheckUpperPattern);
        }
        /// <summary>
        /// 校验小写字符
        /// </summary>
        /// <returns></returns>
        public static bool CheckLower(this string source)
        {
            return Regex.IsMatch(source, CheckLowerberPattern);
        }
        /// <summary>
        ///至少1个大写字母，1个小写字母，1个数字和1个特殊字符：
        /// </summary>
        /// <returns></returns>
        public static bool CheckIsNumberUpperLowerSpecial(this string source)
        {
            return Regex.IsMatch(source, CheckIsNumberUpperLowerSpecialPattern);
        }
    }
}
