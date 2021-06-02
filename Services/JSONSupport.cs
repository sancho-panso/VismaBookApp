using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Services
{
    public static class JSONSupport
    {
        public static void SaveBooksToJSONFile(List<Book> books)
        {
            string jsonString = JsonSerializer.Serialize(books);
            File.WriteAllText("Books", jsonString);
        }        
        
        public static List<Book> ReadBooksFromJSONFile()
        {
            string jsonString = File.ReadAllText("Books");

            var books = JsonSerializer.Deserialize<List<Book>>(jsonString);

            if (books == null || books.Count == 0) books = new List<Book>();

            return books;
        }
    }
}
