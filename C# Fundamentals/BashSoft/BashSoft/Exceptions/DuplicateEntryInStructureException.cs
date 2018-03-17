using System;

namespace BashSoft.Exceptions
{
    public class DuplicateEntryInStructureException : Exception
    {
        private const string DUPLICATE_ENTRY = "The {0} already exists in {1}.";

        public DuplicateEntryInStructureException(string message) : base(message) { }

        public DuplicateEntryInStructureException(string entry, string structureName) 
            : base(string.Format(DUPLICATE_ENTRY, entry, structureName)) { }
    }
}
