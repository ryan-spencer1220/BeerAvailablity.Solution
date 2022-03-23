using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BeerAvailability.Models
{
  public class Brewery
  {
    public string Name { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string WebsiteURL { get; set; }

    public static List<Brewery> GetBreweries(string breweryName)
    {
      var apiCallTask = ApiHelper.ApiCall(breweryName);
      var result = apiCallTask.Result;

      JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
      List<Brewery> BreweryList = JsonConvert.DeserializeObject<List<Brewery>>(jsonResponse.ToString());

      return BreweryList;
    }
  }
}