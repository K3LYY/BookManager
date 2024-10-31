using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Entities
{
    internal class Categories
    {
        public List<Category> categories = new List<Category> { };
        List<string> categoriesNames = new List<string> { "Commedy", "Horror", "Crime", "Classics", "Thrillers", "War", "Adventure stories", "Romance", "Science fiction" };
        public Categories()
        {
            for (int i = 0; i < categoriesNames.Count() - 1; i++)
            {
                categories.Add(new Category(i, categoriesNames[i]));
            }
        }

    }
}
