using Services;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            BookService books = new BookService(JSONSupport.ReadBooksFromJSONFile());

            UIservice userService = new UIservice();

            //userService.EnterNewBookToLibrary(books);
            userService.PrintBooksToConsole(books);
            userService.ChooseBookToRead(books);
            //userService.ChooseBookToReturn(books);
            //userService.ChooseBooksByCriteria(books);
            //userService.ChoseBookToRemoveFromLibrary(books);

            JSONSupport.SaveBooksToJSONFile(books.BookLibrary);
        }
    }
}
