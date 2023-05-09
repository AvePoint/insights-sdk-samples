using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Sdk.Samples
{
    public static class CoreConstant
    {
        /// <summary>
        /// When you use the Insights for Microsoft 365 Web API, you will need the Web API URL. The Web API URL varies with your data center. Choose the Web API URL according to your data center.
        /// https://cdn.avepoint.com/assets/webhelp/insights-for-microsoft-365/index.htm#!Documents/getstarted1.htm
        /// </summary>
        public static string BaseUrl = "https://graph-us.avepointonlineservices.com/insights";
        /// <summary>
        ///  get access token  using IdentityServiceUrl.
        /// https://cdn.avepoint.com/assets/webhelp/insights-for-microsoft-365/index.htm#!Documents/getstarted1.htm
        /// </summary>
        public static string IdentityServiceUrl = "https://graph-us.avepointonlineservices.com";
    }
}
