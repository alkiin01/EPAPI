namespace EPAPI.Repository.JobReceipt
{

    public class PartTran
    {
        public string? Company { get; set; }
        public DateTime? SysDate { get; set; }
        public int? SysTime { get; set; }
        public int? TranNum { get; set; }
        public string? PartNum { get; set; }
        public string? WareHouseCode { get; set; }
        public string? BinNum { get; set; }
        public string? TranClass { get; set; }
        public string? TranType { get; set; }
        public bool InventoryTrans { get; set; }
        public DateTime? TranDate { get; set; }
        public decimal? TranQty { get; set; }
        public string? UM { get; set; }
        public decimal? MtlUnitCost { get; set; }
        public decimal? LbrUnitCost { get; set; }
        public decimal? BurUnitCost { get; set; }
        public decimal? SubUnitCost { get; set; }
        public decimal? MtlBurUnitCost { get; set; }
        public decimal? ExtCost { get; set; }
        public string? CostMethod { get; set; }
        public string? JobNum { get; set; }
        public int? AssemblySeq { get; set; }
        public string? JobSeqType { get; set; }
        public int? JobSeq { get; set; }
        public string? PackType { get; set; }
        public int? PackNum { get; set; }
        public int? PackLine { get; set; }
        public int? PONum { get; set; }
        public int? POLine { get; set; }
        public int? PORelNum { get; set; }
        public string? WareHouse2 { get; set; }
        public string? BinNum2 { get; set; }
        public int? OrderNum { get; set; }
        public int? OrderLine { get; set; }
        public int? OrderRelNum { get; set; }
        public string? EntryPerson { get; set; }
        public string? TranReference { get; set; }
        public string? PartDescription { get; set; }
        public string? RevisionNum { get; set; }
        public int? VendorNum { get; set; }
        public string? PurPoint { get; set; }
        public decimal? POReceiptQty { get; set; }
        public decimal? POUnitCost { get; set; }
        public string? PackSlip { get; set; }
        public string? InvoiceNum { get; set; }
        public int? InvoiceLine { get; set; }
        public string? InvAdjSrc { get; set; }
        public string? InvAdjReason { get; set; }
        public string? LotNum { get; set; }
        public string? DimCode { get; set; }
        public string? DUM { get; set; }
        public decimal? DimConvFactor { get; set; }
        public string? LotNum2 { get; set; }
        public string? DimCode2 { get; set; }
        public string? DUM2 { get; set; }
        public decimal? DimConvFactor2 { get; set; }
        public bool GLTrans { get; set; }
        public bool PostedToGL { get; set; }
        public int? FiscalYear { get; set; }
        public int? FiscalPeriod { get; set; }
        public int? JournalNum { get; set; }
        public bool Costed { get; set; }
        public int? DMRNum { get; set; }
        public int? ActionNum { get; set; }
        public int? RMANum { get; set; }
        public bool COSPostingReqd { get; set; }
        public string? JournalCode { get; set; }
        public string? Plant { get; set; }
        public string? Plant2 { get; set; }
        public int? CallNum { get; set; }
        public int? CallLine { get; set; }
        public int? MatNum { get; set; }
        public string? JobNum2 { get; set; }
        public int? AssemblySeq2 { get; set; }
        public int? JobSeq2 { get; set; }
        public int? CustNum { get; set; }
        public int? RMALine { get; set; }
        public int? RMAReceipt { get; set; }
        public int? RMADisp { get; set; }
        public decimal? OtherDivValue { get; set; }
        public int? PlantTranNum { get; set; }
        public int? NonConfID { get; set; }
        public decimal? MtlMtlUnitCost { get; set; }
        public decimal? MtlLabUnitCost { get; set; }
        public decimal? MtlSubUnitCost { get; set; }
        public decimal? MtlBurdenUnitCost { get; set; }
        public string? RefType { get; set; }
        public string? RefCode { get; set; }
        public string? LegalNumber { get; set; }
        public decimal? BeginQty { get; set; }
        public decimal? AfterQty { get; set; }
        public decimal? BegBurUnitCost { get; set; }
        public decimal? BegLbrUnitCost { get; set; }
        public decimal? BegMtlBurUnitCost { get; set; }
        public decimal? BegMtlUnitCost { get; set; }
        public decimal? BegSubUnitCost { get; set; }
        public decimal? AfterBurUnitCost { get; set; }
        public decimal? AfterLbrUnitCost { get; set; }
        public decimal? AfterMtlBurUnitCost { get; set; }
        public decimal? AfterMtlUnitCost { get; set; }
        public decimal? AfterSubUnitCost { get; set; }
        public decimal? PlantCostValue { get; set; }
        public string? EmpID { get; set; }
        public int? ReconcileNum { get; set; }
        public string? CostID { get; set; }
        public DateTime? FIFODate { get; set; }
        public int? FIFOSeq { get; set; }
        public decimal? ActTranQty { get; set; }
        public string? ActTransUOM { get; set; }
        public string? InvtyUOM { get; set; }
        public string? InvtyUOM2 { get; set; }
        public string? FIFOAction { get; set; }
        public string? FiscalYearSuffix { get; set; }
        public string? FiscalCalendarID { get; set; }
        public string? Bintype { get; set; }
        public int? CCYear { get; set; }
        public int? CCMonth { get; set; }
        public int? CycleSeq { get; set; }
        public string? ABTUID { get; set; }
        public string? BaseCostMethod { get; set; }
        public int? RevertStatus { get; set; }
        public string? RevertID { get; set; }
        public string? DropShipPackSlip { get; set; }
        public string? VarTarget { get; set; }
        public int? FIFOSubSeq { get; set; }
        public decimal? AltMtlUnitCost { get; set; }
        public decimal? AltLbrUnitCost { get; set; }
        public decimal? AltBurUnitCost { get; set; }
        public decimal? AltSubUnitCost { get; set; }
        public decimal? AltMtlBurUnitCost { get; set; }
        public decimal? AltExtCost { get; set; }
        public decimal? AltMtlMtlUnitCost { get; set; }
        public decimal? AltMtlLabUnitCost { get; set; }
        public decimal? AltMtlSubUnitCost { get; set; }
        public decimal? AltMtlBurdenUnitCost { get; set; }
        public string? TranDocTypeID { get; set; }
        public int? PBInvNum { get; set; }
        public string? LoanFlag { get; set; }
        public string? AssetNum { get; set; }
        public int? AdditionNum { get; set; }
        public int? DisposalNum { get; set; }
        public bool ProjProcessed { get; set; }
        public DateTime? AsOfDate { get; set; }
        public int? AsOfSeq { get; set; }
        public int? MscNum { get; set; }
        public decimal? ODCUnitCost { get; set; }
        public int? SysRevID { get; set; }
        public Guid SysRowID { get; set; }
        public int? TranRefType { get; set; }
        public string? PCID { get; set; }
        public int? PCIDCollapseCounter { get; set; }
        public string? PCID2 { get; set; }
        public string? ContractID { get; set; }
        public bool LCFlag { get; set; }
        public decimal? ExtMtlCost { get; set; }
        public decimal? ExtLbrCost { get; set; }
        public decimal? ExtBurCost { get; set; }
        public decimal? ExtSubCost { get; set; }
        public decimal? ExtMtlBurCost { get; set; }
        public decimal? ExtMtlMtlCost { get; set; }
        public decimal? ExtMtlLabCost { get; set; }
        public decimal? ExtMtlSubCost { get; set; }
        public decimal? ExtMtlBurdenCost { get; set; }
        public string? MYImportNum { get; set; }
        public bool AutoReverse { get; set; }
        public int? RevTranNum { get; set; }
        public DateTime? RevSysDate { get; set; }
        public int? RevSysTime { get; set; }
        public decimal? ExtNonRecoverableCost { get; set; }
        public bool EpicorFSA { get; set; }
        public int? AttributeSetID { get; set; }
        public string? AttributeSetDescription { get; set; }
        public string? AttributeSetShortDescription { get; set; }
        public int? NumberOfPieces { get; set; }
        public string? WIPPCID { get; set; }
        public string? WIPPCID2 { get; set; }
        public string? CallCode { get; set; }
        public string? ContractCode { get; set; }
        public string? DIMDescription { get; set; }
        public bool DisableFieldPart { get; set; }
        public string? DispSysTime { get; set; }
        public string? DispUOM { get; set; }
        public string? DummyKeyField { get; set; }
        public bool EnableSerialNumbers { get; set; }
        public string? FSAAction { get; set; }
        public string? FSACallCode { get; set; }
        public string? FSAContractCode { get; set; }
        public int? FSAContractNum { get; set; }
        public string? FSAEmpID { get; set; }
        public int? FSAEquipmentInstallID { get; set; }
        public string? FSAEquipmentPartNum { get; set; }
        public int? FSAServiceOrderNum { get; set; }
        public int? FSAServiceOrderResourceNum { get; set; }
        public string? FSAWarrantyCode { get; set; }
        public bool FullPhysical { get; set; }
        public decimal? GLTranAmt { get; set; }
        public DateTime? GLTranDate { get; set; }
        public decimal? InvBurUnitCost { get; set; }
        public decimal? InvLbrUnitCost { get; set; }
        public decimal? InvMtlBurUnitCost { get; set; }
        public decimal? InvMtlUnitCost { get; set; }
        public decimal? InvSubUnitCost { get; set; }
        public bool IssuedComplete { get; set; }
        public decimal? JobBurUnitCost { get; set; }
        public decimal? JobLbrUnitCost { get; set; }
        public decimal? JobMtlBurUnitCost { get; set; }
        public decimal? JobMtlUnitCost { get; set; }
        public decimal? JobSubUnitCost { get; set; }
        public string? LegalNumberNumber { get; set; }
        public string? LegalNumberPrefix { get; set; }
        public string? LegalNumberPrefixList { get; set; }
        public string? LegalNumberReadOnlyFields { get; set; }
        public int? LegalNumberYear { get; set; }
        public decimal? MtlLbrUnitCost { get; set; }
        public Guid MtlQueueRowId { get; set; }
        public bool MultiEndParts { get; set; }
        public decimal? OnHandQty { get; set; }
        public bool OverRideCosts { get; set; }
        public string? PartDescriptionAsm { get; set; }
        public string? PartDescriptionJH { get; set; }
        public string? PartDescriptionMS { get; set; }
        public string? PartDescriptionSP { get; set; }
        public string? PartNumAsm { get; set; }
        public string? PartNumJH { get; set; }
        public string? PartNumMS { get; set; }
        public string? PartNumSP { get; set; }
        public decimal? QtyAvailToComplete { get; set; }
        public bool QtyBearing { get; set; }
        public decimal? QtyCompleted { get; set; }
        public string? RevisionNumRevDescription { get; set; }
        public string? RevisionNumRevShortDesc { get; set; }
        public decimal? SalvageQtyToDate { get; set; }
        public int? SerialNoTempTranID { get; set; }
        public decimal? ThisTranQty { get; set; }
        public decimal? TranAmount { get; set; }
        public string? TreeDisplay { get; set; }
        public string? WarrantyCode { get; set; }
        public int? ContractNum { get; set; }
        public string? CostMethodDisplay { get; set; }
        public decimal? DispNumberOfPieces { get; set; }
        public string? RevisionNumAsm { get; set; }
        public string? RevisionNumMS { get; set; }
        public string? RevisionNumSP { get; set; }
        public bool PlantConfCtrlEnablePackageControl { get; set; }
        public string? AttributeSetDescriptionMS { get; set; }
        public int? AttributeSetIDMS { get; set; }
        public string? AttributeSetShortDescriptionMS { get; set; }
        public int? BitFlag { get; set; }
        public string? AssemblySeqDescription { get; set; }
        public string? BinNumDescription { get; set; }
        public string? CallLineLineDesc { get; set; }
        public string? CustNumCustID { get; set; }
        public string? CustNumBTName { get; set; }
        public string? CustNumName { get; set; }
        public string? DimCodeDimCodeDescription { get; set; }
        public string? DMRNumPartDescription { get; set; }
        public string? FromBinDescription { get; set; }
        public string? FromPlantName { get; set; }
        public string? FromWareHouseDescription { get; set; }
        public string? InvoiceNumDescription { get; set; }
        public string? JobNumPartDescription { get; set; }
        public string? MatNumLineDesc { get; set; }
        public string? OrderLineLineDesc { get; set; }
        public string? OrderNumCardMemberName { get; set; }
        public string? OrderNumCurrencyCode { get; set; }
        public string? PackLineLineDesc { get; set; }
        public string? PackNumName { get; set; }
        public string? PartLotPartLotDescription { get; set; }
        public bool PartNumTrackInventoryByRevision { get; set; }
        public decimal? PartNumSellingFactor { get; set; }
        public bool PartNumTrackSerialNum { get; set; }
        public bool PartNumTrackDimension { get; set; }
        public bool PartNumTrackLots { get; set; }
        public string? PartNumIUM { get; set; }
        public string? PartNumPartDescription { get; set; }
        public string? PartNumSalesUM { get; set; }
        public string? PartNumPricePerCode { get; set; }
        public bool PartNumTrackInventoryAttributes { get; set; }
        public string? PartNumAttrClassID { get; set; }
        public string? PlantName { get; set; }
        public string? POLineVenPartNum { get; set; }
        public string? POLinePartNum { get; set; }
        public string? POLineLineDesc { get; set; }
        public string? RMALineLineDesc { get; set; }
        public string? VendorNumName { get; set; }
        public string? VendorNumAddress3 { get; set; }
        public string? VendorNumDefaultFOB { get; set; }
        public string? VendorNumCurrencyCode { get; set; }
        public string? VendorNumState { get; set; }
        public string? VendorNumAddress1 { get; set; }
        public string? VendorNumAddress2 { get; set; }
        public string? VendorNumVendorID { get; set; }
        public string? VendorNumCountry { get; set; }
        public string? VendorNumCity { get; set; }
        public string? VendorNumZIP { get; set; }
        public string? VendorNumTermsCode { get; set; }
        public string? VendorPPNumAddress1 { get; set; }
        public string? VendorPPNumAddress2 { get; set; }
        public int? VendorPPNumPrimPCon { get; set; }
        public string? VendorPPNumAddress3 { get; set; }
        public string? VendorPPNumCountry { get; set; }
        public string? VendorPPNumState { get; set; }
        public string? VendorPPNumZip { get; set; }
        public string? VendorPPNumCity { get; set; }
        public string? VendorPPNumName { get; set; }
        public string? WarehouseDescription { get; set; }
        public string? RowMod { get; set; }
        public string? CustomerID_c { get; set; }
        public bool FlagPCID_c { get; set; }
        public string? VendorID_c { get; set; }
    }

    public class JobResponse
    {
        public Parameters parameters { get; set; }

    }
    public class JobReceipt
    {
        public string? JobNum { get; set; }
       public int? Qty { get; set; }
        public string? LotNum { get; set; }
        public string? WarehouseCode { get; set; }
        public string? WarehouseDescription { get; set; }
        public string? BinNum { get; set; }
        public string? nik { get; set; }
        public string? password { get; set; }

    }
    public class LegalNumGenOpts
    {
        public string Company { get; set; } = "SAI";
        public string LegalNumberID { get; set; } = "WIPSTOCK";
        public int TransYear { get; set; } = DateTime.Now.Year;  // Default tahun sekarang
        public string TransYearSuffix { get; set; } = "";
        public string DspTransYear { get; set; } = DateTime.Now.Year.ToString(); // Default tahun sekarang
        public bool ShowDspTransYear { get; set; } = false;
        public string Prefix { get; set; } = "WIP-STOCK";
        public string PrefixList { get; set; } = "";
        public string NumberSuffix { get; set; } = "";
        public bool EnablePrefix { get; set; } = false;
        public bool EnableSuffix { get; set; } = false;
        public string NumberOption { get; set; } = "Manual";
        public DateTime DocumentDate { get; set; } = DateTime.Now;  // Default tanggal sekarang
        public string GenerationType { get; set; } = "system";
        public string Description { get; set; } = "WIP to Stock";
        public int TransPeriod { get; set; } = DateTime.Now.Month;  // Default bulan sekarang
        public string PeriodPrefix { get; set; } = "";
        public bool ShowTransPeriod { get; set; } = false;
        public string LegalNumber { get; set; } = "";
        public string TranDocTypeID { get; set; } = "WIPStock";
        public string TranDocTypeID2 { get; set; } = "WIP-STOCK";
        public string GenerationOption { get; set; } = "Save";
        public Guid SysRowID { get; set; } = Guid.Empty;  // Default Guid kosong
        public string RowMod { get; set; } = "A";

        
    }

}
