using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BookManager.Entities;

namespace BookManager
{
    internal class Reading
    {
        List<Book> books = new List<Book> { };
        public List<Book> read()
        {
            using (StreamReader reader = new StreamReader("..\\..\\..\\BooksDB.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] lineTab = line.Split("|");
                    if (lineTab.Length == 5)
                    {
                        books.Add(new Book(Convert.ToInt32(lineTab[0]), lineTab[1], lineTab[2], Convert.ToInt32(lineTab[3]), Convert.ToInt32(lineTab[4])));
                    }
                }
            }
            return books;
        }
        
    }
}
