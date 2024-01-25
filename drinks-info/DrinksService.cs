using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drinks_info
{
    internal class DrinksService
    {
        public void GetCategories()
        {
            RestClient client = new("www.thecocktaildb.com/api/json/v1/1/");
            RestRequest request = new("list.php?c=list");
            var response = client.ExecuteAsync(request);
            if(response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);
                List<Category> categories = serialize.List;
                foreach(Category category in categories)
                {
                    Console.WriteLine(category.catStr);
                }
            }
        }
    }
}
