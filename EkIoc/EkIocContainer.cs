#region << 文 件 说 明 >>

/*----------------------------------------------------------------
// 文件名称：EkIocContainer
// 创 建 者：IceInk
// 创建时间：2020/07/23/星期四 14:56:37
// 文件版本：V1.0.0
// ===============================================================
//《依赖注入》：
//      在构建A对象的时候，需要依赖B对象，那么久先构造B对象并传入；
//      也就是在构造对象的时候，把对象的依赖初始化并传入进去，就是依赖注入。
//      IOC 可以说是一种设计模式，DI是IOC的一种实现方式
// 
    功能描述：
//		支持无限层级的依赖注入，单接口多实现
//      一：构造函数注入
//          1、支持构造函数多参数注入
//          2、支持选择指定构造函数注入，指定的构造函数用特性 EkIocInjectCtorAttribute 标记
//          3、支持构造函数常量参数注入，列如：参数类型是string int float 等类型，
//               常量参数用特性 EkIocInjectParameterAttribute 标记
//      二：属性注入
//          1、支持选择指定的属性注入，需要注入的属性用特性 EkIocInjectPropAttribute 标记
//
//      三：方法注入
//          1、支持选择指定的方法注入，需要注入的方法用特性 EkIocInjectMethodAttribute 标记
//
//      四：生命周期
//          1、瞬时  每次需要对象的时候都重新创建一个
//          2、单例  全局使用同一个实例对象
//          3、作用域单例  一次请求一个单例
//                每次来一个请求，就clone一个，或者叫创建子容器(包含注册关系)，然后一个请求就一个子容器实例，
//                那么久可以做到请求单例，也就是子容器单例，
//     TODO 4、线程单例 相同线程中使用同一个单例
//
//----------------------------------------------------------------*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IceInk.IOC
{
    /// <summary>
    /// 依赖注入用来生成对象
    /// </summary>
    public class EkIocContainer : IEkIocContainer
    {
        /// <summary>
        /// 存储注册的对象
        /// </summary>
        private readonly Dictionary<string, EkIocContainerRegisterModel> mIocContainerDic =
            new Dictionary<string, EkIocContainerRegisterModel>();

        /// <summary>
        /// 存储构造函数常量的值
        /// </summary>
        private readonly Dictionary<string, object[]> mIocParaDic = new Dictionary<string, object[]>();

        /// <summary>
        /// 存储作用域单例
        /// </summary>
        private readonly Dictionary<string, object> mIocScopeDic = new Dictionary<string, object>();

        /// <summary>
        /// 创建子容器
        /// 用于作用域单例存储
        /// </summary>
        /// <returns></returns>
        public IEkIocContainer CreateChildContainer()
        {
            return new EkIocContainer(this.mIocContainerDic, this.mIocParaDic, new Dictionary<string, object>());
        }
        /// <summary>
        /// 公开构造函数
        /// </summary>
        public EkIocContainer()
        {
        }

        /// <summary>
        /// 私有化构造函数
        /// 用于创建作用域单例的IOC容器
        /// </summary>
        /// <param name="containerDic"></param>
        /// <param name="paraDic"></param>
        /// <param name="scopeDic"></param>
        private EkIocContainer(Dictionary<string, EkIocContainerRegisterModel> containerDic,
            Dictionary<string, object[]> paraDic, Dictionary<string, object> scopeDic)
        {
            //创建子容器 
            this.mIocContainerDic = containerDic;
            this.mIocParaDic = paraDic;
            this.mIocScopeDic = scopeDic;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TFrom">接口类型</typeparam>
        /// <typeparam name="TTo">存储类型</typeparam>
        /// <param name="anotherName">别名(用于一个接口多个实现的区分)</param>
        /// <param name="paraArray">常量参数(用于存储构造函数参数有类型string int 等的指)</param>
        /// <param name="lifetimeType">生命周期</param>
        public void Register<TFrom, TTo>(string anotherName = null, object[] paraArray = null,
            EkIocLifetimeType lifetimeType = EkIocLifetimeType.Transient)
            where TTo : TFrom
        {
            //创建注册KEY
            string key = GetKey(typeof(TFrom).FullName, anotherName);
            //创建注册Model
            EkIocContainerRegisterModel rModel = new EkIocContainerRegisterModel()
            {
                TargetType = typeof(TTo),
                Lifetime = lifetimeType,
            };
            //添加进字典
            mIocContainerDic.Add(key, rModel);
            //如果有常量变量 则把常量变量的值存储
            if (paraArray != null && paraArray.Length > 0)
                mIocParaDic.Add(key, paraArray);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <param name="anotherName">别名(用于单接口多实现区分)</param>
        /// <returns></returns>
        public TFrom Resolve<TFrom>(string anotherName = null)
        {
            //获取
            return (TFrom) ResolveObject(typeof(TFrom), anotherName);
        }

        private object ResolveObject(Type sourceType, string anotherName = null)
        {
            string key = GetKey(sourceType.FullName, anotherName); //获取Key
            EkIocContainerRegisterModel model = mIocContainerDic[key]; //获取Model

            #region 根据生命周期获取缓存里面的对象

            switch (model.Lifetime) //判断生命周期
            {
                case EkIocLifetimeType.Transient: //瞬时  啥也不用干 直接去创建
                    break;
                case EkIocLifetimeType.Singleton: //单例
                    if (model.SingleTonInstance == null) break;
                    else return model.SingleTonInstance;
                case EkIocLifetimeType.Scope: //作用域单例
                    if (mIocScopeDic.ContainsKey(key))
                        return mIocScopeDic[key];
                    break;
                case EkIocLifetimeType.PerThread: //线程单例
                    throw new ArgumentOutOfRangeException("能力有限，线程单例还没有支持！！");
                default:
                    throw new ArgumentOutOfRangeException();
            }

            #endregion

            Type type = model.TargetType; //获取类型

            #region 一、 构造函数注入

            //获取所有构造函数
            ConstructorInfo[] ctorInfoArray = type.GetConstructors();
            // 先去查找被特性 EkIocConstructorAttribute 标记的构造函数
            ConstructorInfo ctor = ctorInfoArray
                .FirstOrDefault(c => c.IsDefined(typeof(EkIocInjectCtorAttribute), true));
            if (ctor == null)
            {
                //如果没有被特性标记的构造函数
                // 使用构造函数参数类型超集  参数个数最多的
                ctor = ctorInfoArray.OrderBy(c => c.GetParameters().Length).FirstOrDefault();
            }

            //获取并准备构造函数的参数
            List<object> paraList = new List<object>();
            object[] paraObjectArray = mIocParaDic.ContainsKey(key) ? mIocParaDic[key] : null; //获取常量参数的值
            int index = 0;
            foreach (ParameterInfo para in ctor.GetParameters())
            {
                if (para.IsDefined(typeof(EkIocInjectParameterAttribute), true))
                {
                    if (paraObjectArray != null) paraList.Add(paraObjectArray[index]);
                    index++;
                }
                else
                {
                    Type paraType = para.ParameterType; //获取参数的类型
                    string aName = GetAnotherName(para); //获取标记的别名
                    object paraInstance = ResolveObject(paraType, aName); //递归获取创建引用的类型
                    paraList.Add(paraInstance);
                }
            }

            #endregion

            //创建对象
            object oInstance = Activator.CreateInstance(type, paraList.ToArray());

            #region 二、 属性注入

            //获取被特性 EkIocInjectPropAttribute 标记的属性
            PropertyInfo[] propInfoArray = type.GetProperties()
                .Where(p => p.IsDefined(typeof(EkIocInjectPropAttribute), true)).ToArray();
            foreach (PropertyInfo prop in propInfoArray)
            {
                Type propType = prop.PropertyType; //获取属性的类型
                string aName = GetAnotherName(prop); //获取标记的别名
                object propInstance = ResolveObject(propType, aName); //递归获取创建属性引用的类型

                prop.SetValue(oInstance, propInstance); //把创建的属性类型指定给创建的类型
            }

            #endregion


            #region 三、方法注入

            //获取被特性 EkIocInjectMethodAttribute 标记的方法
            MethodInfo[] methodInfoArray = type.GetMethods()
                .Where(m => m.IsDefined(typeof(EkIocInjectMethodAttribute), true)).ToArray();
            foreach (MethodInfo method in methodInfoArray)
            {
                List<object> methodParaList = new List<object>();
                foreach (ParameterInfo methodPara in method.GetParameters())
                {
                    Type paraType = methodPara.ParameterType; //获取参数的类型
                    string aName = GetAnotherName(methodPara); //获取标记的别名
                    object paraInstance = ResolveObject(paraType, aName); //递归获取创建引用的类型
                    methodParaList.Add(paraInstance);
                }

                method.Invoke(oInstance, methodParaList.ToArray()); //执行方法
            }

            #endregion

            #region 根据生命周期 把对象添加进缓存

            switch (model.Lifetime) //判断生命周期
            {
                case EkIocLifetimeType.Transient: //瞬时  啥也不用干 直接去创建
                    break;
                case EkIocLifetimeType.Singleton: //单例
                    model.SingleTonInstance = oInstance; //存储单例对象
                    break;
                case EkIocLifetimeType.Scope: //作用域单例
                    mIocScopeDic[key] = oInstance; //存储在作用域单例字典中
                    break;
                case EkIocLifetimeType.PerThread://线程单例
                    throw new ArgumentOutOfRangeException("能力有限，线程单例还没有支持！！");
                default:
                    throw new ArgumentOutOfRangeException();
            }

            #endregion

            return oInstance;
        }

        /// <summary>
        /// 获取Key
        /// </summary>
        /// <param name="typeFullName">类型完整名称</param>
        /// <param name="anotherName">别名</param>
        /// <returns></returns>
        private static string GetKey(string typeFullName, string anotherName)
        {
            //拼接Key 用=*=拼接  防止重名
            string key = string.Format("{0}=*={1}", typeFullName, anotherName);
            return key;
        }

        /// <summary>
        /// 获取标记的别名
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private static string GetAnotherName(ICustomAttributeProvider provider)
        {
            Type anotherType = typeof(EkIocInjectAnotherAttribute);
            if (provider.IsDefined(anotherType, true))
            {
                EkIocInjectAnotherAttribute anotherAttribute =
                    (EkIocInjectAnotherAttribute) provider.GetCustomAttributes(anotherType, true)[0];
                return anotherAttribute.AnotherName;
            }

            return string.Empty;
        }
    }
}