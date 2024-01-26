
using Newtonsoft.Json;

namespace drinks_info
{
    public class Drink
    {
        public string strDrink { get; set; }
        public string idDrink { get; set; }
    }

    public class Drinks
    {
        [JsonProperty("drinks")]
        public List<Drink> List { get; set; }
    }
}
