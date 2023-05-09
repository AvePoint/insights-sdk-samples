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
    public class ExportGroupPermissionSample
    {
        /// <summary>
        /// Export User Permission
        /// </summary>
        public async Task<ExportResult> ExportGroupPermissionAsync(InsightsApiClient insightsClient)
        {
            ExportOptions exportOptions = GetRequestOption();
            return await insightsClient.ExportGroupAccessAsync(exportOptions);
        }
        /// <summary>
        /// Get export file
        /// </summary>
        public async Task<FileResponse> GetExportFileAsync(InsightsApiClient insightsClient, int id)
        {
            //first check export job status
            while (true)
            {
                int status = await GetExportFileStatusAsync(insightsClient, id);
                if (status != 1 || status != 0)
                {
                    break;
                }
                //Check every two minutes.
                Thread.Sleep(2 * 1000 * 60);
            }
            //Get export Site permission file
            return await insightsClient.GetExportFileAsync(id.ToString());
        }

        /// <summary>
        /// Get export job status
        /// </summary>
        public async Task<int> GetExportFileStatusAsync(InsightsApiClient insightsClient, int id)
        {
            //None = 0,
            //Inprogress = 1,
            //Successful = 2,
            //Failed = 3,
            //SuccessWithException = 4,
            //Stopping = 5,
            //Stopped = 6
            InsightsExportResult insightsExportResult = await insightsClient.GetExportStatusAsync(id.ToString());
            return insightsExportResult.Status;
        }

        /// <summary>
        /// Set the request information
        /// </summary>
        /// <returns>ExportOptions</returns>
        private ExportOptions GetRequestOption()
        {
            ExportOptions exportOptions = new ExportOptions();

            #region Required
            //Set the Group UniqueId of the user for whom to export permission reports. Up to 100 Group UniqueIds.
            exportOptions.Emails = new List<string>()
            {
                "ec34726b-f692-424f-aaf0-f6a478a1b9fc7",
                "ec34726b-f692-424f-aaf0-f6a478a1b9fc7"
            };
            //Sets the workspace in which you want to export the access report of users. Multiple values are allowed.
            exportOptions.DataSources = new List<string>()
            {
                "microsoft teams",
                "sharepoint online",
                "onedrive for business",
                "microsoft 365 group"
            };
            //Sets the language of the report you are about to export.Default: en-US Support: en-US/ja-JP/fr-FR
            exportOptions.Language = "en-US";
            //Sets the export option of the report you are about to export.
            /// So far support:
            /// 1 - for exporting both the summary report and site collection level access report
            /// 2 - for exporting both the summary report and access report to all objects in the configured data scope
            /// 3 - for only exporting the summary report
            exportOptions.ExportOptionType = ExportOptionType._1;
            #endregion

            return exportOptions;
        }
    }
}
