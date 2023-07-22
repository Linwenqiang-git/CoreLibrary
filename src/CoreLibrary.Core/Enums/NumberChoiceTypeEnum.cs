namespace CoreLibrary.Core
{
    public enum NumberChoiceTypeEnum
    {
        /// <summary>
        /// 四舍六入五成双
        /// </summary>
        ToEven,

        /// <summary>
        /// 四舍五入
        /// </summary>
        AwayFromZero,

        /// <summary>
        /// 向下去尾法
        /// </summary>
        ToZero,

        /// <summary>
        /// 向下舍入法
        /// </summary>
        ToNegativeInfinity,

        /// <summary>
        /// 向上进一法
        /// </summary>
        ToPositiveInfinity
    }
}
