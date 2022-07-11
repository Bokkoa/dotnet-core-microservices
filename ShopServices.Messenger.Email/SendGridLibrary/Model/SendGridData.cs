namespace ShopServices.Messenger.Email.SendGridLibrary.Model
{
    public class SendGridData
    {
        public string SendGridApiKey { get; set; }
        public string EmailReceiver { get; set; }
        public string NameReceiver { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}