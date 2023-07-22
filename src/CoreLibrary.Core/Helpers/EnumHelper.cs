using System.ComponentModel;
using System.Reflection;

namespace CoreLibrary.Core
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToStr());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToStr();
        }

        /// <summary>
        /// 对象转枚举
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="para"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this object para)
            where TEnum : Enum
        {
            var tEnum = typeof(TEnum);

            return Enum.IsDefined(tEnum, para) ?
                   (TEnum)Enum.ToObject(tEnum, para) :
                   throw new Exception($"Value:{para} is not included {tEnum.Name}!");
        }
        /// <summary>
        /// 将枚举转为集合，顺便取描述
        /// </summary>
        /// <typeparam name="T">枚举类名</typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> EnumToDict<T>()
        {
            var dict = new Dictionary<string, string>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                if (e == null)
                    continue;
                string? description = null;
                object[]? objArr = e!.GetType()?.GetField(e.ToStr())?.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute? da = objArr[0] as DescriptionAttribute;
                    description = da?.Description;
                }
                if (description != null )
                {
                    dict.Add(e!.ToStr(), description);
                }
            }
            return dict;
        }
        /// <summary>
        /// 将枚举转为集合，顺便取描述
        /// </summary>
        /// <typeparam name="T">枚举类名</typeparam>
        /// <returns></returns>
        public static List<EnumModule> EnumToEntityList<T>()
        {
            var list = new List<EnumModule>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                if (e == null)
                    continue;
                var m = new EnumModule();
                object[]? objArr = e!.GetType()?.GetField(e.ToStr())?.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute? da = objArr[0] as DescriptionAttribute;
                    m.Desc = da?.Description;
                }
                m.Value = (int)e;
                m.Name = e.ToStr();
                list.Add(m);
            }
            return list;
        }
    }
}
