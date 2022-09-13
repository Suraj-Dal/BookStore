using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public bool AddBook(BookModel bookModel);
        public bool UpdateBook(BookModel bookModel, int BookID);
        public bool DeleteBook(int BookID);
        public IEnumerable<GetBookModel> GetAllBooks();
        public GetBookModel GetBookById(int BookID);
    }
}
