namespace Renta.Models
{
    public class Message
    {
        public string? Id { get; set; }
        public string? Sender { get; set; }
        public string Text { get; set; }

        public DateTime Time { get; set; }

        public Message()
        {
        }

        public Message(string? sender, string text, DateTime time)
        {
            Sender = sender;
            Text = text;
            Time = time;
        }
    }
}