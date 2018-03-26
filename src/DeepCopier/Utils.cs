using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DeepCopier
{
    /// <summary>
    /// 工具类
    /// </summary>
    internal static class Utils
    {
        private static Type _typeString = typeof(string);

        private static Type _typeIEnumerable = typeof(IEnumerable);


        /// <summary>
        /// 判断是否是string以外的引用类型
        /// </summary>
        /// <returns></returns>
        public static bool IsRefTypeExceptString(Type type)
            => !type.IsValueType && type != _typeString;

        /// <summary>
        /// 判断是否是string以外的可遍历类型
        /// </summary>
        /// <returns></returns>
        public static bool IsIEnumerableExceptString(Type type)
            => _typeIEnumerable.IsAssignableFrom(type) && type != _typeString;
    }
}
