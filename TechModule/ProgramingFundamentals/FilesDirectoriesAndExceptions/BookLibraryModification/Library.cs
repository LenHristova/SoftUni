using System.Collections.Generic;

namespace BookLibraryModification
{
    class Library
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
