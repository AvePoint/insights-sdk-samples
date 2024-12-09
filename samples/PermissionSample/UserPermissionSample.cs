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
    public class UserPermissionSample
    {
        /// <summary>
        /// Get user Permission Json
        /// </summary>
        public async Task<List<AccessViewModule>> GetUserPermisionAsync(InsightsApiClient insightsClient)
        {
            List<AccessViewModule> accessViews = new List<AccessViewModule>();
            //Init Query Token
            string token = null;
            do
            {
                AccessViewModuleOptions userAccessOptions = GetRequestOption(token);
                ResultListOfAccessViewModule accessViewModuleResultList = await insightsClient.User_GetPermissionAsync(userAccessOptions);
                if (accessViewModuleResultList != null && accessViewModuleResultList.Results != null)
                {
                    accessViews.AddRange(accessViewModuleResultList.Results);
                }
                token = accessViewModuleResultList?.NextToken;
            } while (token != null);
            return accessViews;
        }

        /// <summary>
        /// Set the request information
        /// </summary>
        /// <param name="token"></param>
        /// <param name="top"></param>
        /// <returns>AccessViewModuleOptions</returns>
        private AccessViewModuleOptions GetRequestOption(string token,int top = 100)
        {
            AccessViewModuleOptions userAccessOptions = new AccessViewModuleOptions();
            #region Required
            //Sets the URLs of site collections for which you want to get the permission related information. 100 URLs at most.
            userAccessOptions.SiteUrls = new List<string>()
            {
                "https://xxxx.sharepoint.com/sites/sample1",
                "https://xxxx.sharepoint.com/sites/sample2"
            };
            //Sets the email addresses of users for which you want to export the permission report. 100 email addresses at most.
            userAccessOptions.Emails = new List<string>()
            {
                "xxxx@sample.com",
                "xxxx@sample.com"
            };
            //Sets the number of results for one page. 100 results on one page at most.
            userAccessOptions.Top = top;
            //Sets whether to get the remaining results of a request of which the results are more than 100.
            userAccessOptions.Token = token;
            ////Sets the language of the report you are about to export.Default: en-US Support: en-US/ja-JP/fr-FR
            userAccessOptions.Language = "en-US";
            #endregion

            return userAccessOptions;
        }
    }
}
