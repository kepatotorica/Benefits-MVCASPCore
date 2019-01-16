

namespace Benefacts
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }
    }

    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 465;
        public string SmtpUsername { get; set; } = "benefacts2000@gmail.com";
        public string SmtpPassword { get; set; } = "1q2ww3eee4rrrr";
    }
}