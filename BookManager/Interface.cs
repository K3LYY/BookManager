﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookManager.Entities;

namespace BookManager
{
    public class Interface
    {

        ColorConsole consolColor = new();

        List<Book> books = new List<Book> {
            new Book(0, "dziady cz. II", "Adam Mickiewicz", 1800, 2),
            new Book(1, "Pan Tadeusz", "Adam Mickiewicz", 1800, 1),
            new Book(2, "Quo Vadis", "Henryk Sienkiewicz", 1800, 1),
        };

        public void ShowMenu()
        {
            Console.WriteLine("========== Book Menager ==========");
            consolColor.WriteWithColor("1. Show all books", ConsoleColor.Green);
            consolColor.WriteWithColor("2. Show book by Id", ConsoleColor.Green);
            consolColor.WriteWithColor("3. Show all categories", ConsoleColor.Green);
            consolColor.WriteWithColor("4. Add new book", ConsoleColor.Green);
            consolColor.WriteWithColor("5. Delete book", ConsoleColor.Red);
            consolColor.WriteWithColor("6. Edit book", ConsoleColor.Blue);
            Console.WriteLine("==================================");

            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (choice)
            {
                case 1:
                    ShowAllBooks();
                    break;
                case 2:
                    Console.WriteLine("Enter book Id: ");
                    int bookId = Convert.ToInt32(Console.ReadLine());
                    Book book = ShowBookById(bookId);
                    ShowBookByObject(book);
                    break;
                case 3:
                    ShowAllCategories();
                    break;
                case 4:
                    AddBook();
                    break;
                case 5:
                    Console.WriteLine("Enter book Id: ");
                    int bookIdToDelete = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    DeleteBookById(bookIdToDelete);
                    break;
                case 7:
                    SaveBooks();
                    break;
                default:
                    consolColor.WriteWithColor("Enter valid number", ConsoleColor.Red);
                    Console.ResetColor();
                    break;
            }
            ShowMenu();
        }
        public void ShowAllBooks()
        {
            Console.WriteLine("");
            Console.WriteLine("========== Wszystkie ksiazki ==============");

            foreach (var book in books)
            {
                consolColor.WriteWithColor($"Id: {book.Id}", ConsoleColor.Green);
                consolColor.WriteWithColor($"Title: {book.title}", ConsoleColor.Green);
                consolColor.WriteWithColor($"Author: {book.author}", ConsoleColor.Green);
                consolColor.WriteWithColor($"Year: {book.year}", ConsoleColor.Green);
                consolColor.WriteWithColor($"Category: {book.categoryText}", ConsoleColor.Green);
                Console.WriteLine("==================================");
            }
            Console.WriteLine("=========== Koniec ksiazek ===============");
            Console.WriteLine("");

        }

        public void AddBook()
        {
            Console.WriteLine("Title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Author: ");
            string author = Console.ReadLine();
            Console.WriteLine("Year: ");
            int year = Convert.ToInt32(Console.ReadLine());
            ShowAllCategories();
            int category = Convert.ToInt32(Console.ReadLine());

            int lastIndex = books[books.Count - 1].Id + 1;
            books.Add( new Book(lastIndex, title, author, year, category));
            
            
        }


        public void ShowAllCategories()
        {
            Categories allcategories = new Categories();
            Console.WriteLine("=========== Categories ===========");
            for (int i = 0; i < allcategories.categories.Count; i++) {
                consolColor.WriteWithColor($"{allcategories.categories[i].Id} {allcategories.categories[i].Name}", ConsoleColor.Yellow);
            }
        }

        public void ShowBookByObject(Book book)
        {
            Console.WriteLine("==================================");
            Console.WriteLine("Id: " + book.Id);
            Console.WriteLine("Title: " + book.title);
            Console.WriteLine("Author: " + book.author);
            Console.WriteLine("Yeat: " + book.year);
            Console.WriteLine("Category: " + book.categoryText);
            Console.WriteLine("==================================");
        }

        public Book ShowBookById(int id)
        {
            // Finding book with binary search

            int l = 0; // right pointer
            int r = books.Count; // left pointer
            
            while (r >= l)
            {
                int m = (l + r) / 2; // middle poiter

                if (books[m].Id == id)
                {
                    return books[m];
                } 
                else if(books[m].Id > id) {
                    r = m - 1;
                }
                else
                {
                    l = m + 1;
                }
            }
            return null;
        }

        public void DeleteBookById(int id)
        {
            // Finding book with binary search

            int l = 0; // right pointer
            int r = books.Count; // left pointer

            while (r >= l)
            {
                int m = (l + r) / 2; // middle poiter

                if (books[m].Id == id)
                {
                    Console.WriteLine("Book to delete: ");
                    ShowBookByObject(books[m]);

                    Console.Write("Write ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(books[m].title);
                    Console.ResetColor();
                    Console.WriteLine(" to confirm deletion");

                    string confirmation = Console.ReadLine();
                    if (confirmation == books[m].title)
                    {
                        books.Remove(books[m]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Deletion canceled");
                        Console.ResetColor();
                        ShowMenu();
                    }
                }
                else if (books[m].Id > id)
                {
                    r = m - 1;
                }
                else
                {
                    l = m + 1;
                }
            }
        }
        public void SaveBooks()
        {
            Saving temp = new();
            temp.save(books);
            string block = "█";
            string progressBar = "";
            for (int i = 10;i <= 100; i+=10)
            {
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("Saving... " + i + "%");
                
                for (int j = 0; j < 10; j++)
                {
                    progressBar += block;
                    
                }
                
                Console.WriteLine(progressBar);
                
                
            }
            Console.Clear();
            Console.WriteLine("Save Completed!!");
            Console.WriteLine(progressBar);
        }
    }
}