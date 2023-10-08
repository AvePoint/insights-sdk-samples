namespace Insights.Sdk.Samples.SummarySample
{
    #region using directives
    using Insights.Client;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    #endregion
    public class UserSummarySample
    {
        /// <summary>
        /// Get user summary Json
        /// </summary>
        public async Task<List<UserSummaryViewModule>> GetUserSummaryAsync(InsightsApiClient insightsClient)
        {
            List<UserSummaryViewModule> userSummaryViews = new List<UserSummaryViewModule>();
            //Init first page
            int startPage = 1;
            while (true)
            {
                SummaryViewModuleOptions userSummaryOptions = GetRequestOption(startPage);
                ResultListOfUserSummaryViewModule userSummaryViewModuleResultList = await insightsClient.User_GetUserSummaryAsync(userSummaryOptions);
                if (userSummaryViewModuleResultList != null && !userSummaryViewModuleResultList.Results.IsNullOrEmpty())
                {
                    userSummaryViews.AddRange(userSummaryViewModuleResultList.Results);
                    startPage ++;
                }
                else
                {
                    break;
                }
            }
            return userSummaryViews;
        }

        /// <summary>
        /// Set the request information
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="top"></param>
        /// <returns>SummaryViewModuleOptions</returns>
        private SummaryViewModuleOptions GetRequestOption(int startPage, int top = 100)
        {
            SummaryViewModuleOptions summaryViewModuleOptions = new SummaryViewModuleOptions();
            #region Required
            //Sets the start page from which you want to get the group summary. The default value is 1.
            summaryViewModuleOptions.StartPage = startPage;
            //Sets the number of groups displayed in each page of the result. The number must be no larger than 100. The default value is 100.
            summaryViewModuleOptions.PageSize = top;
            ////Sets the language of the report you are about to export.Default: en-US Support: en-US/ja-JP/fr-FR
            summaryViewModuleOptions.Language = "en-US";
            #endregion

            return summaryViewModuleOptions;
        }
    }
}
