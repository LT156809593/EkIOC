#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：EkIocInjectAnotherAttribute
// 创 建 者：IceInk
// 创建时间：2020/07/23/星期四 20:47:07
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//      别名标记 用于标记引用类型有别名构造的情况
//      AttributeTargets.Parameter ：可用于参数
//      AttributeTargets.Property  ：可用于属性
//		
//
//----------------------------------------------------------------*/
#endregion

using System;

namespace IceInk.IOC
{
    /// <summary>
    /// 别名标记   
    /// 用于参数和属性
    /// 不能重复标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class EkIocInjectAnotherAttribute : Attribute
    {
        /// <summary>
        /// 别名
        /// </summary>
        public string AnotherName { get; }

        public EkIocInjectAnotherAttribute(string anotherName)
        {
            AnotherName = anotherName;
        }
    }
}
