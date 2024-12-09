namespace samples.DetailRecordSample
{
    #region using directives
    using Insights.Client;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    #endregion
    public class DetailRecordSample
    {
        public async Task<List<DetailRecordData>> QueryDetailRecordAsync(InsightsApiClient insightsClient)
        {
            List<DetailRecordData> datas = new();
            DetailRecordExportOptions option = new()
            {
                Language = "en-US",
                PageSize = 100, //1 ~ 100
                Token = null,
                Filters = new List<Filter>()
                {
                    new Filter()
                    {
                        Id = "module",
                        Values = new List<string>(){ "1", "2", "3", "4" }
                    }
                }
            };
            do
            {
                DetailRecordDataResultList result = await insightsClient.DetailRecord_QueryAsync(option, new());
                if (result.Results is not null && result.Results.Count > 0)
                {
                    datas.AddRange(result.Results);
                    option.Token = result.NextToken;
                }
            } while (!string.IsNullOrEmpty(option.Token));
            return datas;
        }

        public async Task<List<DetailRecordData>> QueryDetailRecordBySiteIdAsync(InsightsApiClient insightsClient)
        {
            List<DetailRecordData> datas = new();
            DetailRecordExportOptions option = new()
            {
                Language = "en-US",
                PageSize = 100, //1 ~ 100
                Token = null,
                SiteId = "6ed328c0-e069-4f0c-ae66-776ef950c754",
                Filters = new List<Filter>()
                {
                    new Filter()
                    {
                        Id = "module",
                        Values = new List<string>(){ "1", "2", "3", "4" }
                    }
                },
            };
            do
            {
                DetailRecordDataResultList result = await insightsClient.DetailRecord_QueryBySiteAsync(option, new());
                if (result.Results is not null && result.Results.Count > 0)
                {
                    datas.AddRange(result.Results);
                    option.Token = result.NextToken;
                }
            } while (!string.IsNullOrEmpty(option.Token));
            return datas;
        }

        public async Task<List<SiteResponseViewModel>> QuerySitIdbySiteUrlAsync(InsightsApiClient insightsClient)
        {
            List<SiteResponseViewModel> datas = new();
            SiteUrlRequest option = new()
            {
                SiteUrls = new List<string>() { "https://xxxx.sharepoint.com/sites/sample1" },
            };
            SiteResponseViewModelResultList result = await insightsClient.DetailRecord_GetSiteIdsBySiteUrlAsync(option, new());
            if (result.Results is not null && result.Results.Count > 0)
            {
                datas.AddRange(result.Results);
            }
            return datas;
        }

        public async Task<List<SitesData>> QuerySiteOverViewAsync(InsightsApiClient insightsClient)
        {
            List<SitesData> datas = new();
            ExportOptionsBase option = new()
            {
                PageSize = 100, //1 ~ 100
                Token = null,
                Filters = new List<Filter>()
                {
                    new Filter()
                    {
                        Id = "module",
                        Values = new List<string>(){ "1", "2", "3", "4" }
                    },
                    new Filter()
                    {
                        Id = "sensitivitylevel",
                        Values = new List<string>(){ "0","1", "2", "3" }
                    },
                    new Filter()
                    {
                        Id = "Exposurelevel",
                        Values = new List<string>(){ "0","1", "2", "3" }
                    },
                    new Filter()
                    {
                        Id = "riskLevel",
                        Values = new List<string>(){ "0","1", "2", "3" }
                    },
                },
            };
            do
            {
                SitesDataResultList result = await insightsClient.DetailRecord_GetSiteOverViewAsync(option, new());
                if (result.Results is not null && result.Results.Count > 0)
                {
                    datas.AddRange(result.Results);
                    option.Token = result.NextToken;
                }
            } while (!string.IsNullOrEmpty(option.Token));
            return datas;
        }
    }
}