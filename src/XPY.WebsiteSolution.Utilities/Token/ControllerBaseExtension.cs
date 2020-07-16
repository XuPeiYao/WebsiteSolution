using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Utilities.Token
{
    /// <summary>
    /// MVC控制器擴充方法
    /// </summary>
    public static class ControllerBaseExtension
    {
        /// <summary>
        /// 取得JWT資訊
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <returns>JWT資訊</returns>
        public static TJwtTokenModel GetJwtTokenModel<TJwtTokenModel>(this ControllerBase controller)
        {
            if (controller.Request.Headers.TryGetValue("Authorization", out StringValues auth))
            {
                var jwt = controller.HttpContext.RequestServices.GetService<JwtHelper<TJwtTokenModel>>();
                return jwt.DecodeJwt(auth.ToString());
            }

            return default(TJwtTokenModel);
        }
    }
}
