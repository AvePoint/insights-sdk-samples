namespace Insights.Sdk.Samples.SummarySample
{
    #region using directives
    using Insights.Client;
    using System.Net.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    #endregion
    public class GroupSumarySample
    {
        /// <summary>
        /// Get group summary Json
        /// </summary>
        public async Task<List<GroupSummaryViewModule>> GetGroupSummaryAsync(InsightsApiClient insightsClient)
        {
            List<GroupSummaryViewModule> groupSummaryViews = new List<GroupSummaryViewModule>();
            //Init first page
            int startPage = 1;
            while (true)
            {
                SummaryViewModuleOptions groupSummaryOptions = GetRequestOption(startPage);
                GroupSummaryViewModuleResultList groupSummaryViewModuleResultList = await insightsClient.GetGroupSummaryAsync(groupSummaryOptions);
                if (groupSummaryViewModuleResultList != null && !groupSummaryViewModuleResultList.Results.IsNullOrEmpty())
                {
                    groupSummaryViews.AddRange(groupSummaryViewModuleResultList.Results);
                    startPage++;
                }
                else
                {
                    break;
                }
            }
            return groupSummaryViews;
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
