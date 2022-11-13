using System.Text.Json;

namespace LintCoder.RateLimiting.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJosn<T>(this T @object) where T : class
        {
            if (@object == null)
            {
                return string.Empty;
            }
            return JsonSerializer.Serialize(@object);
        }
    }
}