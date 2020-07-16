using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Utilities.Extensions.DependencyInjection.CycleDependent
{
    /// <summary>
    /// 循環相依注入包裝類型
    /// </summary>
    /// <typeparam name="T">相依注入類型</typeparam>
    public class Cycle<T>
    {
        /// <summary>
        /// 注入類型實例緩載入
        /// </summary>
        private Lazy<T> _instance = null;

        /// <summary>
        /// 注入類型實例
        /// </summary>
        public T Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        /// <summary>
        /// 初始化 <see cref="Cycle{T}"/>
        /// </summary>
        /// <param name="instance">注入類型實例</param>
        public Cycle(T instance)
        {
            _instance = new Lazy<T>(() => {
                return instance;
            });
        }

        /// <summary>
        /// 初始化 <see cref="Cycle{T}"/>
        /// </summary>
        /// <param name="serviceProvider">服務容器</param>
        public Cycle(IServiceProvider serviceProvider)
        {
            _instance = new Lazy<T>(() => {
                return (T)serviceProvider.GetService(typeof(T));
            });
        }

        /// <summary>
        /// 顯式轉換為 <see cref="T"/> 類型的實例
        /// </summary>
        /// <param name="cycle"><see cref="Cycle{T}"/>實例</param>
        public static implicit operator T(Cycle<T> cycle)
        {
            return cycle.Instance;
        }
    }
}
