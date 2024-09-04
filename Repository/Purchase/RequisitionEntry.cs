namespace EPAPI.Repository.Purchase
{
    public class RequisitionEntry
    {
    }
    public class ReqPayLoad
    {
        public int ReqNum { get; set; }
        public string ReplyOption { get; set; }
        public string? ReqActionID {  get; set; }
        public string? ReqActionIDReqActionDesc {  get; set; }
        public string? NextActionID { get; set; }
        public string? NextDispatcherID { get; set; }
        public string? CurrDispatcherID { get; set; }
        public string? CurrDispatcherName { get; set; }
        public string? ApproveMsg { get; set; }
        public string? nik { get; set; }
        public string? password { get; set; } 
    }
    public class ReqHead
    {
        public string? Company { get; set; } = "SAI";
        public bool? OpenReq { get; set; } = true;
        public int? ReqNum { get; set; } = 0;
        public string? RequestorID { get; set; } = "270723-001";
        public DateTime? RequestDate { get; set; } = DateTime.Parse("2024-08-15T00:00:00");
        public string? ShipName { get; set; } = "PT. SUMMIT ADYAWINSA INDONESIA";
        public string? ShipAddress1 { get; set; } = "JL. PANGKAL PERJUANGAN NO. 98 RT. 005 RW. 014";
        public string? ShipAddress2 { get; set; } = "TANJUNGMEKAR";
        public string? ShipAddress3 { get; set; } = "KEC. KARAWANG BARAT";
        public string? ShipCity { get; set; } = "KARAWANG";
        public string? ShipState { get; set; } = "JAWA BARAT";
        public string? ShipZIP { get; set; } = "";
        public string? ShipCountry { get; set; } = "";
        public int? PrcConNum { get; set; } = 0;
        public string? CommentText { get; set; } = "";
        public string? ShipToConName { get; set; } = "";
        public int? ShipCountryNum { get; set; } = 1;
        public string? ReqActionID { get; set; } = "";
        public string? CurrDispatcherID { get; set; } = "251220-001";
        public string? CurrDispatcherName { get; set; } = "";
        public bool? NotifyUponReceipt { get; set; } = false;
        public string? Note { get; set; } = "";
        public string? StatusType { get; set; } = "P";
        public string? GlbCompany { get; set; } = "";
        public int? GlbReqNum { get; set; } = 0;
        public string? CPDispatcherID { get; set; } = "";
        public long? SysRevID { get; set; } = 19691797;
        public string? SysRowID { get; set; } = "fbe0e8b9-418d-4f59-9518-f3817d378a4a";
        public string? CreatedBy { get; set; } = "";
        public DateTime? CreatedOn { get; set; } = DateTime.Parse("2024-08-15T08:43:10.783");
        public string? AddrList { get; set; } = "SAI~JL. PANGKAL PERJUANGAN NO. 98 RT. 005 RW. 014~TANJUNGMEKAR~KEC. KARAWANG BARAT~KARAWANG~JAWA BARAT~41316    INDONESIA";
        public string? CurrDispatcherCurMenuID { get; set; } = "";
        public bool? LockLines { get; set; } = false;
        public bool? MemoAvailable { get; set; } = false;
        public string? NextActionDesc { get; set; } = "";
        public string? NextActionID { get; set; } = "";
        public string? NextDispatcherID { get; set; } = "";
        public string? NextDispatcherName { get; set; } = "";
        public string? NextNote { get; set; } = "";
        public bool? OkToBuy { get; set; } = false;
        public string? ReplyOption { get; set; } = "";
        public string? RequestorIDCurMenuID { get; set; } = "";
        public string? ReqUserId { get; set; } = "";
        public string? ReqUserID { get; set; } = "";
        public string? StatusDesc { get; set; } = "";
        public bool? ToDoFlag { get; set; } = false;
        public string? ShipToAddressFormatted { get; set; } = "PT. SUMMIT ADYAWINSA INDONESIA\r\nJL. PANGKAL PERJUANGAN NO. 98 RT. 005 RW. 014\r\nTANJUNGMEKAR\r\nKEC. KARAWANG BARAT\r\nKARAWANG\r\nJAWA BARAT\r\n41316\r\n\r\n";
        public int? BitFlag { get; set; } = 0;
        public string? ReqActionIDReqActionDesc { get; set; } = "";
        public string? RequestorIDName { get; set; } = "";
        public bool? XbSystDisableOverridePriceListOption { get; set; } = false;
        public string? RowMod { get; set; } = "";
        public bool? AlreadyPO_c { get; set; } = false;
        public string? DocNum_c { get; set; } = "";
        public string? ProjectCode_c { get; set; } = "";
        public string? ReqCategory_c { get; set; } = "";
        public string? UD_SysRevID { get; set; } = "AAAAAAEscG8=";
    }
    public class PRResponse
    {
        public Parameters parameters { get; set; }
    }
    
}
