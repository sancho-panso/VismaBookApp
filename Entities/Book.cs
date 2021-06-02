using System;

namespace Entities
{
    public class Book
    {
        public enum Categories
        {
            Fiction,
            Sci_Fiction,
            Not_Fiction,
            NotDefined
        }

        public enum Languages
        {
            EN,
            LT,
            DE,
            RU,
            NotDefined

        }
        public string Title { get; set; }
        public string Author { get; set; }
        public Categories Category { get; set; }
        public Languages Language { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime? ReservationDate { get; set; } = null;
        public DateTime? LatestReturnDate { get; set; } = null;
        public string UserName { get; set; } = null;
        public bool IsAvailable { get; set; } = true;
        public string ISBN { get; set; }
    }
}
