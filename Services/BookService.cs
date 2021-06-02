using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookService
    {
        public List<Book> BookLibrary { get; set; } = new List<Book>();

        public BookService()
        {

        }
        public BookService(List<Book> books)
        {
            BookLibrary = books;
        }
        public void AddBookToLibrary(Book book)
        {
            BookLibrary.Add(book);
        }

        internal void DeleteBookFromList(string title)
        {
            Book bookToDelete = GetBookByName(title);

            if(bookToDelete != null)
                {
                    Console.WriteLine("Are you sure about to delete book {0}? Please press Y to confirm", bookToDelete.Title);
                    if(Console.ReadLine().ToLower()== "y")
                    {
                        BookLibrary.Remove(GetBookByName(title));
                    }
                }
                else
                {
                    Console.WriteLine("Book not found, nothing to delete");
                }
        }

        public void CheckBookFromLibrary(string title, string user)
        {
           if (CheckForMaxLending(user))
            {
                Console.WriteLine("Dear {0}, ou reach the limit, max 3 books can be lended to one user", user);
            }
            else
            {
                GetBookFromLibrary(title, user);
            }
        }

        private bool CheckForMaxLending(string user)
        {
            return BookLibrary.FindAll(book => book.UserName == user).Count() == 3;
        }

        private void GetBookFromLibrary(string title, string user)
        {
            var book = GetBookByName(title); 

            if (book != null && book.IsAvailable)
            {
                book.IsAvailable = false;
                book.ReservationDate = DateTime.Now;
                book.LatestReturnDate = GetReturnDate();
                book.UserName = user;
                Console.WriteLine("Reservation successed");

            }
            else if (book != null && !book.IsAvailable)
            {
                Console.WriteLine("this book is lended till {0}", book.LatestReturnDate);
            }
            else
            {
                Console.WriteLine("Sorry, we can not find book, you are looking for");
            }
        }

        private DateTime? GetReturnDate()
        {
            Console.WriteLine("Please enter book's return date as YYYY/MM/DD or DD/MM/YYYY");
            string inputString = Console.ReadLine();

            DateTime returnDate = TimeInputService.GetTimeFromInput(inputString);

            if(returnDate <= DateTime.Now || returnDate > DateTime.Now.AddMonths(2))
            {
                Console.WriteLine("Not valid date selected, you are granted default 2 monthes terms");
                return DateTime.Now.AddMonths(2);
            }
            else
            {
                return returnDate;
            }
        }

        public bool ReturnBookToLibrary(string title)
        {
            var book = GetBookByName(title);

            if (book != null)
            {
                CheckForLateReturn(book);
                book.IsAvailable = true;
                book.ReservationDate = null;
                book.LatestReturnDate = null;
                book.UserName = null;
                return true;
            }

            return false;
        }

        public void PrintBookDetails(Book book)
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Title: {0}, Author: {1}, Category: {2}, Language: {3}, Published in {4}, ISBN:{5}:",
                               book.Title, 
                               book.Author,
                               book.Category, 
                               book.Language,
                               book.PublicationDate.Year, 
                               book.ISBN);
            if (book.IsAvailable)
            {
                Console.WriteLine("this book is available for lending");
            }
            else
            {
                Console.WriteLine("this book is lended to user {1} till {0}", book.LatestReturnDate, book.UserName);
            }

        }

        private void CheckForLateReturn(Book book)
        {
            if (book.LatestReturnDate != null)
            {
                DateTime latestReturn = (DateTime)book.LatestReturnDate;
                string message = DateTime.Compare(DateTime.Now, latestReturn) > 0 ? "You are late!!" : " Thanks being in time" ;
                Console.WriteLine(message);
            };

        }

        private Book GetBookByName(string title)
        {
            return BookLibrary.Find(b => b.Title == title);
        }

        internal List<Book> FilterBooksByLanguage(Book.Languages language)
        {
            return BookLibrary.FindAll(book => book.Language == language);
        }

        internal List<Book> FilterBooksByTitle(string title)
        {
            return BookLibrary.FindAll(b => b.Title == title);
        }

        internal List<Book> FilterBooksByCategory(Book.Categories category)
        {
            return BookLibrary.FindAll(book => book.Category == category);
        }

        internal List<Book> FilterBooksByLending(string choice)
        {

            List<Book> filteredBooks; 
            if(choice == "a")
            {
                filteredBooks = BookLibrary.FindAll(book => book.IsAvailable == true);
                if (filteredBooks.Count == 0) Console.WriteLine("All books are lended, no avalable books.");
                return filteredBooks;
            }
            else if(choice == "l")
            {
                filteredBooks = BookLibrary.FindAll(book => book.IsAvailable == false);
                if (filteredBooks.Count == 0) Console.WriteLine("All books are availabe, no lended books.");
                return filteredBooks;
            }
            else
            {
                Console.WriteLine("Not valid choice, here is full list of books");
                return BookLibrary;
            }
        }

        internal List<Book> FilterBooksByAuthor(string author)
        {
            return BookLibrary.FindAll(book => book.Author == author);
        }

        
    }
}
