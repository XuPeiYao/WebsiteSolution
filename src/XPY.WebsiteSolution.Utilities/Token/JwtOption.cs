using System;
using System.Collections.Generic;
using System.Text;

namespace XPY.WebsiteSolution.Utilities.Token
{
    /// <summary>
    /// JWT選項
    /// </summary>
    public class JwtOption
    {
        /// <summary>
        /// 發行者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 密鑰
        /// </summary>
        public string SecureKey { get; set; }
    }
}
