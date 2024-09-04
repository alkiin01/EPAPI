
namespace EPAPI.Repository.Purchase
{
    
    public class PurchaseOrder
    {
        public string? Company { get; set; } = "SAI";
        public int PONum { get; set; } = 0;
        public string? MsgType { get; set; } = "1";
        public DateTime? MsgDate { get; set; } = null;
        public int MsgTime { get; set; } = 0;
        public string? MsgTo { get; set; } = "";
        public string? MsgFrom { get; set; } = "";
        public string? MsgText { get; set; } = "";
        public string? ApproverResponse { get; set; } = "";
        public string? DcdUserID { get; set; } = "";
        public int SysRevID { get; set; } = 0;
        public string? SysRowID { get; set; } = "0";
        public string? VendorName { get; set; } = "CV KEMBAR JAYA TEHNIK";
        public string? BuyerName { get; set; } = "";
        public decimal BuyerLimit { get; set; } =0 ;
        public decimal POAmt { get; set; } =0 ;
        public decimal ApvAmt { get; set; } = 0;
        public string? MsgTimeString { get; set; } = "";
        public string? ApproverName { get; set; } = "";
        public int BitFlag { get; set; } = 0;
        public string? MsgToName { get; set; } = "";
        public string? RowMod { get; set; } = "";
        public int SeqNum { get; set; }
        public string? nik { get; set; } 
        public string? password {  get; set; }
    }
    public class POApvMsg {
        public string? Company { get; set; } = "SAI";
        public int PONum { get; set; } = 0;
        public string? MsgType { get; set; } = "1";
        public DateTime? MsgDate { get; set; } = null;
        public int MsgTime { get; set; } = 0;
        public string? MsgTo { get; set; } = "";
        public string? MsgFrom { get; set; } = "";
        public string? MsgText { get; set; } = "";
        public string? ApproverResponse { get; set; } = "";
        public string? DcdUserID { get; set; } = "";
        public int SysRevID { get; set; } = 0;
        public string? SysRowID { get; set; } = "0";
        public string? VendorName { get; set; } = "CV KEMBAR JAYA TEHNIK";
        public string? BuyerName { get; set; } = "";
        public decimal BuyerLimit { get; set; } = 0;
        public decimal POAmt { get; set; } = 0;
        public decimal ApvAmt { get; set; } = 0;
        public string? MsgTimeString { get; set; } = "";
        public string? ApproverName { get; set; } = "";
        public int BitFlag { get; set; } = 0;
        public string? MsgToName { get; set; } = "";
        public string? RowMod { get; set; } = "";
    }

    public class POHeader
    {
        public string? Company { get; set; } = "";
        public bool OpenOrder { get; set; } = true; // Default value from API
        public bool VoidOrder { get; set; } = false; // Default value from API
        public int? PONum { get; set; } = 0;
        public string? EntryPerson { get; set; } = "";
        public DateTime? OrderDate { get; set; } = null;
        public string? FOB { get; set; } = "";
        public string? ShipViaCode { get; set; } = "";
        public string? TermsCode { get; set; } = "";
        public string? ShipName { get; set; } = "";
        public string? ShipAddress1 { get; set; } = "";
        public string? ShipAddress2 { get; set; } = "";
        public string? ShipAddress3 { get; set; } = "";
        public string? ShipCity { get; set; } = "";
        public string? ShipState { get; set; } = "";
        public string? ShipZIP { get; set; } = "";
        public string? ShipCountry { get; set; } = "";
        public string? BuyerID { get; set; } = "";
        public bool FreightPP { get; set; } = false; // Default value from API
        public int? PrcConNum { get; set; } = 0;
        public int? VendorNum { get; set; } = 0;
        public string? PurPoint { get; set; } = "";
        public string? CommentText { get; set; } = "";
        public bool OrderHeld { get; set; } = false; // Default value from API
        public string? ShipToConName { get; set; } = "";
        public bool ReadyToPrint { get; set; } = true; // Default value from API
        public string? PrintAs { get; set; } = "";
        public string? CurrencyCode { get; set; } = "";
        public decimal? ExchangeRate { get; set; } = 1; // Default value from API
        public bool LockRate { get; set; } = false; // Default value from API
        public int? ShipCountryNum { get; set; } = 1; // Default value from API
        public bool LogChanges { get; set; } = false; // Default value from API
        public DateTime? ApprovedDate { get; set; } = null;
        public string? ApprovedBy { get; set; } = "";
        public bool Approve { get; set; } = true; // Default value from API
        public string? ApprovalStatus { get; set; } = "";
        public decimal? ApprovedAmount { get; set; } = 0;
        public bool PostToWeb { get; set; } = false; // Default value from API
        public DateTime? PostDate { get; set; } = null;
        public string? VendorRefNum { get; set; } = "";
        public bool ConfirmReq { get; set; } = false; // Default value from API
        public bool Confirmed { get; set; } = false; // Default value from API
        public string? ConfirmVia { get; set; } = "";
        public int? OrderNum { get; set; } = 0;
        public string? LegalNumber { get; set; } = "";
        public bool Linked { get; set; } = false; // Default value from API
        public string? ExtCompany { get; set; } = "";
        public string? XRefPONum { get; set; } = "";
        public bool ConsolidatedPO { get; set; } = false; // Default value from API
        public string? GlbCompany { get; set; } = "";
        public bool ContractOrder { get; set; } = false; // Default value from API
        public DateTime? ContractStartDate { get; set; } = null;
        public DateTime? ContractEndDate { get; set; } = null;
        public bool PrintHeaderAddress { get; set; } = true; // Default value from API
        public string? RateGrpCode { get; set; } = "";
        public string? POType { get; set; } = "";
        public string? APLocID { get; set; } = "";
        public string? TranDocTypeID { get; set; } = "";
        public bool AutoPrintReady { get; set; } = false; // Default value from API
        public bool ICPOLocked { get; set; } = false; // Default value from API
        public int? SysRevID { get; set; } = 0;
        public string? SysRowID { get; set; } = "";
        public DateTime? DueDate { get; set; } = null;
        public DateTime? PromiseDate { get; set; } = null;
        public string? ChangedBy { get; set; } = "";
        public DateTime? ChangeDate { get; set; } = null;
        public bool POTaxReadyToProcess { get; set; } = true; // Default value from API
        public string? TaxRegionCode { get; set; } = "";
        public DateTime? TaxPoint { get; set; } = null;
        public DateTime? TaxRateDate { get; set; } = null;
        public decimal? TotalTax { get; set; } = 0;
        public decimal? DocTotalTax { get; set; } = 0;
        public decimal? Rpt1TotalTax { get; set; } = 0;
        public decimal? Rpt2TotalTax { get; set; } = 0;
        public decimal? Rpt3TotalTax { get; set; } = 0;
        public decimal? TotalWhTax { get; set; } = 0;
        public decimal? DocTotalWhTax { get; set; } = 0;
        public decimal? Rpt1TotalWhTax { get; set; } = 0;
        public decimal? Rpt2TotalWhTax { get; set; } = 0;
        public decimal? Rpt3TotalWhTax { get; set; } = 0;
        public decimal? TotalSATax { get; set; } = 0;
        public decimal? DocTotalSATax { get; set; } = 0;
        public decimal? Rpt1TotalSATax { get; set; } = 0;
        public decimal? Rpt2TotalSATax { get; set; } = 0;
        public decimal? Rpt3TotalSATax { get; set; } = 0;
        public bool InPrice { get; set; } = false; // Default value from API
        public bool HdrTaxNoUpdt { get; set; } = false; // Default value from API
        public string? TaxRateGrpCode { get; set; } = "";
        public decimal? TotalDedTax { get; set; } = 0;
        public decimal? DocTotalDedTax { get; set; } = 0;
        public decimal? Rpt1TotalDedTax { get; set; } = 0;
        public decimal? Rpt2TotalDedTax { get; set; } = 0;
        public decimal? Rpt3TotalDedTax { get; set; } = 0;
        public decimal? TotalCharges { get; set; } = 0;
        public decimal? TotalMiscCharges { get; set; } = 0;
        public decimal? TotalOrder { get; set; } = 0;
        public decimal? DocTotalCharges { get; set; } = 0;
        public decimal? DocTotalMisc { get; set; } = 0;
        public decimal? DocTotalOrder { get; set; } = 0;
        public decimal? Rpt1TotalCharges { get; set; } = 0;
        public decimal? Rpt2TotalCharges { get; set; } = 0;
        public decimal? Rpt3TotalCharges { get; set; } = 0;
        public decimal? Rpt1TotalMiscCharges { get; set; } = 0;
        public decimal? Rpt2TotalMiscCharges { get; set; } = 0;
        public decimal? Rpt3TotalMiscCharges { get; set; } = 0;
        public decimal? Rpt1TotalOrder { get; set; } = 0;
        public decimal? Rpt2TotalOrder { get; set; } = 0;
        public decimal? Rpt3TotalOrder { get; set; } = 0;
        public int? APTaxRoundOption { get; set; } = 0;
        public bool CNBonded { get; set; } = false; // Default value from API
        public int? EDIRevNum { get; set; } = 0;
        public bool EDIPosted { get; set; } = false; // Default value from API
        public DateTime? EDIPostedDate { get; set; } = null;
        public DateTime? EDIAckDate { get; set; } = null;
        public string? ApproveMessage { get; set; } = "";
        public bool RecalcUnitCosts { get; set; } = false; // Default value from API
        public int? RuleCode { get; set; } = 0;
        public bool UpdateDtlAndRelRecords { get; set; } = false; // Default value from API
        public string? VendCntFaxNum { get; set; } = "";
        public string? VendCntPhoneNumber { get; set; } = "";
        public bool ApproveChkBxSensitive { get; set; } = true; // Default value from API
        public string? BaseCurrencyID { get; set; } = "";
        public bool ConfirmChkBxSensitive { get; set; } = true; // Default value from API
        public bool EnableSupplierID { get; set; } = true; // Default value from API
        public bool HasLines { get; set; } = true; // Default value from API
        public bool HoldChkBxSensitive { get; set; } = true; // Default value from API
        public bool MassPrntChkBxSensitive { get; set; } = true; // Default value from API
        public string? RefCodeCurrSymbol { get; set; } = "";
        public string? VendAddrFormat { get; set; } = "";
        public bool EDIEnable { get; set; } = false; // Default value from API
        public int? BitFlag { get; set; } = 2; // Default value from API
        public string? APLocDescription { get; set; } = "";
        public string? BuyerIDName { get; set; } = "";
        public string? CurrencyCodeCurrName { get; set; } = "";
        public string? CurrencyCodeCurrSymbol { get; set; } = "";
        public string? CurrencyCodeDocumentDesc { get; set; } = "";
        public string? CurrencyCodeCurrDesc { get; set; } = "";
        public string? CurrencyCodeCurrencyID { get; set; } = "";
        public string? FOBDescription { get; set; } = "";
        public string? RateGrpDescription { get; set; } = "";
        public string? ShipCountryNumDescription { get; set; } = "";
        public bool ShipViaCodeInactive { get; set; } = false; // Default value from API
        public string? ShipViaCodeDescription { get; set; } = "";
        public string? ShipViaCodeWebDesc { get; set; } = "";
        public string? TaxRegionCodeDescription { get; set; } = "";
        public string? TermsCodeDescription { get; set; } = "";
        public string? VendorVendorID { get; set; } = "";
        public string? VendorZIP { get; set; } = "";
        public string? VendorDefaultFOB { get; set; } = "";
        public string? VendorCity { get; set; } = "";
        public string? VendorName { get; set; } = "";
        public string? VendorCountry { get; set; } = "";
        public string? VendorAddress3 { get; set; } = "";
        public string? VendorTermsCode { get; set; } = "";
        public string? VendorAddress1 { get; set; } = "";
        public string? VendorAddress2 { get; set; } = "";
        public string? VendorCurrencyCode { get; set; } = "";
        public string? VendorState { get; set; } = "";
        public bool VendorEDISupplier { get; set; } = false; // Default value from API
        public string? VendorCntName { get; set; } = "";
        public string? VendorCntEmailAddress { get; set; } = "";
        public string? VendorCntPhoneNum { get; set; } = "";
        public string? VendorCntFaxNum { get; set; } = "";
        public string? VendorPPAddress3 { get; set; } = "";
        public string? VendorPPCountry { get; set; } = "";
        public string? VendorPPZip { get; set; } = "";
        public string? VendorPPState { get; set; } = "";
        public string? VendorPPAddress1 { get; set; } = "";
        public string? VendorPPName { get; set; } = "";
        public int? VendorPPPrimPCon { get; set; } = 0;
        public string? VendorPPAddress2 { get; set; } = "";
        public string? VendorPPCity { get; set; } = "";
        public bool XbSystAllowLinkedPOChg { get; set; } = false; // Default value from API

        public int? XbSystPOUserInt1Label { get; set; } = 0;

        public bool XbSystDisableOverridePriceListOption { get; set; } = false; // Default value from API
        public bool XbSystPOTaxCalculate { get; set; } = true; // Default value from API
        public bool XbSystAPTaxLnLevel { get; set; } = true; // Default value from API
        public string? RowMod { get; set; } = "";
        public string? DocNum_c { get; set; } = "";
        public string? ProjectCode_c { get; set; } = "";
        public string? ReqCategory_c { get; set; } = "";
        public string? UD_SysRevID { get; set; } = "";
    }

    public class POResponse
    {
        public Parameters parameters { get; set; }
    }
    public class Parameters
    {
        public Ds ds { get; set; }
    }

    public class Ds
    {
        public List<POHeader> POHeader { get; set; }
        public List<ReqHead> PRHeader { get; set; }
    }

    public class SetApprove
    {
#warning Using UD39
        public int? PONum { set; get; } //Key3
        public string? NikCheck { set; get; }//ShortChar03
        public string? ActCheck { set; get; } //ShortChar04
        public string? NikApprove { set; get; }//ShortChar05
        public string? ActApprove { set; get; }//ShortChar06
        public string? NikLegalize { set; get; }//ShortChar07
        public string? ActLegalize { set; get; }//ShortChar08
        public string? nik { get; set; }
        public string? password { get; set; }
        public string? RowMod { get; set; }

    }

    public class UD39
    {
        public string? Company { get; set; }
        public string? Key1 { get; set; }
        public string? Key2 { get; set; }
        public string? Key3 { get; set; }
        public string? Key4 { get; set; }
        public string? Key5 { get; set; }
        public string? Character01 { get; set; }
        public string? Character02 { get; set; }
        public string? Character03 { get; set; }
        public string? Character04 { get; set; }
        public string? Character05 { get; set; }
        public string? Character06 { get; set; }
        public string? Character07 { get; set; }
        public string? Character08 { get; set; }
        public string? Character09 { get; set; }
        public string? Character10 { get; set; }
        public int? Number01 { get; set; }
        public int? Number02 { get; set; }
        public int? Number03 { get; set; }
        public int? Number04 { get; set; }
        public int? Number05 { get; set; }
        public int? Number06 { get; set; }
        public int? Number07 { get; set; }
        public int? Number08 { get; set; }
        public int? Number09 { get; set; }
        public int? Number10 { get; set; }
        public int? Number11 { get; set; }
        public int? Number12 { get; set; }
        public int? Number13 { get; set; }
        public int? Number14 { get; set; }
        public int? Number15 { get; set; }
        public int? Number16 { get; set; }
        public int? Number17 { get; set; }
        public int? Number18 { get; set; }
        public int? Number19 { get; set; }
        public int? Number20 { get; set; }
        public DateTime? Date01 { get; set; }
        public DateTime? Date02 { get; set; }
        public DateTime? Date03 { get; set; }
        public DateTime? Date04 { get; set; }
        public DateTime? Date05 { get; set; }
        public DateTime? Date06 { get; set; }
        public DateTime? Date07 { get; set; }
        public DateTime? Date08 { get; set; }
        public DateTime? Date09 { get; set; }
        public DateTime? Date10 { get; set; }
        public DateTime? Date11 { get; set; }
        public DateTime? Date12 { get; set; }
        public DateTime? Date13 { get; set; }
        public DateTime? Date14 { get; set; }
        public DateTime? Date15 { get; set; }
        public DateTime? Date16 { get; set; }
        public DateTime? Date17 { get; set; }
        public DateTime? Date18 { get; set; }
        public DateTime? Date19 { get; set; }
        public DateTime? Date20 { get; set; }
        public bool? CheckBox01 { get; set; }
        public bool? CheckBox02 { get; set; }
        public bool? CheckBox03 { get; set; }
        public bool? CheckBox04 { get; set; }
        public bool? CheckBox05 { get; set; }
        public bool? CheckBox06 { get; set; }
        public bool? CheckBox07 { get; set; }
        public bool? CheckBox08 { get; set; }
        public bool? CheckBox09 { get; set; }
        public bool? CheckBox10 { get; set; }
        public bool? CheckBox11 { get; set; }
        public bool? CheckBox12 { get; set; }
        public bool? CheckBox13 { get; set; }
        public bool? CheckBox14 { get; set; }
        public bool? CheckBox15 { get; set; }
        public bool? CheckBox16 { get; set; }
        public bool? CheckBox17 { get; set; }
        public bool? CheckBox18 { get; set; }
        public bool? CheckBox19 { get; set; }
        public bool? CheckBox20 { get; set; }
        public string? ShortChar01 { get; set; }
        public string? ShortChar02 { get; set; }
        public string? ShortChar03 { get; set; }
        public string? ShortChar04 { get; set; }
        public string? ShortChar05 { get; set; }
        public string? ShortChar06 { get; set; }
        public string? ShortChar07 { get; set; }
        public string? ShortChar08 { get; set; }
        public string? ShortChar09 { get; set; }
        public string? ShortChar10 { get; set; }
        public string? ShortChar11 { get; set; }
        public string? ShortChar12 { get; set; }
        public string? ShortChar13 { get; set; }
        public string? ShortChar14 { get; set; }
        public string? ShortChar15 { get; set; }
        public string? ShortChar16 { get; set; }
        public string? ShortChar17 { get; set; }
        public string? ShortChar18 { get; set; }
        public string? ShortChar19 { get; set; }
        public string? ShortChar20 { get; set; }
        public bool? GlobalUD39 { get; set; }
        public bool? GlobalLock { get; set; }
        public int? SysRevID { get; set; }
        public Guid? SysRowID { get; set; }
        public int? BitFlag { get; set; }
        public string? RowMod { get; set; }
    }

}
