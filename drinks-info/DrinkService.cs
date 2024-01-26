using Newtonsoft.Json;
using RestSharp;
using System.Web;

namespace drinks_info
{
    internal class DrinkService
    {
        protected string baseUrl = "http://www.thecocktaildb.com/api/json/v1/1/";
        public void GetCategories()
        {
            RestClient client = new(baseUrl);
            RestRequest request = new("list.php?c=list");
            var response = client.ExecuteAsync(request);
            if(response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                List<Category> categories = serialize.List;
                TableBuilder.PrintTable(categories, "Categories");
            }
        }
        public void GetDrinksByCategory(string categoryInput)
        {
            RestClient client = new(baseUrl);
            RestRequest request = new($"filter.php?c={HttpUtility.UrlEncode(categoryInput)}");
            var response = client.ExecuteAsync(request);
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

                List<Drink> drinks = serialize.List;
                TableBuilder.PrintTable(drinks, $"Drinks In {categoryInput}");
            }
        }

        public void GetDrinkInfo(string id)
        {
            RestClient client = new(baseUrl);
            RestRequest request = new($"lookup.php?i={HttpUtility.UrlEncode(id)}");
            var response = client.ExecuteAsync(request);
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<DrinkInfoObject>(rawResponse);
                List<DrinkInfo> drinkInfoList = serialize.List;
                TableBuilder.PrintTable(drinkInfoList, $"Drink Info: ");
            }
        }
    }
}
