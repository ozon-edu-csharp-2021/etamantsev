using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels;
using OzonEdu.MerchandiseService.HttpModels.ResponseModels;

namespace OzonEdu.MerchandiseService.HttpClients
{
    public interface IMerchHttpClient
    {
        Task<List<MerchItemResponseModel>> V1GetAll(CancellationToken token);

        Task<List<IssuanceInfoResponseModel>> V1GetIssuance(long merchId, CancellationToken token);
    }

    public class MerchHttpClient : IMerchHttpClient
    {
        private readonly HttpClient _httpClient;

        public MerchHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MerchItemResponseModel>> V1GetAll(CancellationToken token)
        {
            using var response = await _httpClient.GetAsync("v1/api/merch", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<List<MerchItemResponseModel>>(body);
        }

        public async Task<List<IssuanceInfoResponseModel>> V1GetIssuance(long merchId, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"v1/api/merch/{merchId}/issuance", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<List<IssuanceInfoResponseModel>>(body);
        }
    }
}