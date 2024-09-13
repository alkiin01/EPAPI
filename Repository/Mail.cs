namespace EPAPI.Repository
{
    public class Mail
    {
        public string EmailFrom { get; set; } = "system@summitadyawinsa.co.id";
        public string EmailTo { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; } = "";
        public string AttachmentsDictionaryJson { get; set; } = "";
        public bool BodyIsHtml { get; set; } = true;
        public bool SendAsync { get; set; } = true;
        public string EmailCC { get; set; } = "";
        public string EmailBCC { get; set; } = "";
        public string EmailReplyTo { get; set; } = "";
        public string EmailMessageID { get; set; } = "";
        public string EmailPriority { get; set; } = "";
        public string nik {  get; set; } = "";
        public string password {  get; set; } = "";
    }
}
