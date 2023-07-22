using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoreLibrary.Core
{
    /// <summary>
    /// 可空日期格式转换器
    /// </summary>
    public class DateTimeNullableConverter : JsonConverter<DateTime?>
    {
        /// <summary>
        /// DateTime? 读取
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.TryParse(reader.GetString(), out var dateTime) ? dateTime : default(DateTime?);
        }
        /// <summary>
        /// DateTime? 写入
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
