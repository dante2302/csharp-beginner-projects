using Newtonsoft.Json;
using RestSharp;

namespace drinks_info
{
    internal class DrinksService
    {
        public void GetCategories()
        {
            RestClient client = new("http://www.thecocktaildb.com/api/json/v1/1/");
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
    }
}
