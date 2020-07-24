#region << 文 件 说 明 >>

/*----------------------------------------------------------------
// 文件名称：IEkIocContainer
// 创 建 者：IceInk
// 创建时间：2020/07/23/星期四 15:13:57
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/

#endregion

namespace IceInk.IOC
{
    /// <summary>
    /// IOC容器接口
    /// </summary>
    public interface IEkIocContainer
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TFrom">接口类型</typeparam>
        /// <typeparam name="TTo">存储类型</typeparam>
        /// <param name="anotherName">别名(用于一个接口多个实现的区分)</param>
        /// <param name="paraArray">常量参数(用于存储构造函数参数有类型string int 等的指)</param>
        /// <param name="lifetimeType">生命周期</param>
        void Register<TFrom, TTo>(string anotherName = null, 
            object[] paraArray = null,
            EkIocLifetimeType lifetimeType = EkIocLifetimeType.Transient)
            where TTo : TFrom;

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="TFrom">获取的类型</typeparam>
        /// <returns></returns>
        TFrom Resolve<TFrom>(string anotherName = null);
    }
}