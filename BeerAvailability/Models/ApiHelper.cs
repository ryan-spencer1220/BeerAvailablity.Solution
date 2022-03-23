using System.Threading.Tasks;
using RestSharp;

namespace BeerAvailability.Models
{
  class ApiHelper
  {
    public static async Task<string> ApiCall(string breweryName)
    {
      RestClient client = new RestClient($"https://api.openbrewerydb.org/breweries?by_name={breweryName}");
      RestRequest request = new RestRequest("", Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }
  }
}