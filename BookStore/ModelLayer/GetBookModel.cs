using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public class GetBookModel
    {
        public string BookID { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string rating { get; set; }
        public string PeopleRated { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string BookImage { get; set; }
    }
}
