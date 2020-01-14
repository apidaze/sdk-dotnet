namespace APIdaze.SDK.Messages
{
    public interface IMessage
    {
        string SendTextMessage(PhoneNumber from, PhoneNumber to, string bodyMessage);
    }
}