namespace CoreLibrary.Core
{
    public static class NumberHelper
    {
        /// <summary>
        /// 保留精度
        /// </summary>
        /// <param name="number">转换的数字</param>
        /// <param name="decimals">小数位数</param>
        /// <param name="numberChoiceType"></param>
        public static decimal Choice(decimal number, int decimals = 4, NumberChoiceTypeEnum numberChoiceType = NumberChoiceTypeEnum.AwayFromZero)
        {
            return Math.Round(number, decimals, (MidpointRounding)(int)numberChoiceType);
        }

        /// <summary>
        /// 保留精度
        /// </summary>
        /// <param name="number">转换的数字</param>
        /// <param name="decimals">小数位数</param>
        /// <param name="numberChoiceType"></param>
        public static decimal Choice(string numberString, int decimals = 4, NumberChoiceTypeEnum numberChoiceType = NumberChoiceTypeEnum.AwayFromZero)
        {
            return Choice(decimal.Parse(numberString), decimals, numberChoiceType);
        }

        /// <summary>
        /// 保留精度
        /// </summary>
        /// <param name="number">转换的数字</param>
        /// <param name="decimals">小数位数</param>
        /// <param name="numberChoiceType"></param>
        public static double Choice(double number, int decimals = 4, NumberChoiceTypeEnum numberChoiceType = NumberChoiceTypeEnum.AwayFromZero)
        {
            return Math.Round(number, decimals, (MidpointRounding)(int)numberChoiceType);
        }
    }
}
