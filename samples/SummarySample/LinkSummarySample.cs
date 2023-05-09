namespace Insights.Sdk.Samples.SummarySample
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
    public class LinkSummarySample
    {
        /// <summary>
        /// Get link summary Json
        /// </summary>
        public async Task<List<LinkViewModule>> GetLinkSummaryAsync(InsightsApiClient insightsClient)
        {
            List<LinkViewModule> linkSummaryViews = new List<LinkViewModule>();
            //Init Query Token
            string token = null;
            do
            {
                LinkViewModuleOptions linkViewModuleOptions = GetRequestOption(token);
                LinkViewModuleResultList linkViewModuleResultList = await insightsClient.GetLinksSummaryAsync(linkViewModuleOptions);
                if (linkViewModuleResultList != null && !linkViewModuleResultList.Results.IsNullOrEmpty())
                {
                    linkSummaryViews.AddRange(linkViewModuleResultList.Results);
                }
                token = linkViewModuleResultList?.NextToken;
            } while (token != null);
            return linkSummaryViews;
        }

        /// <summary>
        /// Set the request information
        /// </summary>
        /// <param name="token"></param>
        /// <param name="top"></param>
        /// <returns>LinkViewModuleOptions</returns>
        private LinkViewModuleOptions GetRequestOption(string token, int top = 100)
        {
            LinkViewModuleOptions linkViewModuleOptions = new LinkViewModuleOptions();
            #region Required
            //Sets the URLs of site collections for which you want to get the summary information. 100 URLs at most.
            linkViewModuleOptions.SiteUrls = new List<string>()
            {
                "https://xxxx.sharepoint.com/sites/sample1",
                "https://xxxx.sharepoint.com/sites/sample2"
            };
            //Sets whether to get the remaining results of a request of which the results are more than 100.
            linkViewModuleOptions.Token = token;
            //Sets the number of links displayed in each page of the result.The number must be no larger than 100.
            linkViewModuleOptions.PageSize = top;
            //Sets the link types that you are about to get.
            //External Link = 32
            //Organization Link = 64
            //Anonymous Link = 128
            linkViewModuleOptions.LinkType = 32;
            ////Sets the language of the report you are about to export.Default: en-US Support: en-US/ja-JP/fr-FR
            linkViewModuleOptions.Language = "en-US";
            #endregion

            return linkViewModuleOptions;
        }
    }
}
