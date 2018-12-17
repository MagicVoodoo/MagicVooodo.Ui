namespace MagicVoodoo.Xamarin
{
    public interface IMessage
    {
       string Topic { get; set; }
       object Content { get; set; }
    }

    public class Message : IMessage
    {
        public string Topic { get; set; }
        public object Content { get; set; }
    }

    public class Message<T> : Message
    {
        public new T Content     { get; set; }
    }
}