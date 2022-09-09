using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IBookML
    {
        public bool AddBook(BookModel bookModel);
        public bool UpdateBook(BookModel bookModel, int BookID);
        public bool DeleteBook(int BookID);
        public IEnumerable<GetBookModel> GetAllBooks();
        public IEnumerable<GetBookModel> GetBookById(int BookID);

    }
}
