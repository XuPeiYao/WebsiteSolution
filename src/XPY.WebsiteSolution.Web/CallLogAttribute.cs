using AspectCore.DynamicProxy;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XPY.WebsiteSolution.Web
{
    public class CallLogAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            Console.WriteLine("Before");
            await next(context);
            Console.WriteLine("After");
        }
    }
}
