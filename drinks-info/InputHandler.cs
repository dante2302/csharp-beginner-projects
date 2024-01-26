using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drinks_info
{
    internal class InputHandler
    {
        private DrinkService drinkService = new();
        public void GetCategoriesInput()
        {
            drinkService.GetCategories();
            Console.Write("Choose a category (type 0 to exit):");
            string chosenCategory = Console.ReadLine();
            GetDrinksInput(chosenCategory);
        }
        public void GetDrinksInput(string category)
        {
            drinkService.GetByCategory(category);
            Console.Write("Choose a drink to see details about (type 0 to go back): ");
            string chosenDrink = Console.ReadLine();
            GetDrinksInfo(chosenDrink);
        }
    }
}
