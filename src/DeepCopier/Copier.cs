namespace DeepCopier
{
    /// <summary>
    /// The class that can deep copy object by Expression Tree
    /// 利用表达式树实现深拷贝的类
    /// </summary>
    public static class Copier
    {
        /// <summary>
        /// Create a new instance of the target type,
        /// and deep copy the property values of the given source object into the target instance
        /// 新建目标类型实例，并将源对象的属性值拷贝至目标对象的对应属性
        /// </summary>
        /// <typeparam name="TSource">The type of source object 源对象类型</typeparam>
        /// <typeparam name="TTarget">The type of target object 目标对象类型</typeparam>
        /// <param name="source">The source object 源对象实例</param>
        /// <returns>
        /// A new instance of the target type
        /// 深拷贝了源对象属性的目标对象实例
        /// </returns>
        public static TTarget Copy<TSource, TTarget>(TSource source)
            => Copier<TSource, TTarget>.Copy(source);

        /// <summary>
        /// Deep copy the source object
        /// 对源对象进行深拷贝
        /// </summary>
        /// <typeparam name="T">The type of source obejct 对象类型</typeparam>
        /// <param name="source">The source obejct 源对象</param>
        /// <returns>
        /// A deep copied instance of source obejct
        /// 深拷贝的对象实例
        /// </returns>
        public static T Copy<T>(T source) => Copier<T, T>.Copy(source);

        /// <summary>
        /// Copy the property values of the given source object into the existing target object
        /// 将源对象的属性值拷贝至已存在的目标对象的对应属性
        /// </summary>
        /// <typeparam name="TSource">The type of source object 源对象类型</typeparam>
        /// <typeparam name="TTarget">The type of target object 目标对象类型</typeparam>
        /// <param name="source">The source object 源对象实例</param>
        /// <param name="target">The target object 目标对象实例</param>
        public static void Copy<TSource, TTarget>(TSource source, TTarget target)
            => Copier<TSource, TTarget>.Copy(source, target);
    }
}
