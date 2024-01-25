using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drinks_info
{
    internal class InputHandler
    {
        DrinksService drinkService = new();
        public void getCategoriesInput()
        {
            drinkService.GetCategories();
        }
    }
}
