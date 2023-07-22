using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace CoreLibrary.Core.Helpers
{
    /// <summary>
    /// Json操作
    /// </summary>
    public static class JsonHelper
    {

        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        public static T? JsonTo<T>(this string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            return string.IsNullOrWhiteSpace(json) ? default : JsonSerializer.Deserialize<T>(json, options);
        }

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="source">目标对象,若为string,则不做处理</param>
        /// <param name="isCamel">是否小驼峰命名</param>
        /// <param name="isConvertToSingleQuotes">是否将双引号转成单引号</param>
        /// <param name="isIgnoreNull">是否忽略空值</param>
        public static string ToJson(this object source, bool isCamel = true, bool isConvertToSingleQuotes = false, bool isIgnoreNull = false)
        {
            if (source is string)
                return source.ToStr();
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeConverter());
            options.Converters.Add(new DateTimeNullableConverter());
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All); // 中文序列化处理
            options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            if (isCamel) options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            if (isIgnoreNull) options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            var result = JsonSerializer.Serialize(source, options);
            if (isConvertToSingleQuotes)
                result = result.Replace("\"", "'");
            return result;
        }

        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        public static async Task<T?> JsonToAsync<T>(this string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };
            options.Converters.Add(new DateTimeConverter());
            options.Converters.Add(new DateTimeNullableConverter());
            //字符，数字格式兼容
            options.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString;
            using var memoryStream = new MemoryStream();
            await memoryStream.WriteAsync(Encoding.UTF8.GetBytes(json));
            memoryStream.Position = 0;
            return await JsonSerializer.DeserializeAsync<T>(memoryStream, options);
        }

        /// <summary>
        /// 将Json字符串转换为对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        public static async Task<object?> JsonToAsync(this string json, Type type)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };
            options.Converters.Add(new DateTimeConverter());
            options.Converters.Add(new DateTimeNullableConverter());
            //字符，数字格式兼容
            options.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString;
            using var memoryStream = new MemoryStream();
            await memoryStream.WriteAsync(Encoding.UTF8.GetBytes(json));
            memoryStream.Position = 0;
            return await JsonSerializer.DeserializeAsync(memoryStream, type, options);
        }
        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <param name="source">目标对象,若为string,则不做处理</param>
        /// <param name="isCamel">是否小驼峰命名</param>
        /// <param name="isConvertToSingleQuotes">是否将双引号转成单引号</param>
        /// <param name="isIgnoreNull">是否忽略空值</param>
        public static async Task<string?> ToJsonAsync(this object source, bool isCamel = true, bool isConvertToSingleQuotes = false, bool isIgnoreNull = false)
        {
            if (source == null)
            {
                return default;
            }
            if (source is string)
                return source.ToStr();

            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeConverter());
            options.Converters.Add(new DateTimeNullableConverter());
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All); // 中文序列化处理
            options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            if (isCamel) options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            if (isIgnoreNull) options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            using var memoryStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(memoryStream, source, options);
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream, Encoding.UTF8);
            var result = await streamReader.ReadToEndAsync();
            if (isConvertToSingleQuotes)
                result = result.Replace("\"", "'");
            return result;
        }
    }
}
