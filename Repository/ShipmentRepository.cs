namespace EPAPI.Repository
{
    public class ShipmentRepository
    {
        public class ShipHead
        {
            public string? AGAuthorizationCode { get; set; } = "";
            public DateTime? AGAuthorizationDate { get; set; } = null;
            public bool AGCOTMark { get; set; } = false;
            public string? AGCarrierCUIT { get; set; } = "";
            public string? AGDocumentLetter { get; set; } = "";
            public string? AGInvoicingPoint { get; set; } = "";
            public string? AGInvoicingPointDescription { get; set; } = "";
            public string? AGLegalNumber { get; set; } = "";
            public string? AGPrintingControlType { get; set; } = "";
            public string? AGShippingWay { get; set; } = "";
            public string? AGTrackLicense { get; set; } = "";
            public DateTime? ASNPrintedDate { get; set; } = null;
            public bool AddlHdlgFlag { get; set; } = false;
            public string? AddrList { get; set; } = "";
            public bool AllowChgAfterPrint { get; set; } = true;
            public bool ApplyChrg { get; set; } = false;
            public string? AutoInvoiceMessage { get; set; } = "";
            public bool AutoPrintReady { get; set; } = false;
            public decimal BOLLine { get; set; } = 0;
            public decimal BOLNum { get; set; } = 0;
            public decimal BTConNum { get; set; } = 0;
            public string? BTCustID { get; set; } = "";
            public decimal BTCustNum { get; set; } = 0;
            public bool BTCustNumInactive { get; set; } = false;
            public string? BTCustomerName { get; set; } = "";
            public string? BillAddr { get; set; } = "";
            public decimal BitFlag { get; set; } = 0;
            public bool CNBonded { get; set; } = false;
            public string? CNDeclarationBill { get; set; } = "";
            public bool CNSample { get; set; } = false;
            public bool COD { get; set; } = false;
            public decimal CODAmount { get; set; } = 0;
            public bool CODCheck { get; set; } = false;
            public bool CODFreight { get; set; } = false;
            public string? CarNum_c { get; set; } = "";
            public decimal CartonContentValue { get; set; } = 0;
            public string? CartonStageNbr { get; set; } = "";
            public bool CertOfOrigin { get; set; } = false;
            public DateTime? ChangeDate { get; set; } = null;
            public DateTime? ChangeDateTime { get; set; } = null;
            public decimal ChangeTime { get; set; }
            public string? ChangedBy { get; set; } = "";
            public string? CheckOrderMessage { get; set; } = "";
            public decimal ChrgAmount { get; set; } = 0;
            public bool CommercialInvoice { get; set; } = false;
            public string? Company { get; set; } = "SAI";
            public DateTime? CreatedOn { get; set; } = null;
            public string? CreditCardMessage { get; set; } = "";
            public bool CreditHold { get; set; } = false;
            public string? CtnPkgCode { get; set; } = "";
            public string? CurrencyCode { get; set; } = "";
            public string? CurrencyCodeCurrDesc { get; set; } = "";
            public string? CurrencyCodeCurrName { get; set; } = "";
            public string? CurrencyCodeCurrSymbol { get; set; } = "";
            public string? CurrencyCodeCurrencyID { get; set; } = "";
            public string? CurrencyCodeDocumentDesc { get; set; } = "";
            public decimal CustNum { get; set; } = 0;
            public string? CustomerBTName { get; set; } = "";
            public string? CustomerCustID { get; set; } = "";
            public bool CustomerInactive { get; set; } = false;
            public string? CustomerName { get; set; } = "";
            public bool CustomerRecievedStatus_c { get; set; } = false;
            public bool CustomerSendToFSA { get; set; } = false;
            public decimal DeclaredAmt { get; set; } = 0;
            public bool DeclaredIns { get; set; } = false;
            public decimal DeliveryConf { get; set; } = 1;
            public string? DeliveryType { get; set; } = "";
            public string? DeliveryTypeDescription { get; set; } = "";
            public string? DeviceUOM { get; set; } = "";
            public string? DigitalSignature { get; set; } = "";
            public string? DispatchReason { get; set; } = "";
            public bool DisplayInPrice { get; set; } = false;
            public bool DoPostUpdate { get; set; } = false;
            public decimal DocCopyNum { get; set; } = 0;
            public bool DocOnly { get; set; } = false;
            public decimal DocOrderAmt { get; set; } = 0;
            public decimal DocRounding { get; set; } = 0;
            public decimal DocTaxAmt { get; set; } = 0;
            public decimal DocTotalDiscount { get; set; } = 0;
            public decimal DocTotalSATax { get; set; } = 0;
            public decimal DocTotalTax { get; set; } = 0;
            public decimal DocTotalWHTax { get; set; } = 0;
            public decimal DocWithholdingTaxAmt { get; set; } = 0;
            public bool DocumentPrinted { get; set; } = false;
            public string? DriverID { get; set; } = "";
            public string? DspDigitalSignature { get; set; } = "";
            public bool EDIReady { get; set; } = false;
            public string? EDIShipToNum { get; set; } = "";
            public bool ERSOrder { get; set; } = false;
            public bool EnableAssignLegNum { get; set; } = false;
            public bool EnableIncotermLocation { get; set; } = false;
            public bool EnablePackageControl { get; set; } = true;
            public bool EnablePhantom { get; set; } = false;
            public bool EnableShipped { get; set; } = true;
            public bool EnableTax { get; set; } = false;
            public bool EnableTranDocType { get; set; } = false;
            public bool EnableVoidLegNum { get; set; } = true;
            public bool EnableWeight { get; set; } = true;
            public string? EntryPerson { get; set; } = "270723-001";
            public bool ExternalDeliveryNote { get; set; } = false;
            public string? ExternalID { get; set; } = "";
            public string? FFAddress1 { get; set; } = "";
            public string? FFAddress2 { get; set; } = "";
            public string? FFAddress3 { get; set; } = "";
            public string? FFCity { get; set; } = "";
            public string? FFCompName { get; set; } = "";
            public string? FFContact { get; set; } = "";
            public string? FFCountry { get; set; } = "";
            public decimal FFCountryNum { get; set; } = 0;
            public string? FFID { get; set; } = "";
            public string? FFPhoneNum { get; set; } = "";
            public string? FFState { get; set; } = "";
            public string? FFZip { get; set; } = "";
            public DateTime? FirstPrintDate { get; set; } = null;
            public string? FreightedShipViaCode { get; set; } = "";
            public string? FreightedShipViaCodeDescription { get; set; } = "";
            public string? FreightedShipViaCodeWebDesc { get; set; } = "";
            public bool FromMasterPack { get; set; } = false;
            public string? GroundType { get; set; } = "";
            public bool HasCartonLines { get; set; } = false;
            public bool HasLegNumCnfg { get; set; } = true;
            public bool HazardousShipment { get; set; } = false;
            public bool Hazmat { get; set; } = false;
            public bool ICReceived { get; set; } = false;
            public bool InPrice { get; set; } = false;
            public string? IncotermCode { get; set; } = "";
            public string? IncotermLocation { get; set; } = "";
            public string? IncotermsDescription { get; set; } = "";
            public bool IndividualPackIDs { get; set; } = false;
            public bool IntrntlShip { get; set; } = false;
            public bool Invoiced { get; set; } = false;
            public string? LabelComment { get; set; } = "";
            public bool LastCartonFlag { get; set; } = false;
            public string? LegalNumber { get; set; } = "";
            public string? LegalNumberMessage { get; set; } = "";
            public bool LetterOfInstr { get; set; } = false;

            public bool ManifestFlag { get; set; } = false;

            public decimal MasterpackPackNum { get; set; } = 0;
            public bool MultipleShippers { get; set; } = false;
            public bool NonStdPkg { get; set; } = false;
            public string? NotifyEMail { get; set; } = "";
            public bool NotifyFlag { get; set; } = false;
            public decimal OTSOrderNum { get; set; } = 0;
            public decimal OrderAmt { get; set; } = 0;
            public DateTime? OrderDate { get; set; } = null;
            public bool OrderHold { get; set; } = false;
            public decimal OrderNum { get; set; } = 0;
            public string? OurBank { get; set; } = "";
            public string? OurBankBankName { get; set; } = "";
            public string? OurBankDescription { get; set; } = "";
            public string? OurSupplierCode { get; set; } = "";
            public bool PBHoldNoInv { get; set; } = false;
            public string? PCID { get; set; } = "";
            public decimal PackNum { get; set; } = 0;
            public string? PayAccount { get; set; } = "";
            public string? PayBTAddress1 { get; set; } = "";
            public string? PayBTAddress2 { get; set; } = "";
            public string? PayBTAddress3 { get; set; } = "";
            public string? PayBTCity { get; set; } = "";
            public string? PayBTCountry { get; set; } = "";
            public string? PayBTPhone { get; set; } = "";
            public string? PayBTState { get; set; } = "";
            public string? PayBTZip { get; set; } = "";
            public string? PayFlag { get; set; } = "";
            public bool PhantomCasesExist { get; set; } = false;
            public bool PhantomPack { get; set; } = false;

            public string? Plant { get; set; } = "MfgSys";
            public string? PostUpdMessage { get; set; } = "";
            public string? ProjectCode_c { get; set; } = "";

            public string? RateGrpCode { get; set; } = "";
            public bool ReadyToInvoice { get; set; } = false;
            public bool ReadyToInvoiceChanged { get; set; } = false;
            public string? RefNotes { get; set; } = "";
            public decimal ReplicatedFrom { get; set; } = 0;
            public string? ReplicatedStat { get; set; } = "";
            public bool ResDelivery { get; set; } = false;
            public decimal Rounding { get; set; } = 0;
            public string? RowMod { get; set; } = "A";
            public decimal Rpt1OrderAmt { get; set; } = 0;

            public bool SatDelivery { get; set; } = false;
            public bool SatPickup { get; set; } = false;
            public string? ScheduleNumber { get; set; } = "Schedule Number";
            public bool SendShipment { get; set; } = false;
            public bool ServAOD { get; set; } = false;
            public bool ServAlert { get; set; } = false;
            public string? ServAuthNum { get; set; } = "";
            public DateTime? ServDeliveryDate { get; set; } = null;
            public bool ServHomeDel { get; set; } = false;
            public string? ServInstruct { get; set; } = "";
            public bool ServPOD { get; set; } = false;
            public string? ServPhone { get; set; } = "";
            public string? ServRef1 { get; set; } = "";
            public string? ServRef2 { get; set; } = "";
            public string? ServRef3 { get; set; } = "";
            public string? ServRef4 { get; set; } = "";
            public string? ServRef5 { get; set; } = "";
            public bool ServRelease { get; set; } = false;
            public bool ServSatDelivery { get; set; } = false;
            public bool ServSatPickup { get; set; } = false;
            public bool ServSignature { get; set; } = false;
            public string? ShipComment { get; set; } = "";
            public DateTime? ShipDate { get; set; } = null;
            public bool ShipExprtDeclartn { get; set; } = false;
            public string? ShipGroup { get; set; } = "";
            public string? ShipLog { get; set; } = "";
            public bool ShipOvers { get; set; } = false;
            public string? ShipPerson { get; set; } = "270723-001";
            public string? ShipStatus { get; set; } = "OPEN";
            public string? ShipToAddressFormatted { get; set; } = "";
            public string? ShipToCustBTName { get; set; } = "";
            public string? ShipToCustCustID { get; set; } = "";
            public bool ShipToCustInactive { get; set; } = false;
            public string? ShipToCustName { get; set; } = "";
            public decimal ShipToCustNum { get; set; } = 0;
            public string? ShipToNum { get; set; } = "";
            public bool ShipToNumInactive { get; set; } = false;
            public string? ShipToNumName { get; set; } = "";
            public string? ShipViaCode { get; set; } = "";
            public string? ShipViaCodeDescription { get; set; } = "";
            public string? ShipViaCodeWebDesc { get; set; } = "";
            public string? SignedBy { get; set; } = "";
            public DateTime? SignedOn { get; set; } = null;
            public string? SlipStatus { get; set; } = "";
            public string? SoldToAddressFormatted { get; set; } = "";
            public bool StageShipped { get; set; } = false;
            public bool StagingReq { get; set; } = false;
            public string? StatusChgMessage { get; set; } = "";
            public decimal SysRevID { get; set; } = 0;
            public Guid SysRowID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
            public decimal TaxAmt { get; set; } = 0;
            public DateTime? TaxCalcDate { get; set; } = null;
            public bool TaxCalculated { get; set; } = false;
            public string? TaxRegionCode { get; set; } = "";
            public string? TaxRegionDescription { get; set; } = "";

            public string? TrackingNumber { get; set; } = "";
            public string? TranDocTypeDescription { get; set; } = "Customer Shipment";
            public string? TranDocTypeID { get; set; } = "CSSHIP";
            public string? UPSQVMemo { get; set; } = "";
            public string? UPSQVShipFromName { get; set; } = "";
            public bool UPSQuantumView { get; set; } = false;
            public bool UseOTS { get; set; } = false;
            public string? VehicleLicensePlate { get; set; } = "";
            public string? VehicleType { get; set; } = "";
            public decimal VehicleYear { get; set; } = 0;
            public bool VerbalConf { get; set; } = false;
            public bool Voided { get; set; } = false;
            public bool WIPackSlipCreated { get; set; } = false;
            public string? WayBillNbr { get; set; } = "";
            public decimal Weight { get; set; } = 0;
            public string? WeightUOM { get; set; } = "KG";
            public string? XRefPackNum { get; set; } = "";
            public int OrderNum_C { get; set; } = 0;
        }

        public class Ds
        {
            public List<ShipHead> ShipHead { get; set; }
            public List<LegalNumGenOpts> LegalNumGenOpts { get; set; }
            public List<ShipDtl> ShipDtl { get; set; }

        }
        public class Parameters
        {
            public Ds Ds { get; set; }
        }

        public class ApiResponse
        {
            public Parameters Parameters { get; set; }
        }

        public class OrderInfo
        {
            public decimal PackNum { get; set; } = 0;
            public string? TranDocTypeDescription { get; set; } = "Customer Shipment ";
            public string? TranDocTypeID { get; set; } = "CSSHIP";
            public string? WeightUOM { get; set; }
            public string? ShipStatus { get; set; } = "OPEN";
            public string? ShipPerson { get; set; }
            public string? ScheduleNumber { get; set; } = "Schedule Number";
        }
        public class OrderNumInfo
        {
            public int PackNum { get; set; } = 0;
            public decimal OrderNum { get; set; } = 0;
            public int OrderLine { get; set; } = 0;
            public int OrderRel { get; set; } = 0;
            public string PartNum { get; set; } = "";
            public string PartDesc { get; set; } = "";
            public decimal DisplayInvQty { get; set; } = 0;
            public string WarehouseCode { get; set; } = "05-03-01";
            public string WarehouseCodeDescription { get; set; } = "Warehouse FG";
            public string BinNum { get; set; } = "GENERAL";
            public bool ShipCmpl { get; set; } = true;
            public string? LotNum { get; set; } = "";
            public string? PONum { get; set; } = "";
            public int PackLine { get; set; } = 0;
            public string RowMod { get; set; } = "";
            public string nik { get; set; }
            
        }

        public class PackNumLine
        {
            public int PackNum { get; set; } = 0;
            public int PackLine { get; set; } = 0;
            public string nik { get; set; }
            
        }
        public class OrderNumLine
        {
            public int PackNum { get; set; } = 0;
            public int PackLine { get; set; } = 0;
            public decimal OrderNum { get; set; } = 0;
            public int OrderLine { get; set; } = 0;
            public int OrderRel { get; set; } = 0;
            public string PartNum { get; set; } = "";
            public string PartDesc { get; set; } = "";
            public decimal DisplayInvQty { get; set; } = 0;
            public string WarehouseCode { get; set; } = "05-03-01";
            public string WarehouseCodeDescription { get; set; } = "Warehouse FG";
            public string BinNum { get; set; } = "GENERAL";
            public bool ShipCmpl { get; set; } = true;
            public string? LotNum { get; set; } = "";
            public string? PONum { get; set; } = "";
            public string RowMod { get; set; } = "";

            public string nik { get; set; }
            
        }

        public class ReadyPrint
        {
            public int PackNum { get; set; } = 0;
            public bool ReadyToPrint_c { get; set; } = false;
            public string nik { get; set; }
            

        }

        public class LegalNumGenOpts
        {
            public string? Company { get; set; }
            public string? LegalNumberID { get; set; } = "CSSHIP";
            public decimal TransYear { get; set; } = DateTime.Now.Year;
            public string? TransYearSuffix { get; set; }
            public string? DspTransYear { get; set; } = "2024";
            public bool ShowDspTransYear { get; set; }
            public string? Prefix { get; set; } = "SAI-CSH";
            public string? PrefixList { get; set; }
            public string? NumberSuffix { get; set; }
            public bool EnablePrefix { get; set; }
            public bool EnableSuffix { get; set; }
            public string? NumberOption { get; set; } = "Manual";
            public DateTime? DocumentDate { get; set; }
            public Guid? SysRowID { get; set; }
            public string? GenerationType { get; set; }
            public string? Description { get; set; } = "Customer Shipment";
            public decimal TransPeriod { get; set; } = DateTime.Now.Month;
            public string? PeriodPrefix { get; set; }
            public bool ShowTransPeriod { get; set; } = false;
            public string? LegalNumber { get; set; }
            public string? TranDocTypeID { get; set; }
            public string? TranDocTypeID2 { get; set; }
            public string? GenerationOption { get; set; }
            public string? RowMod { get; set; } = "A";
        }
        public class Var
        {
            public string? packNum { get; set; }
            public string? orderNum { get; set; }
           
        }
        public class OrderDetails
        {
            public string? BinNum { get; set; } = "GENERAL";
            public string? BinNumDescription { get; set; }
            public string? BinType { get; set; } = "Std";
            public int BitFlag { get; set; } = 0;
            public decimal ChangeTime { get; set; }
            public string? ChangedBy { get; set; }
            public string? Company { get; set; } = "SAI";
            public int CustNum { get; set; }
            public string? CustNumCustID { get; set; }
            public string? CustNumName { get; set; }
            public int DisplayInvQty { get; set; }
            public string? IUM { get; set; } = "PCS";
            public string? InventoryShipUOM { get; set; } = "PCS";
            public string? JobShipUOM { get; set; } = "PCS";
            public string? LineDesc { get; set; }
            public string? LineType { get; set; } = "PART";
            public string? LotNum { get; set; } = "A";
            public int OrderLine { get; set; }
            public string? OrderLineLineDesc { get; set; }  //PartNumber
            public string? OrderLineProdCode { get; set; }
            public int OrderNum { get; set; }
            public string? OrderNumCurrencyCode { get; set; } = "IDR";
            public string? OrderNumPSCurrCode { get; set; } = "IDR";
            public int OrderRelNum { get; set; }
            public int OrderRelOurReqQty { get; set; } = 0;
            public int OurInventoryShipQty { get; set; }
            public string? OurJobShipIUM { get; set; } = "PCS";
            public int OurRemainQty { get; set; }
            public string? OurRemainUM { get; set; } = "PCS";
            public int OurReqQty { get; set; }
            public string? OurReqUM { get; set; } = "PCS";
            public int OurShippedQty { get; set; }
            public string? OurShippedUM { get; set; } = "PCS";
            public int OurStockShippedQty { get; set; }
            public string? PONum { get; set; }
            public int PackLine { get; set; } = 0;
            public int PackNum { get; set; }
            public string? PackNumShipStatus { get; set; } = "OPEN";

            public string? PartNum { get; set; }
            public string? PartNumAttrClassID { get; set; } = "";
            public bool PartNumFSAEquipment { get; set; } = false;
            public string? PartNumIUM { get; set; } = "PCS";
            public string? PartNumPartDescription { get; set; }
            public string? PartNumPricePerCode { get; set; } = "E";
            public string? PartNumSalesUM { get; set; } = "PCS";
            public int PartNumSellingFactor { get; set; } = 1;
            public bool PartNumTrackLots { get; set; } = true;
            public bool PartUseHTSDesc { get; set; }
            public int PickedAutoAllocatedQty { get; set; }
            public string? Plant { get; set; } = "MfgSys";
            public string? PlantName { get; set; } = "Main Site";
            public bool ReadyToInvoice { get; set; } = false;
            public DateTime? RequestDate { get; set; }
            public string? RevisionNum { get; set; }
            public string RowMod { get; set; } = "A";
            public string? SalesUM { get; set; } = "PCS";
            public int SellingFactor { get; set; } = 1;
            public string? SellingFactorDirection { get; set; } = "D";
            public int SellingInventoryShipQty { get; set; } = 0;
            public string? SellingRemainUM { get; set; } = "PCS";
            public int SellingReqQty { get; set; }
            public string? SellingReqUM { get; set; } = "PCS";
            public int SellingShipmentQty { get; set; }
            public string? SellingShipmentUM { get; set; } = "PCS";
            public int SellingShippedQty { get; set; }
            public string? SellingShippedUM { get; set; } = "PCS";
            public bool ShipCmpl { get; set; } = true;
            public bool StockPart { get; set; } = true;
            public Guid SysRowID { get; set; }
            public string? WUM { get; set; } = "PCS";
            public string? WarehouseCode { get; set; }
            public string? WarehouseCodeDescription { get; set; }
        }

        public class ShipDtl
        {
            public string AttributeSetDescription { get; set; } = "";
            public int AttributeSetID { get; set; } = 0;
            public string AttributeSetShortDescription { get; set; } = "";
            public string BinNum { get; set; } = "";
            public string BinNumDescription { get; set; } = "";
            public string BinType { get; set; } = "";
            public int BitFlag { get; set; } = 0;
            public bool BuyToOrder { get; set; } = false;
            public string CNDeclarationBill { get; set; } = "";
            public int CNDeclarationBillLine { get; set; } = 0;
            public DateTime? ChangeDate { get; set; } = null;
            public DateTime? ChangeDateTime { get; set; } = null;
            public int ChangeTime { get; set; } = 0;
            public string ChangedBy { get; set; } = "";
            public string Company { get; set; } = "SAI";
            public string ComplianceMsg { get; set; } = "";
            public string ContractCode { get; set; } = "";
            public string ContractCodeContractDescription { get; set; } = "";
            public int ContractNum { get; set; } = 0;
            public string CurrencyCode { get; set; } = "";
            public string CustContainerPartNum { get; set; } = "";
            public int CustNum { get; set; } = 0;
            public string CustNumBTName { get; set; } = "";
            public string CustNumCustID { get; set; } = "";
            public string CustNumName { get; set; } = "";
            public bool CustNumSendToFSA { get; set; } = false;
            public string DUM { get; set; } = "";
            public string DimCode { get; set; } = "";
            public string DimCodeList { get; set; } = "";
            public decimal DimConvFactor { get; set; } = 0;
            public string DimensionDimCodeDescription { get; set; } = "";
            public decimal Discount { get; set; } = 0;
            public decimal DiscountPercent { get; set; } = 0;
            public decimal DispInventoryNumberOfPieces { get; set; } = 0;
            public decimal DispJobNumberOfPieces { get; set; } = 0;
            public decimal DisplayInvQty { get; set; } = 0;
            public decimal DocDiscount { get; set; } = 0;
            public decimal DocExtPrice { get; set; } = 0;
            public decimal DocInDiscount { get; set; } = 0;
            public decimal DocInExtPrice { get; set; } = 0;
            public decimal DocInUnitPrice { get; set; } = 0;
            public decimal DocLineTax { get; set; } = 0;
            public decimal DocUnitPrice { get; set; } = 0;
            public string DockingStation { get; set; } = "";
            public bool DropShip { get; set; } = false;
            public bool DtlError { get; set; } = false;
            public DateTime? EffectiveDate { get; set; } = null;
            public bool EnableAttributeSetSearch { get; set; } = false;
            public bool EnableInvIDBtn { get; set; } = false;
            public bool EnableInvSerialBtn { get; set; } = false;
            public bool EnableJobFields { get; set; } = false;
            public bool EnableKitIDBtn { get; set; } = false;
            public bool EnableMfgIDBtn { get; set; } = false;
            public bool EnableMfgSerialBtn { get; set; } = false;
            public bool EnableOBInvSerialBtn { get; set; } = false;
            public bool EnableOBMfgSerialBtn { get; set; } = false;
            public bool EnablePOSerialBtn { get; set; } = false;
            public bool EnablePackageControl { get; set; } = false;
            public string ExtJobNum { get; set; } = "";
            public decimal ExtPrice { get; set; } = 0;

            public bool GetLocIDNum { get; set; } = false;
            public string HeaderShipComment { get; set; } = "";
            public string IUM { get; set; } = "PCS";
            public decimal InDiscount { get; set; } = 0;
            public decimal InExtPrice { get; set; } = 0;
            public bool InPrice { get; set; } = false;
            public decimal InUnitPrice { get; set; } = 0;
            public string InvLegalNumber { get; set; } = "";
            public decimal InventoryNumberOfPieces { get; set; } = 0;
            public string InventoryShipUOM { get; set; } = "";
            public string InvoiceComment { get; set; } = "";
            public int InvoiceNum { get; set; } = 0;
            public bool Invoiced { get; set; } = false;
            public string JobLotNum { get; set; } = "";
            public decimal JobNotAllocatedQty { get; set; } = 0;
            public string JobNum { get; set; } = "";
            public string JobNumPartDescription { get; set; } = "";
            public string JobNum_c { get; set; } = "";
            public decimal JobNumberOfPieces { get; set; } = 0;
            public decimal JobPickedAutoAllocatedQty { get; set; } = 0;
            public string JobShipUOM { get; set; } = "";

            public bool LabCovered { get; set; } = false;
            public string LabelType { get; set; } = "";
            public decimal LaborDuration { get; set; } = 0;
            public DateTime? LaborExpiration { get; set; } = null;
            public string LaborMod { get; set; } = "";
            public DateTime? LastExpiration { get; set; } = null;
            public string LegalNumber { get; set; } = "";
            public decimal LineContentValue { get; set; } = 0;
            public string LineDesc { get; set; } = "";
            public decimal LineTax { get; set; } = 0;
            public string LineType { get; set; } = "PART";
            public string LinkMsg { get; set; } = "";
            public string LotNum { get; set; } = "";
            public string LotPartLotDescription { get; set; } = "";

            public string MaterialMod { get; set; } = "";
            public bool MiscCovered { get; set; } = false;
            public decimal MiscDuration { get; set; } = 0;
            public DateTime? MiscExpiration { get; set; } = null;
            public string MiscMod { get; set; } = "";
            public decimal NotAllocatedQty { get; set; } = 0;
            public bool NotCompliant { get; set; } = false;

            public bool Onsite { get; set; } = false;
            public string OrdRef_c { get; set; } = "";
            public bool OrderHold { get; set; } = false;
            public int OrderLine { get; set; } = 0;
            public string OrderLineLineDesc { get; set; } = "";
            public string OrderLineProdCode { get; set; } = "";
            public int OrderNum { get; set; } = 0;
            public string OrderNumCardMemberName { get; set; } = "";
            public string OrderNumCurrencyCode { get; set; } = "";
            public string OrderNumPSCurrCode { get; set; } = "";
            public int OrderRelNum { get; set; } = 0;
            public decimal OrderRelOurReqQty { get; set; } = 0;
            public decimal OurInventoryShipQty { get; set; } = 0;
            public string OurJobShipIUM { get; set; } = "PCS";
            public decimal OurJobShipQty { get; set; } = 0;
            public decimal OurJobShippedQty { get; set; } = 0;
            public decimal OurRemainQty { get; set; } = 0;
            public string OurRemainUM { get; set; } = "PCS";
            public decimal OurReqQty { get; set; } = 0;
            public string OurReqUM { get; set; } = "PCS";
            public decimal OurShippedQty { get; set; } = 0;
            public string OurShippedUM { get; set; } = "PCS";
            public decimal OurStockShippedQty { get; set; } = 0;
            public string PCID { get; set; } = "";
            public string PCID_c { get; set; } = "";
            public string PONum { get; set; } = "";
            public int PackLine { get; set; } = 0;
            public int PackNum { get; set; } = 0;
            public string PackNumShipStatus { get; set; } = "";
            public bool PackNumUseOTS { get; set; } = false;
            public string PackSlip { get; set; } = "";
            public int Packages { get; set; } = 1;
            public string PartAESExp { get; set; } = "";
            public string PartCompany { get; set; } = "";
            public string PartECNNumber { get; set; } = "";
            public string PartExpLicNumber { get; set; } = "";
            public string PartExpLicType { get; set; } = "";
            public string PartHTS { get; set; } = "";
            public string PartHazClass { get; set; } = "";
            public string PartHazGvrnmtID { get; set; } = "";
            public bool PartHazItem { get; set; } = false;
            public string PartHazPackInstr { get; set; } = "";
            public string PartHazSub { get; set; } = "";
            public string PartHazTechName { get; set; } = "";
            public string PartNAFTAOrigCountry { get; set; } = "";
            public string PartNAFTAPref { get; set; } = "";
            public string PartNAFTAProd { get; set; } = "";
            public string PartNum { get; set; } = "";
            public string PartNumAttrClassID { get; set; } = "";
            public bool PartNumFSAEquipment { get; set; } = false;
            public string PartNumIUM { get; set; } = "PCS";
            public string PartNumPartDescription { get; set; } = "";
            public string PartNumPricePerCode { get; set; } = "";
            public string PartNumSalesUM { get; set; } = "PCS";
            public decimal PartNumSellingFactor { get; set; } = 1;
            public bool PartNumSendToFSA { get; set; } = false;
            public bool PartNumTrackDimension { get; set; } = false;
            public bool PartNumTrackLots { get; set; } = false;
            public string PartPartNum { get; set; } = "";
            public decimal PickedAutoAllocatedQty { get; set; } = 0;
            public string PkgCodePartNum { get; set; } = "";
            public string Plant { get; set; } = "MfgSys";
            public string PlantName { get; set; } = "";
            public string PricePerCode { get; set; } = "";
            public string ProjectID { get; set; } = "";
            public string PurPoint { get; set; } = "";
            public bool ReadyToInvoice { get; set; } = false;
            public int RenewalNbr { get; set; } = 0;
            public DateTime? RequestDate { get; set; } = null;
            public string RevisionNum { get; set; } = "";
            public string RowMod { get; set; } = "A";

            public string SalesUM { get; set; } = "PCS";
            public decimal SelectedLocationIDQty { get; set; } = 0;
            public decimal SellingFactor { get; set; } = 1;
            public string SellingFactorDirection { get; set; } = "D";
            public decimal SellingInventoryShipQty { get; set; } = 0;
            public decimal SellingJobShipQty { get; set; } = 0;
            public decimal SellingRemainQty { get; set; } = 0;
            public string SellingRemainUM { get; set; } = "PCS";
            public decimal SellingReqQty { get; set; } = 0;
            public string SellingReqUM { get; set; } = "PCS";
            public decimal SellingShipmentQty { get; set; } = 0;
            public string SellingShipmentUM { get; set; } = "PCS";
            public decimal SellingShippedQty { get; set; } = 0;
            public string SellingShippedUM { get; set; } = "PCS";
            public bool ShipCmpl { get; set; } = false;
            public string ShipComment { get; set; } = "";
            public DateTime? ShipDate { get; set; } = null;
            public bool ShipOvers { get; set; } = false;
            public int ShipToCustNum { get; set; } = 0;
            public string ShipToNum { get; set; } = "";
            public string ShipToWarning { get; set; } = "";
            public string ShipViaCode { get; set; } = "";
            public int ShpConNum { get; set; } = 0;
            public bool StockPart { get; set; } = false;
            public int SysRevID { get; set; } = 0;
            public Guid SysRowID { get; set; } = Guid.Empty;
            public bool TMBilling { get; set; } = false;
            public decimal TotalNetWeight { get; set; } = 0;
            public bool TrackID { get; set; } = false;
            public bool TrackSerialNum { get; set; } = false;
            public decimal TranLocationIDQty { get; set; } = 0;
            public string UD_SysRevID { get; set; } = "";
            public decimal UnitPrice { get; set; } = 0;
            public bool UpdatedInventory { get; set; } = false;
            public bool UseOTMF { get; set; } = false;
            public bool UseShipDtlInfo { get; set; } = false;
            public int VendorNum { get; set; } = 0;
            public string WarrantyCodeWarrDescription { get; set; } = "";
            public string WIPWarehouseCode { get; set; } = "";
            public string WIPWarehouseCodeDescription { get; set; } = "";
            public string WIPBinNum { get; set; } = "";

            public string? WUM { get; set; } = "KG";
            public string WarehouseCode { get; set; } = "05-03-01";
            public string WarehouseCodeDescription { get; set; } = "Warehouse FG";

        }
        public class PartFromOrder
        {
            public int orderLine { get; set; }
            public int orderNum { get; set; }
            public string partNum { get; set; }
        }
        public class SetUpdateMaster
        {
            public string WarehouseCode { get; set; } = "05-03-01";
            public string WarehouseCodeDescription { get; set; } = "Warehouse FG";
            public string BinNum { get; set; } = "GENERAL";
            public int PackNum { get; set; }
            public int PackLine { get; set; }

            public int orderNum { get; set; }
            public int orderLine { get; set; }
            public int orderRelease { get; set; }

            public string PONum { get; set; }
            public string PartNum { get; set; }
            public string LineDesc { get; set; }

            public bool ShipCmpl { get; set; } = true;
            public string? RowMod { get; set; } = "A";

        }
        public class PrePartInfo
        {
            public string? orderLine { get; set; }
            public string? orderNum { get; set; }
            public string? partNum { get; set; }
        }

        public class UserEpicor
        {
            public string nik { get; set; }
            
        }
    }
}
