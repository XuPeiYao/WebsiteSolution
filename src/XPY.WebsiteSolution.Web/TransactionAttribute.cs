using AspectCore.DynamicProxy;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace XPY.WebsiteSolution.Web
{
    public class TransactionAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            using (var transaction = new TransactionScope()) 
            {
                await next(context);
                transaction.Complete();
            }
        }
    }
}
