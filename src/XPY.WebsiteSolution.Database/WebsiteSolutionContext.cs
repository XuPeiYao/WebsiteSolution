using LinqToDB.Data;
using LinqToDB.DataProvider;

using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Database
{
    /// <summary>
    /// DAO
    /// </summary>
    public class WebsiteSolutionContext : DataConnection
    {

        //public ITable<SampleTableDao> SampleTableDao => GetTable<SampleTableDao>();

        /// <summary>
        /// 初始化 <see cref="WebsiteSolutionContext"/>
        /// </summary>
        /// <param name="dataProvider">資料提供者</param>
        /// <param name="connectionString">資料庫連線字串</param>
        public WebsiteSolutionContext(IDataProvider dataProvider, string connectionString)
            : base(dataProvider, connectionString)
        {
        }
    }
}
