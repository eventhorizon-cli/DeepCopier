using System;
using System.Collections.Generic;
using System.Text;

namespace DeepCopier
{
    /// <summary>
    /// 利用表达式树实现深拷贝的类
    /// </summary>
    public static class Copier
    {
        /// <summary>
        /// 新建目标类型实例，并将源对象的属性值拷贝至目标对象的对应属性
        /// </summary>
        /// <typeparam name="TSource">源对象类型</typeparam>
        /// <typeparam name="TTarget">目标对象类型</typeparam>
        /// <param name="source">源对象实例</param>
        /// <returns>深拷贝了源对象属性的目标对象实例</returns>
        public static TTarget Copy<TSource, TTarget>(TSource source)
            => Copier<TSource, TTarget>.Copy(source);

        /// <summary>
        /// 对指定对象进行深拷贝
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>深拷贝的对象实例</returns>
        public static T Copy<T>(T source) => Copier<T, T>.Copy(source);
    }
}
