using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DeepCopier
{
    /// <summary>
    /// 利用表达式树实现深拷贝的类
    /// </summary>
    /// <typeparam name="TSource">源对象类型</typeparam>
    /// <typeparam name="TTarget">目标对象类型</typeparam>
    internal static class Copier<TSource, TTarget>
    {
        // 缓存委托
        private static Func<TSource, TTarget> _copy;

        /// <summary>
        /// 新建目标类型实例，并将源对象的属性值拷贝至目标对象的对应属性
        /// </summary>
        /// <param name="source">源对象实例。</param>
        /// <returns>深拷贝了源对象属性的目标对象实例</returns>
        public static TTarget Copy(TSource source)
        {
            if (source == null) return default(TTarget);

            // 因为对于泛型类型而言，每次传入不同的泛型参数都会调用静态构造函数，所以可以通过这种方式进行缓存
            if (_copy != null)
            {
                // 如果之前缓存过，则直接调用缓存的委托
                return _copy(source);
            }
            else
            {
                Type sourceType = typeof(TSource);
                Type targetType = typeof(TTarget);

                var paramExpr = Expression.Parameter(sourceType, nameof(source));

                Expression bodyExpr = null;

                // 如果对象属性可以遍历（目前只支持数组和List）
                if (sourceType == targetType && Utils.IsIEnumerableExceptString(sourceType))
                {
                    bodyExpr = Expression.Call(null, EnumerableCopier.GetMethondInfo(sourceType), paramExpr);
                }
                else
                {
                    var memberBindings = new List<MemberBinding>();
                    // 遍历目标对象的所有属性信息
                    foreach (var targetPropInfo in targetType.GetProperties())
                    {
                        // 从源对象获取同名的属性信息
                        var sourcePropInfo = sourceType.GetProperty(targetPropInfo.Name);

                        Type sourcePropType = sourcePropInfo?.PropertyType;
                        Type targetPropType = targetPropInfo.PropertyType;

                        // 只在满足以下三个条件的情况下进行拷贝
                        // 1.源属性类型和目标属性类型一致
                        // 2.源属性可读
                        // 3.目标属性可写
                        if (sourcePropType == targetPropType
                            && sourcePropInfo.CanRead
                            && targetPropInfo.CanWrite)
                        {
                            // 获取属性值的表达式
                            Expression expression = Expression.Property(paramExpr, sourcePropInfo);

                            // 如果目标属性是值类型或者字符串，则直接做赋值处理
                            // 暂不考虑目标值类型有非字符串的引用类型这种特殊情况
                            // 非字符串引用类型做递归处理
                            if (Utils.IsRefTypeExceptString(targetPropType))
                            {
                                // 进行递归
                                var method = typeof(Copier<,>)
                                    .MakeGenericType(sourcePropType, targetPropType)
                                    .GetMethod(nameof(Copy));
                                expression = Expression.Call(null, method, expression);
                            }
                            memberBindings.Add(Expression.Bind(targetPropInfo, expression));
                        }
                    }

                    bodyExpr = Expression.MemberInit(Expression.New(targetType), memberBindings);
                }

                var lambdaExpr
                    = Expression.Lambda<Func<TSource, TTarget>>(bodyExpr, paramExpr);

                _copy = lambdaExpr.Compile();
                return _copy(source);
            }
        }
    }
}
