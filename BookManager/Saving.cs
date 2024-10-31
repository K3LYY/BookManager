using BookManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace BookManager
{
    public class Saving
    {
        Interface temp = new();
        string tempBook = "";
        public void save(List<Book> books)
        {
            FileStream stream = new FileStream("E:\\BookManagerFinal\\BookManager\\BooksDB.txt", FileMode.OpenOrCreate);
            using (StreamWriter write = new StreamWriter(stream))
            {
                for (int i = 0; i <= books.Count-1; i++)
                {
                    tempBook += books[i].Id + "|" + books[i].title + "|" + books[i].author + "|" + books[i].year + "|" + books[i].categoryNumber;
                    Console.WriteLine(books[i].Id + "|" + books[i].title + "|" + books[i].author + "|" + books[i].year + "|" + books[i].categoryText);
                    write.WriteLine(tempBook);                    
                    tempBook = "";
                }
                write.Close();
                stream.Close();
            }

        }        
    }
}
