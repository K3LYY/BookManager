using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookManager.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int year { get; set; }
        public int categoryNumber { get; set; }
        public string categoryText { get; set; }


        public Book(int id, string title, string author, int year, int categoryNumber)
        {
            Id = id;
            this.title = title;
            this.author = author;
            this.year = year;
            this.categoryNumber = categoryNumber;
            Categories categories = new Categories();
            categoryText = categories.categories[categoryNumber].Name;
        }
    }
}
