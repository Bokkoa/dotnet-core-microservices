using System;
using System.Threading.Tasks;
using ShopServices.Api.Cart.RemoteModel;

namespace ShopServices.Api.Cart.RemoteInterfaces
{
    public interface IBookService
    {
         Task<(bool result, BookRemote book, string ErrorMessage )>GetBook(Guid BookId);
    }
}