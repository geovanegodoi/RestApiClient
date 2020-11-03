using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestApiClient
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string ToQueryString(this Dictionary<string, string> value)
        {
            var queryString = "";

            foreach (var item in value)
            {
                if (!queryString.IsNullOrEmpty())
                {
                    queryString += "&";
                }
                queryString += $"{item.Key}={item.Value}";
            }
            return $"?{queryString}";
        }

        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
