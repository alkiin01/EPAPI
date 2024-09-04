namespace EPAPI.Repository
{
    public class User
    {
        public string nik {  get; set; }
        public string password { get; set; }
    }
    public class UserFile
    {
        public string UserID { get; set; }
        public string Name {  get; set; }
        public string EMailAddress {  get; set; }
        public bool SecurityMgr { get; set; }
        public string GroupList { get; set; }


    }
}
