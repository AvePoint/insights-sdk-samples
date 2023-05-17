namespace Insights.Sdk.Samples
{
    #region using directives
    using Insights.Client;
    using Insights.Sdk.Samples.GetAccessTokenSample;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    #endregion
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //get access token
            string accessToken = await GetAccessTokenAsyncByCertificatePath(CoreConstant.IdentityServiceUrl, "{clientId}", "{certificate file Path}");
            //get Insights Api Client
            InsightsApiClient insightsClient = GetClient(accessToken);
        }

        /// <summary>
        /// Get Access Token By Local certificate file
        /// </summary>
        /// <param name="url">identity service url</param>
        /// <param name="clientId">identity service clientId</param>
        /// <param name="certificatePath">certificate file Path</param>
        static async Task<string> GetAccessTokenAsyncByCertificatePath(string url, string clientId, string certificatePath)
        {
            try
            {
                return await new PublicIdentityServiceHelperSample().GetAccessTokenAsyncByCertificatePath(url, clientId, certificatePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get Access Token By CerPrint
        /// </summary>
        /// <param name="url">identity service url</param>
        /// <param name="clientId">identity service clientId</param>
        /// <param name="cerPrint">certificate Thumbprint</param>
        static async Task<string> GetAccessTokenAsyncByCerPrint(string url, string clientId, string cerPrint)
        {
            try
            {
                return await new PublicIdentityServiceHelperSample().GetAccessTokenAsyncByCerPrint(url, clientId, cerPrint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }


        /// <summary>
        /// init Insights Api Client
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        static InsightsApiClient GetClient(string accessToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            InsightsApiClient insightsClient = new InsightsApiClient(CoreConstant.BaseUrl, httpClient);
            return insightsClient;
        }
    }
}
