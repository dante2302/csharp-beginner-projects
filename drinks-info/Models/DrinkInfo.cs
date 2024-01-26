using Newtonsoft.Json;

namespace drinks_info
{

    public class DrinkInfo
    {
        public string idDrink;
        public string strDrink;
        public object strDrinkAlternate;
        public object strTags;
        public object strVideo;
        public string strCategory;
        public object strIBA;
        public string strAlchoholic;
        public string strGlass;
        public string strInstructions;
        public string strIngredient1;
        public string strIngredient2;
        public string strIngredient3;
        public string strIngredient4;
        public string strIngredient5;
        public string strIngredient6;
        public string strIngredient7;
        public string strMeasure1;
        public string strMeasure2;
        public string strMeasure3;
    }

    public class DrinkInfoObject
    {
        [JsonProperty("drinks")]
        public List<DrinkInfo> List { get; set; }
    }
}
