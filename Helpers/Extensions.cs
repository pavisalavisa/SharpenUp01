using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicKeywordShowcase.Helpers
{
    public static class Extensions
    {
        public static bool IsEnumerable(this Type type)
        {
            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)) && type != typeof(string);
        }

        public static object ListOf<T>(this IEnumerable<T> obj, Type type)
        {
            var appropriateTypeList = typeof(Enumerable).GetMethod("Cast")?.MakeGenericMethod(type).Invoke(obj, new object[] { obj });

            return typeof(Enumerable).GetMethod("ToList")?.MakeGenericMethod(type).Invoke(appropriateTypeList, new[] { appropriateTypeList });
        }

        public static bool IsSimpleType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsSimpleType(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive
                   || type.IsEnum
                   || type == typeof(string)
                   || type == typeof(decimal);
        }
    }
}
