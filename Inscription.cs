namespace ConsumerAndMailer
{
    internal class Inscription
    {
        public required string MailAddress { get; set; }
        public required string FullName { get; set; }
        public string? JWT { get; set; }
    }
}
