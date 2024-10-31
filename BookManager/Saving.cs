using BookManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookManager
{
    public class Saving
    {
        Interface temp = new();
        string tempBook = "";
        public void save(List<Book> books)
        {
            FileStream stream = new FileStream("E:\\GitProjects\\BookManager\\BooksDB.txt", FileMode.OpenOrCreate);
            using (StreamWriter write = new StreamWriter(stream, Encoding.UTF8))
            {
                for (int i = 0; i < books.Count; i++)
                {
                    tempBook += books[i].Id + "|" + books[i].title + "|" + books[i].author + "|" + books[i].year + "|" + books[i].categoryText;
                    write.WriteLine(tempBook);                    
                    tempBook = "";
                }
            }

        }        
    }
}
