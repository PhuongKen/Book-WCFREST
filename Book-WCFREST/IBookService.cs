using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Book_WCFREST
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBookService
    {
        [OperationContract]
        [WebInvoke
            (Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Books/")]
        List<Book> GetBookList();

        [OperationContract]
        [WebInvoke
            (Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Books/{id}")]
        Book GetBookById(string id);

        [OperationContract]
        [WebInvoke
            (Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "AddBook/")]
        string AddBook(Book book);

        [OperationContract]
        [WebInvoke
            (Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "UpdateBook/")]
        string UpdateBook(Book book);

        [OperationContract]
        [WebInvoke
            (Method = "DELETE",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DeleteBook/{id}")]
        string DeleteBook(string id);
    }
    
}
