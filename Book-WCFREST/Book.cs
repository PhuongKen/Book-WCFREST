using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Book_WCFREST
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public int BookId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string ISBN { get; set; }
    }

    public interface IBookRepository {
        List<Book> GetAllBooks();

        Book GetBookById(int id);

        Book AddNewBook(Book item);

        bool DeleteBook(int id);

        bool UpdateBook(Book item);
    }

    public class BookRepository : IBookRepository
    {
        List<Book> books = new List<Book>();

        private int counter = 1;
        public Book AddNewBook(Book item)
        {
            if (item == null)
                throw new NotImplementedException();
            item.BookId = counter++;
            books.Add(item);
            return item;
        }

        public bool DeleteBook(int id)
        {
            int idx = books.FindIndex(b => b.BookId == id);
            if(idx == -1)
            {
                return false;
            }
            books.RemoveAll(b => b.BookId == id);
            return true;
            //throw new NotImplementedException();
        }

        public List<Book> GetAllBooks()
        {
            return books;
            //throw new NotImplementedException();
        }

        public Book GetBookById(int id)
        {
            return books.Find(b => b.BookId == id);
            //throw new NotImplementedException();
        }

        public bool UpdateBook(Book item)
        {

            int idx = books.FindIndex(b => b.BookId == item.BookId);
            if (idx == -1)
            {
                return false;
            }
            books.RemoveAt(idx);
            books.Add(item);
            return true;
            //throw new NotImplementedException();
        }
    }
}