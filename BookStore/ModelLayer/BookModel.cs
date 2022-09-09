using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public class BookModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int rating { get; set; }
        public int PeopleRated { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string BookImage { get; set; }
    }
}
