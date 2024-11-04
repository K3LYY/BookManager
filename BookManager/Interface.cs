using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using BookManager.Entities;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookManager
{
    public class Interface
    {
        Reading reader = new();
        ColorConsole consolColor = new();
        List<Book> books;
        Validation validate = new();
        public Interface()
        {
            books = reader.read();
        }

        //List<Book> books = new List<Book> {
        //    new Book(0, "dziady cz. II", "Adam Mickiewicz", 1800, 2),
        //    new Book(1, "Pan Tadeusz", "Adam Mickiewicz", 1800, 1),
        //    new Book(2, "Quo Vadis", "Henryk Sienkiewicz", 1800, 1),
        //};

        public void ShowMenu()
        {
            Console.WriteLine("========== Book Menager ==========");
            consolColor.WriteWithColor("1. Show all books", ConsoleColor.Green);
            consolColor.WriteWithColor("2. Show book by Id", ConsoleColor.Green);
            consolColor.WriteWithColor("3. Show all categories", ConsoleColor.Green);
            consolColor.WriteWithColor("4. Add new book", ConsoleColor.Green);
            consolColor.WriteWithColor("5. Delete book", ConsoleColor.Red);
            consolColor.WriteWithColor("6. Edit book", ConsoleColor.Blue);
            consolColor.WriteWithColor("7. Save book", ConsoleColor.Blue);
            consolColor.WriteWithColor("8.Exit", ConsoleColor.Red);
            Console.WriteLine("==================================");

            string inputChoice = Console.ReadLine();
            int choice = 0;


            if (validate.lettersInNumbers(inputChoice) != -1)
            {
                choice = validate.lettersInNumbers(inputChoice);
            }
            
            Console.Clear();

            switch (choice)
            {
                case 1:
                    ShowAllBooks();
                    break;
                case 2:
                    Console.WriteLine("Enter book Id: ");
                    int bookId = Convert.ToInt32(Console.ReadLine());
                    ShowBookByObject(ShowBookById(bookId));
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
                case 6:
                    Console.WriteLine("Enter book Id: ");
                    string bookIdToEditInput = Console.ReadLine();
                    int bookIdToEdit;
                    if (validate.lettersInNumbers(bookIdToEditInput) != -1)
                    {
                        bookIdToEdit = validate.lettersInNumbers(bookIdToEditInput);
                    }
                    else
                    {
                        Console.Clear();
                        consolColor.WriteWithColor("Please enter number not letters", ConsoleColor.Red);
                        ShowMenu();
                    }
                    Console.Clear();
                    EditBookById(bookIdToEdit);
                    break;
                case 7:
                    SaveBooks();
                    break;
                case 8:
                    System.Environment.Exit(-1);
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
            books.Add(new Book(lastIndex, title, author, year, category));

        }

        public void ShowAllCategories()
        {
            Categories allcategories = new Categories();
            Console.WriteLine("=========== Categories ===========");
            for (int i = 0; i < allcategories.categories.Count; i++)
            {
                consolColor.WriteWithColor($"{allcategories.categories[i].Id} {allcategories.categories[i].Name}", ConsoleColor.Yellow);
            }
        }

        public void ShowBookByObject(Book book)
        {
            if(book == null)
            {
                consolColor.WriteWithColor("Wrong book", ConsoleColor.Red);
                ShowMenu();
            }

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

            if(id > books.Count || id < 0)
            {
                consolColor.WriteWithColor($"Book with Id {id} does not exists ", ConsoleColor.Red);
                ShowMenu();
            }
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
                else if (books[m].Id > id)
                {
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
            if (id > books.Count)
            {
                consolColor.WriteWithColor($"Book with Id {id} does not exists ", ConsoleColor.Red);
                ShowMenu();
            }

            // Finding book with binary search

            int l = 0; // right pointer
            int r = books.Count; // left pointer
            Console.WriteLine(r);
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
            consolColor.WriteWithColor($"Book with Id {id} does not exists ", ConsoleColor.Red);

        }
        public void SaveBooks()
        {
            Saving temp = new();
            temp.save(books);
            string block = "█";
            string progressBar = "";
            for (int i = 10; i <= 100; i += 10)
            {
                System.Threading.Thread.Sleep(500);
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

        public void EditBookById(int id)
        {
            if (id > books.Count)
            {
                consolColor.WriteWithColor($"Book with Id {id} does not exists ", ConsoleColor.Red);
                ShowMenu();
            }

            // Finding book with binary search

            int l = 0; // right pointer
            int r = books.Count; // left pointer
            int tempM = -1;

            while (r >= l)
            {
                int m = (l + r) / 2; // middle poiter

                if (books[m].Id == id)
                {
                    tempM = m;
                    break;
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
            Console.WriteLine(tempM);

            if (tempM == -1)
            {
                consolColor.WriteWithColor($"Book with Id {id} does not exists ", ConsoleColor.Red);
            }

            ShowBookByObject(ShowBookById(tempM));

            Console.WriteLine("Title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Author: ");
            string author = Console.ReadLine();
            Console.WriteLine("Year: ");
            int year = Convert.ToInt32(Console.ReadLine());
            ShowAllCategories();
            int category = Convert.ToInt32(Console.ReadLine());

            books.Add(new Book(books[tempM].Id, title, author, year, category));
            books.Remove(books[tempM]);

        }
    }
}