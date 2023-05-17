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
    using System.IO;
    #endregion
    public class ExportSitePermissionSample
    {
        /// <summary>
        /// Export Site Permission
        /// </summary>
        public async Task<ExportResult> ExportSitePermissionAsync(InsightsApiClient insightsClient)
        {
            ExportOptions exportOptions = GetRequestOption();
            ExportResult result =  await insightsClient.ExportSitePermissionAsync(exportOptions);
            return result;
        }

        /// <summary>
        /// Get export file
        /// </summary>
        public async Task GetExportFileAsync(InsightsApiClient insightsClient, int id)
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
            FileResponse response = await insightsClient.GetExportFileAsync(id.ToString());
            if (response != null && (response.StatusCode == 200 || response.StatusCode == 206))
            {
                GetFile(response, "targetPath");
            }
        }

        /// <summary>
        /// Get File Note:The file type is compressed file
        /// </summary>
        /// <param name="response"></param>
        /// <param name="targetPath"></param>
        private static void GetFile(FileResponse response,string targetPath)
        {
            using (FileStream fs1 = File.OpenWrite(Path.Combine("targetPath", $"{DateTime.UtcNow.Ticks}.zip")))
            {
                using (Stream st = response.Stream)
                {
                    byte[] buffer = new byte[1024];
                    int numBytesRead = 0;
                    do
                    {
                        numBytesRead = st.Read(buffer, 0, 1024);
                        fs1.Write(buffer, 0, numBytesRead);
                    }
                    while (numBytesRead > 0);
                }
            }
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
            //Sets the URLs of site collections for which you want to export the permission report. 100 email addresses at most.
            exportOptions.SiteUrls = new List<string>()
            {
                "https://xxxx.sharepoint.com/sites/sample1",
                "https://xxxx.sharepoint.com/sites/sample2"
            };
            //Sets the language of the report you are about to export.Default: en-US Support: en-US/ja-JP/fr-FR
            exportOptions.Language = "en-US";
            #endregion

            return exportOptions;
        }
    }
}
