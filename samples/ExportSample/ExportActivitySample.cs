namespace Insights.Sdk.Samples.ExportSample
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
    using System.Threading;
    #endregion
    public class ExportActivitySample
    {
        /// <summary>
        /// export user activity
        /// </summary>
        public async Task<ExportResult> ExportUserActivityAsync(InsightsApiClient insightsClient)
        {
            ActivityExportOptions userActiviytExportOptions = GetUserActiviytRequestOption();
            return await insightsClient.ExportActivityAsync(userActiviytExportOptions);
        }

        /// <summary>
        /// export object activity
        /// </summary>
        public async Task<ExportResult> ExportObjectActivityAsync(InsightsApiClient insightsClient)
        {
            ActivityExportOptions objectActivityExportOptions = GetObjectActivityRequestOption();
            return await insightsClient.ExportActivityAsync(objectActivityExportOptions);
        }

        /// <summary>
        /// Get export file
        /// </summary>
        public async Task<FileResponse> GetExportActivityAsync(InsightsApiClient insightsClient, int id)
        {
            //first check export job status
            while (true)
            {
                int status = await GetExportActivityStatusAsync(insightsClient, id);
                if (status != 1 || status != 0)
                {
                    break;
                }
                //Check every two minutes.
                Thread.Sleep(2 * 1000 * 60);
            }
            //Get export Site permission file
            return await insightsClient.GetExportActivityFileAsync(id.ToString());
        }

        /// <summary>
        /// Get export job status
        /// </summary>
        public async Task<int> GetExportActivityStatusAsync(InsightsApiClient insightsClient, int id)
        {
            //None = 0,
            //Inprogress = 1,
            //Successful = 2,
            //Failed = 3,
            //SuccessWithException = 4,
            //Stopping = 5,
            //Stopped = 6
            InsightsExportResult insightsExportResult = await insightsClient.GetExportActivityStatusAsync(id.ToString());
            return insightsExportResult.Status;
        }

        /// <summary>
        /// Set the request information
        /// </summary>
        /// <returns>ActivityExportOptions</returns>
        private ActivityExportOptions GetUserActiviytRequestOption()
        {
            ActivityExportOptions activityExportOptions = new ActivityExportOptions();

            #region Required
            // Sets the language of the report you are about to export.Default: en-US Support: en-US/ja-JP/fr-FR
            activityExportOptions.Language = "en-US";
            // Filter By Time, Time format:yyyy-MM-ddTHH:mm:ss"
            activityExportOptions.StartTime = "2023-01-01T01:37:57";
            // Filter By Time, Time format:yyyy-MM-ddTHH:mm:ss"
            activityExportOptions.EndTime = "2023-05-01T01:37:57";
            // Filter by Event Type
            activityExportOptions.EventTypes = new List<string>();
            // Filter by Path
            activityExportOptions.Path = "https://xxxx.sharepoint.com/sites/sample1";
            #endregion

            return activityExportOptions;
        }

        /// <summary>
        /// Set the request information
        /// </summary>
        /// <returns>ActivityExportOptions</returns>
        private ActivityExportOptions GetObjectActivityRequestOption()
        {
            ActivityExportOptions activityExportOptions = new ActivityExportOptions();

            #region Required
            // Sets the language of the report you are about to export.Default: en-US Support: en-US/ja-JP/fr-FR
            activityExportOptions.Language = "en-US";
            // Filter By Time, Time format:yyyy-MM-ddTHH:mm:ss"
            activityExportOptions.StartTime = "2023-01-01T01:37:57";
            // Filter By Time, Time format:yyyy-MM-ddTHH:mm:ss"
            activityExportOptions.EndTime = "2023-05-01T01:37:57";
            // Filter by Operation
            // https://learn.microsoft.com/en-us/microsoft-365/compliance/audit-log-activities?view=o365-worldwide
            activityExportOptions.EventTypes = new List<string>();
            // Filter by User email
            activityExportOptions.Email = "xxxx@sample.com";
            #endregion

            return activityExportOptions;
        }
    }
}
