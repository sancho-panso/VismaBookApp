using Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class UIservice
    {
        public void ChoseBookToRemoveFromLibrary(BookService books)
        {
            Console.WriteLine("Please enter title of book to remove it from library");
            books.DeleteBookFromList(Console.ReadLine());
        }
        public void PrintBooksToConsole(BookService books)
        {
            Console.WriteLine("Would you like print all books?, press Y");
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.WriteLine("********Here is list of all books********");
                foreach (Book book in books.BookLibrary)
                {
                    books.PrintBookDetails(book);
                }
            }

        }
        public void ChooseBooksByCriteria(BookService books)
        {
            Console.WriteLine("Would you like print list of books by criteria?, press Y");
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.WriteLine("********Here is list of books filtered by your criteria********");
                List<Book> filteredBooks = ChooseCriteria(books);
                foreach (Book book in filteredBooks)
                {
                    books.PrintBookDetails(book);
                }
            }

        }

        public void ChooseBookToRead(BookService books)
        {
            Console.WriteLine("Please enter book's title, you wanna read");
            string title = Console.ReadLine();
            Console.WriteLine("Please enter your name");
            string user = Console.ReadLine();

            books.CheckBookFromLibrary(title, user);
        }
        public void ChooseBookToReturn(BookService books)
        {
            Console.WriteLine("Please enter book's title, you wanna return");

            if (books.ReturnBookToLibrary(Console.ReadLine()))
            {
                Console.WriteLine("Book is returned");
            }
            else
            {
                Console.WriteLine("Wrong title of book");
            }

        }
        public void EnterNewBookToLibrary(BookService books)
        {
            Console.WriteLine("+++++++ You can enter new book to library here +++++++");

            var newBook = new Book();

            newBook.Title = GetTitleFromInput();
            newBook.Author = GetAuthorFromInput();
            newBook.Category = GetCategoryFromInput();
            newBook.Language = GetLanguageFromInput();
            newBook.PublicationDate = GetPublishDateFromInput();
            newBook.ISBN = GetISBNFromInput();

            books.AddBookToLibrary(newBook);
        }

        private string GetISBNFromInput()
        {
            Console.WriteLine("Please enter book's ISBN");
            string inputString = Console.ReadLine();

            string isbn = "NotDefined";

            string pattern = @"/^(?:ISBN(?:-10)?:?\\)?(?=[0-9X]{10}$|(?=(?:[0-9]+[-\\ ]){3})[-\\ 0-9X]{13}$)[0-9]{1,5}[-\\ ]?[0-9]+[-\\]?[0-9]+[-\\ ]?[0-9X]$/";
            
            Regex rg = new Regex(pattern);

            if (rg.Match(inputString).Success) isbn = inputString;

            return isbn;
        }

        private DateTime GetPublishDateFromInput()
        {
            Console.WriteLine("Please enter book's publication date as YYYY/MM/DD or DD/MM/YYYY");
            string inputString = Console.ReadLine();

            return TimeInputService.GetTimeFromInput(inputString);
        }

        private Book.Languages GetLanguageFromInput()
        {
            Console.WriteLine("Please choose book's language:EN, LT, DE, RU");
            string inputString = Console.ReadLine();

            if (inputString == "EN")
                return Book.Languages.EN;

            if (inputString == "LT")
                return Book.Languages.LT;

            if (inputString == "DE")
                return Book.Languages.DE;

            if (inputString == "RU")
                return Book.Languages.RU;

            return Book.Languages.NotDefined;
        }

        private Book.Categories GetCategoryFromInput()
        {
            Console.WriteLine("Please choose book's category:Fiction, Sci_Fiction, Not_Fiction ");
            string inputString = Console.ReadLine();

            if (inputString == "Fiction")
                return Book.Categories.Fiction;
            
            if (inputString == "Sci_Fiction")
                return Book.Categories.Sci_Fiction;
            
            if (inputString == "Not_Fiction")
                return Book.Categories.Not_Fiction;
            
            return Book.Categories.NotDefined;
        }

        private string GetAuthorFromInput()
        {
            Console.WriteLine("Please enter author name");
            return Console.ReadLine();
        }
        
        private string GetTitleFromInput()
        {
            Console.WriteLine("Please enter book title");
            return Console.ReadLine();
        }

        private string GetAvalabilityFromInput()
        {
            Console.WriteLine("Press a for available or l for lended books");
            return Console.ReadLine().ToLower();
        }

        private List<Book> ChooseCriteria(BookService books)
        {
            Console.WriteLine("Please choose filtering criteria, press 1 for author, 2 for language, 3 for title, 4 for category, 5 for availability ");
            string choice = Console.ReadLine();

            List<Book> filteredBooks;

            switch (choice)
            {
                case "1":
                    filteredBooks = books.FilterBooksByAuthor(GetAuthorFromInput());
                    if (filteredBooks.Count == 0) goto default;
                    return filteredBooks;
                case "2":
                    filteredBooks = books.FilterBooksByLanguage(GetLanguageFromInput());
                    if (filteredBooks.Count == 0) goto default;
                    return filteredBooks;
                case "3":
                    filteredBooks = books.FilterBooksByTitle(GetTitleFromInput());
                    if (filteredBooks.Count == 0) goto default;
                    return filteredBooks;
                case "4":
                    filteredBooks = books.FilterBooksByCategory(GetCategoryFromInput());
                    if (filteredBooks.Count == 0) goto default;
                    return filteredBooks;
                case "5":
                    string userChoice = GetAvalabilityFromInput();
                    filteredBooks = books.FilterBooksByLending(userChoice);
                    
                    return filteredBooks;
                default:
                    Console.WriteLine("Not valid choosen option for filtering, you got list of all books");
                    return books.BookLibrary;
            }
        }
    }
}
