using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XPY.WebsiteSolution.Utilities
{
    /// <summary>
    /// 類型工具
    /// </summary>
    public static class TypeUtility
    {
        /// <summary>
        /// 取得指定命名空間內的公開類型
        /// </summary>
        /// <param name="ns">命名空間</param>
        /// <returns>類型陣列</returns>
        public static Type[] GetTypesFromNamespace(string ns)
        {
            return GetAllTypes()
                .Where(x => x.Namespace == ns)
                .ToArray();
        }

        /// <summary>
        /// 取得所有類型
        /// </summary>
        /// <returns>類型陣列</returns>
        public static Type[] GetAllTypes()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetReferencedAssemblies())
                .Select(Assembly.Load)
                .SelectMany(x => x.ExportedTypes)
                .ToArray();
        }
    }
}
