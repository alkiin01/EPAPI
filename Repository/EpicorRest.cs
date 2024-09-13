using EpicorRestAPI;
using EpicorRestSharedClasses;

namespace EPAPI.Repository
{
    public class EpicRest
    {
        public IConfiguration? Configuration { get; }

        public static string AppPoolHost = "epicor.epicor.com";
        public static string key = "your x-api key"; 

        public static string AppPoolInstance = "dev";
        public static string EpiUser = "yourUserName";
        public static string EpiPass = "yourPassword";
        public static string Company = "yourCompany";


        public bool CreateBearer()
        {
            EpicorRest.AppPoolHost = AppPoolHost; //TLD to your Epicor Server
            EpicorRest.AppPoolInstance = AppPoolInstance; //Epicor AppServer Insdtance
            EpicorRest.UserName = EpiUser; //Epicor Username
            EpicorRest.Password = EpiPass; //Epicor Password
            EpicorRest.APIKey = key; //API key required with V2
            EpicorRest.Company = Company; //Epicor Company (Current company) Required with V2
            EpicorRest.APIVersion = EpicorRestVersion.V2; //DataCollections to V2
            EpicorRest.License = EpicorLicenseType.DataCollection;
            EpicorRest.IgnoreCertErrors = true; // If you are too cheap to get a free Certificate from Let's 
            if (EpicorRest.CreateBearerToken()) //Generates a Token and Assigns it to the API calls
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PortalBeearer(User user)
        {
            EpicorRest.AppPoolHost = AppPoolHost; //TLD to your Epicor Server
            EpicorRest.AppPoolInstance = AppPoolInstance; //Epicor AppServer Insdtance
            EpicorRest.UserName = user.nik; //Epicor Username
            EpicorRest.Password = user.password; //Epicor Password
            EpicorRest.APIKey = key; //API key required with V2
            EpicorRest.Company = Company; //Epicor Company (Current company) Required with V2
            EpicorRest.APIVersion = EpicorRestVersion.V2; //DataCollections to V2
            EpicorRest.License = EpicorLicenseType.DataCollection;
            EpicorRest.IgnoreCertErrors = true; // If you are too cheap to get a free Certificate from Let's 
            if (EpicorRest.CreateBearerToken()) //Generates a Token and Assigns it to the API calls
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CreateLogin(User user)
        {
            EpicorRest.AppPoolHost = AppPoolHost; //TLD to your Epicor Server
            EpicorRest.AppPoolInstance = AppPoolInstance; //Epicor AppServer Insdtance
            EpicorRest.UserName = user.nik; //Epicor Username
            EpicorRest.Password = user.password; //Epicor Password
            EpicorRest.APIKey = key; //API key required with V2
            EpicorRest.Company = Company; //Epicor Company (Current company) Required with V2
            EpicorRest.APIVersion = EpicorRestVersion.V2; //DataCollections to V2
            EpicorRest.License = EpicorLicenseType.DataCollection;
            EpicorRest.IgnoreCertErrors = true; // If you are too cheap to get a free Certificate from Let's 
            if (EpicorRest.CreateBearerToken()) //Generates a Token and Assigns it to the API calls
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
