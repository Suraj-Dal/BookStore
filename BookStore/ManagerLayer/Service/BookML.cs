using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Service
{
    public class BookML : IBookML
    {
        private readonly IBookRL ibookRL;
        public BookML(IBookRL ibookRL)
        {
            this.ibookRL = ibookRL;
        }

        public bool AddBook(BookModel bookModel)
        {
            try
            {
                return ibookRL.AddBook(bookModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBook(BookModel bookModel, int BookID)
        {
            try
            {
                return ibookRL.UpdateBook(bookModel, BookID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteBook(int BookID)
        {
            try
            {
                return ibookRL.DeleteBook(BookID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<GetBookModel> GetAllBooks()
        {
            try
            {
                return ibookRL.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<GetBookModel> GetBookById(int BookID)
        {
            try
            {
                return ibookRL.GetBookById(BookID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
