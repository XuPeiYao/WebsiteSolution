using Castle.DynamicProxy;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XPY.WebsiteSolution.Web
{
    public class CallLogger : IInterceptor
    {        
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("Before");
            invocation.Proceed();
            Console.WriteLine("After");
        }
    }
}
