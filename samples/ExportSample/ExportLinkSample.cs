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
    public class ExportLinkSample
    {
        /// <summary>
        /// Export User Permission
        /// </summary>
        public async Task<ExportResult> ExportLinkAsync(InsightsApiClient insightsClient)
        {
            ExportOptions exportOptions = GetRequestOption();
            return await insightsClient.Permission_ExportLinkAsync(exportOptions);
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
            FileResponse response = await insightsClient.Permission_GetFileAsync(id.ToString());
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
        private static void GetFile(FileResponse response, string targetPath)
        {
            using (FileStream fs1 = File.OpenWrite(Path.Combine(targetPath, $"{DateTime.UtcNow.Ticks}.zip")))
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
            InsightsExportResult insightsExportResult = await insightsClient.Permission_GetExportStatusAsync(id.ToString());
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
            //Sets the language of the report you are about to export.Default: en-US Support: en-US/ja-JP/fr-FR
            exportOptions.Language = "en-US";
            //Set the export link type.
            //AnonymousLink (for organization link)
            //FlexibleLink (for external link)
            //OrganizationLink (for organization link)
            exportOptions.ExportLinkType = PrincipalType.AnonymousLink;
            
            // Filter Link Create By Time, Time format:yyyy-MM-ddTHH:mm:ss"
            exportOptions.StartTime = "2023-01-01T01:37:57";
            // Filter Link Create By Time, Time format:yyyy-MM-ddTHH:mm:ss"
            exportOptions.EndTime = "2023-05-01T01:37:57";
            #endregion

            return exportOptions;
        }
    }
}
