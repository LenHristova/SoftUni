namespace Chushka.Common.Models.Home
{
    using Products;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public string LoggedInUserFullName { get; set; }

        public ICollection<ICollection<IndexProductViewModel>> RowsProducts { get; set; }
    }
}
