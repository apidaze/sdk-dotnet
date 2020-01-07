namespace APIdaze.SDK.Messages
{
    public interface IMessageAction
    {
        string SendTextMessage(PhoneNumber from, PhoneNumber to, string bodyMessage);
    }
}