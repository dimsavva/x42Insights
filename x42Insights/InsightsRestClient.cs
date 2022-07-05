using RestSharp;
using System.Text;

namespace x42Insights
{
    public class InsightsRestClient
    {
         private readonly RestClient _restclient;
  
        public InsightsRestClient()
        {
             _restclient = new RestClient("https://x42.indexer.blockcore.net/api");

        }

        public async Task<List<RichModel>> GetRichList(int limit, int offset) {

 
            var request = new RestRequest("/insight/richlist?limit=" + limit+"&offset="+offset);

            var response = await _restclient.GetAsync<List<RichModel>>(request);

            return response;

        }


        public async Task<List<TransactionModel>> GetLast(string address, int limit, int offset)
        {


            var request = new RestRequest("/query/address/"+ address + "/transactions?limit=" + limit + "&offset=");

            var response = await _restclient.GetAsync<List<TransactionModel>>(request);

            return response;

        }
    }
}