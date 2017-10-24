using System.Collections.Generic;

namespace BookLibrary
{
    class Library
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
