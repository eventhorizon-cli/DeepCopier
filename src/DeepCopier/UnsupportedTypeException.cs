using System;
using System.Collections.Generic;
using System.Text;

namespace DeepCopier
{
    /// <summary>
    /// 对尚不支持的类型进行拷贝时抛出的异常
    /// </summary>
    public class UnsupportedTypeException : Exception
    {
        /// <summary>
        /// 用指定的错误消息初始化 DeepCopier.UnsupportedTypeException 类的新实例
        /// </summary>
        /// <param name="msg">描述错误的消息</param>
        public UnsupportedTypeException(string msg) : base(msg)
        {
        }
    }
}
