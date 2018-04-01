using System;
using System.Collections;
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

        private static MethodInfo _copyICollectionMethodInfo { get; }

        private static Type _typeICollection = typeof(ICollection<>);

        static EnumerableCopier()
        {
            Type type = typeof(EnumerableCopier);
            _copyArrayMethodInfo = type.GetMethod(nameof(CopyArray));
            _copyICollectionMethodInfo = type.GetMethod(nameof(CopyICollection));
        }

        public static MethodInfo GetMethondInfo(Type type)
        {
            if (type.IsArray)
            {
                return _copyArrayMethodInfo.MakeGenericMethod(type.GetElementType());
            }
            else if (type.GetGenericArguments().Length > 0)
            {
                Type elementType = type.GetGenericArguments()[0];
                if (_typeICollection.MakeGenericType(elementType).IsAssignableFrom(type))
                {
                    return _copyICollectionMethodInfo.MakeGenericMethod(type, elementType);

                }
            }
            throw new UnsupportedTypeException($"Type[{type.Name}] has not been supported yet.");
        }


        /// <summary>
        /// 拷贝List
        /// </summary>
        /// <typeparam name="TElement">源ICollection元素类型</typeparam>
        /// <param name="list">源ICollection对象</param>
        /// <returns>深拷贝完成的ICollection<对象/returns>
        public static T CopyICollection<T, TElement>(T source)
            where T : ICollection<TElement>
        {
            T result = (T)Utils.CreateNewInstance(source.GetType());

            if (Utils.IsRefTypeExceptString(typeof(TElement)))
            {
                foreach (TElement item in source)
                {
                    result.Add(Copier<TElement, TElement>.Copy(item));
                }
            }
            else
            {
                foreach (TElement item in source)
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
