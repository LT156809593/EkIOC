#region << 文 件 说 明 >>

/*----------------------------------------------------------------
// 文件名称：EkIocContainerRegisterModel
// 创 建 者：IceInk
// 创建时间：2020年07月24日 星期五 14:21
// 文件版本：V1.0.0
//===============================================================
// 功能描述：
//		IOC注册模型
//
//----------------------------------------------------------------*/

#endregion

using System;

namespace IceInk.IOC
{
    /// <summary>
    /// IOC注册模型
    /// </summary>
    public class EkIocContainerRegisterModel
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// 生命周期
        /// </summary>
        public EkIocLifetimeType Lifetime { get; set; }

        /// <summary>
        /// 存储单例对象
        /// </summary>
        public object SingleTonInstance { get; set; }
    }
}