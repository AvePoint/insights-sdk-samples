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
            string accessToken = await GetAccessTokenAsync(CoreConstant.IdentityServiceUrl, "{clientId}", "{cerPrint}");
            //get Insights Api Client
            InsightsApiClient insightsClient = GetClient(accessToken);
        }
        /// <summary>
        /// Get Access Token
        /// </summary>
        /// <param name="url">identity service url</param>
        /// <param name="clientId">identity service clientId</param>
        /// <param name="cerPrint">certificate Thumbprint</param>
        static async Task<string> GetAccessTokenAsync(string url, string clientId, string cerPrint)
        {
            try
            {
                return await new PublicIdentityServiceHelperSample().GetAccessTokenAsync(url, clientId, cerPrint);
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
