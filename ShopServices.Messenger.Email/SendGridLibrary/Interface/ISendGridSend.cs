using System.Threading.Tasks;
using ShopServices.Messenger.Email.SendGridLibrary.Model;

namespace ShopServices.Messenger.Email.SendGridLibrary.Interface
{
    public interface ISendGridSend
    {
        Task<(bool result, string errorMessage)> SendMail( SendGridData data);
    }
}