#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：EkIocInjectMethodAttribute
// 创 建 者：IceInk
// 创建时间：2020/07/23/星期四 18:21:06
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		用于给方法标记，指定IOC使用哪个方法注入
//      AttributeUsage():标记此特性的使用
//      AttributeTargets.Method:标记此特性只能用于函数
//----------------------------------------------------------------*/
#endregion

using System;

namespace IceInk.IOC
{
    /// <summary>
    /// 用于给方法标记
    /// 指定IOC使用哪个方法注入
    /// 不能重复标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EkIocInjectMethodAttribute : Attribute
    {
        
    }
}
