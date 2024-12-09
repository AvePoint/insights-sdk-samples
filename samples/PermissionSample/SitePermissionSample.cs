namespace Insights.Sdk.Samples.PermissionSample
{
    #region using directives
    using Insights.Client;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    #endregion
    public class SitePermissionSample
    {
        /// <summary>
        /// Get site Permission Json
        /// </summary>
        public async Task<List<SitePermissionViewModule>> GetSitePermisionAsync(InsightsApiClient insightsClient)
        {
            List<SitePermissionViewModule> sitePermissionsViews = new List<SitePermissionViewModule>();
            //Init Query Token
            string token = null;
            do
            {
                SitePermissionViewModuleOptions userAccessOptions = GetRequestOption(token);
                ResultListOfSitePermissionViewModule sitePermissionViewModuleResultList = await insightsClient.Site_GetSitePermissionAsync(userAccessOptions);
                if (sitePermissionViewModuleResultList != null && sitePermissionViewModuleResultList.Results != null)
                {
                    sitePermissionsViews.AddRange(sitePermissionViewModuleResultList.Results);
                }
                token = sitePermissionViewModuleResultList?.NextToken;
            } while (token != null);
            return sitePermissionsViews;
        }

        /// <summary>
        /// Set the request information
        /// </summary>
        /// <param name="token"></param>
        /// <param name="top"></param>
        /// <returns>SitePermissionViewModuleOptions</returns>
        private SitePermissionViewModuleOptions GetRequestOption(string token, int top = 100)
        {
            SitePermissionViewModuleOptions sitePermissionViewModuleOptions = new SitePermissionViewModuleOptions();
            #region Required
            //Sets the URLs of site collections for which you want to get the permission related information. 100 URLs at most.
            sitePermissionViewModuleOptions.SiteUrls = new List<string>()
            {
                "https://xxxx.sharepoint.com/sites/sample1",
                "https://xxxx.sharepoint.com/sites/sample2"
            };
            //Sets the number of results for one page. 100 results on one page at most.
            sitePermissionViewModuleOptions.Top = top;
            //Sets whether to get the remaining results of a request of which the results are more than 100.
            sitePermissionViewModuleOptions.Token = token;
            ////Sets the language of the report you are about to export.Default: en-US Support: en-US/ja-JP/fr-FR
            sitePermissionViewModuleOptions.Language = "en-US";
            #endregion

            return sitePermissionViewModuleOptions;
        }
    }
}
