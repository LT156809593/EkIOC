#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：EkIocInjectPropAttribute
// 创 建 者：IceInk
// 创建时间：2020/07/23/星期四 19:26:06
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		用于给属性标记，指定IOC使用哪个属性注入
//      AttributeUsage():标记此特性的使用
//      AttributeTargets.Property:标记此特性只能用于构造函数
//----------------------------------------------------------------*/
#endregion

using System;

namespace IceInk.IOC
{
    /// <summary>
    /// 用于给属性标记
    /// 指定IOC使用哪个属性注入
    /// 不能重复标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EkIocInjectPropAttribute : Attribute
    {
        
    }
}
