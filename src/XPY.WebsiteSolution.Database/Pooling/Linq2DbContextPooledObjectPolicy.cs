using LinqToDB.DataProvider;
using LinqToDB.DataProvider.PostgreSQL;

using Microsoft.Extensions.ObjectPool;

using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Database.Pooling
{
    public class Linq2DbContextPooledObjectPolicy : IPooledObjectPolicy<WebsiteSolutionContext>
    {
        public IDataProvider DataProvider { get; private set; } 
        public string ConnectionString { get; private set; }

        public Linq2DbContextPooledObjectPolicy(
            IDataProvider dataProvider,
            string connectionString)
        {
            DataProvider = dataProvider;
            ConnectionString = connectionString;
        }

        public WebsiteSolutionContext Create()
        {
            try
            {
                return new WebsiteSolutionContext(
                    DataProvider,
                    ConnectionString
                );
            }
            catch
            {
                return null;
            }
        }

        public bool Return(WebsiteSolutionContext obj)
        {
            return true;
        }
    }
}
