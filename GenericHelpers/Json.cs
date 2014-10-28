using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericHelpers
{
    public static class Json<T> where T : class
    {
        public static string ListToJson(List<T> list)
        {
            var properties = typeof(T).GetProperties();
            
            Func<T, JObject> objToJson = 
                obj => new JObject(properties.Select(propInfo => PropertyToJProperty(propInfo, obj)));

            return JsonConvert.SerializeObject(new JArray(list.Select(objToJson)));
        }

        private static JProperty PropertyToJProperty(PropertyInfo propInfo, T obj)
        {
            return new JProperty(propInfo.Name, propInfo.GetValue(obj));
        }
    }
}
