using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DeepCopier
{
    /// <summary>
    /// 可遍历类型拷贝器
    /// </summary>
    internal class EnumerableCopier
    {
        private static MethodInfo _copyArrayMethodInfo { get; }

        private static MethodInfo _copyListMethodInfo { get; }

        private static string _listTypeFullName = typeof(List<>).FullName.TrimEnd('1');

        static EnumerableCopier()
        {
            Type type = typeof(EnumerableCopier);
            _copyArrayMethodInfo = type.GetMethod(nameof(CopyArray));
            _copyListMethodInfo = type.GetMethod(nameof(CopyList));
        }

        public static MethodInfo GetMethondInfo(Type type)
        {
            if (type.IsArray)
            {
                return _copyArrayMethodInfo.MakeGenericMethod(type.GetElementType());
            }
            else if (type.FullName.StartsWith(_listTypeFullName))
            {
                return _copyListMethodInfo.MakeGenericMethod(type.GetGenericArguments()[0]);

            }
            throw new UnsupportedTypeException($"Type[{type.Name}] has not been supported yet.");
        }


        /// <summary>
        /// 拷贝List
        /// </summary>
        /// <typeparam name="TElement">源List元素类型</typeparam>
        /// <param name="list">源List</param>
        /// <returns>深拷贝完成的List</returns>
        public static List<TElement> CopyList<TElement>(List<TElement> list)
        {
            List<TElement> result = new List<TElement>();

            if (Utils.IsRefTypeExceptString(typeof(TElement)))
            {
                foreach (TElement item in list)
                {
                    result.Add(Copier<TElement, TElement>.Copy(item));
                }
            }
            else
            {
                foreach (TElement item in list)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 拷贝数组
        /// </summary>
        /// <typeparam name="TElement">源数组元素类型</typeparam>
        /// <param name="array">源List</param>
        /// <returns>深拷贝完成的数组</returns>
        public static TElement[] CopyArray<TElement>(TElement[] array)
        {
            TElement[] result = new TElement[array.Length];
            if (Utils.IsRefTypeExceptString(typeof(TElement)))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    result[i] = Copier<TElement, TElement>.Copy(array[i]);
                }
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    result[i] = array[i];
                }
            }
            return result;
        }
    }
}
