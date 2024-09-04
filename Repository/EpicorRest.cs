using EpicorRestAPI;
using EpicorRestSharedClasses;

namespace EPAPI.Repository
{
    public class EpicRest
    {
        public IConfiguration? Configuration { get; }

        public static string urlpwoapi = "https://epicor.summitadyawinsa.co.id/app/api/v2/odata/SAI";
        public static string AppPoolHost = "epicor.summitadyawinsa.co.id";
        //public static string key = "K9Ku0h6qpTqPtULQ0cu9j9cdzrLODBZcmrTBmFxkTT2o2"; //PILOT
        //public static string key = "BO3k5AR3vGXvav1aDRLz6I3CudXSAhaym5HebH2z9LlxN"; //UAT
        public static string key = "CGgBRV0fZcIGTMP1vtnicaeWeguYNiU7FjvmejOWyR7nu"; // dev
        //public static string key = "hbaXzBKAudmdN4jH2BbOje64YCsCMnpYcUysQsYbPe161"; // app

        public static string AppPoolInstance = "dev";
        public static string EpiUser = "270723-001";
        public static string EpiPass = "Epicor123";
        public static string Company = "SAI";


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
