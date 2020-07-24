#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：EkIocInjectCtorAttribute
// 创 建 者：IceInk
// 创建时间：2020/07/23/星期四 16:26:06
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		用于给构造函数标记，指定IOC使用哪个构造函数注入
//      AttributeUsage():标记此特性的使用
//      AttributeTargets.Constructor:标记此特性只能用于构造函数
//      AllowMultiple = false ：不能重复标记
//----------------------------------------------------------------*/
#endregion

using System;

namespace IceInk.IOC
{
    /// <summary>
    /// 用于给构造函数标记
    /// 指定IOC使用哪个构造函数注入
    /// 不能重复标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public class EkIocInjectCtorAttribute : Attribute
    {
        
    }
}
