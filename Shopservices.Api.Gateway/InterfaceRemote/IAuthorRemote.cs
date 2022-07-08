using System;
using System.Threading.Tasks;
using Shopservices.Api.Gateway.BookRemote;

namespace Shopservices.Api.Gateway.InterfaceRemote
{
    public interface IAuthorRemote
    {
         Task<(bool result, AuthorModelRemote author, string ErrorMessage) > GetAuthor(Guid AuthorId);
    }
}