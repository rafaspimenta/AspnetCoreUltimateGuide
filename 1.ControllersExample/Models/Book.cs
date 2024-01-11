using Microsoft.AspNetCore.Mvc;

namespace _1.ControllersExample.Models
{
    public class Book
    {
        //[FromQuery]
        public int? BookId { get; set; }

        //[FromQuery] 
        public string? Author { get; set; }

        public override string ToString()
        {
            return $"Book Id {BookId}, Author {Author}";
        }
    }
}
