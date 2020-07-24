#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：EkIocInjectPropAttribute
// 创 建 者：IceInk
// 创建时间：2020/07/23/星期四 15:20:06
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		用于给常量参数标记，指定IOC注入的常量参数
//      AttributeUsage():标记此特性的使用
//      AttributeTargets.Parameter:标记此特性只能用于方法的参数
//----------------------------------------------------------------*/
#endregion

using System;

namespace IceInk.IOC
{
    /// <summary>
    /// 用于给常量参数标记
    /// 指定IOC注入的常量参数
    /// 不能重复标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class EkIocInjectParameterAttribute : Attribute
    {
        
    }
}
