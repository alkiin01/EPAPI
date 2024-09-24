namespace EPAPI.Repository
{
    public class TimeEntries
    {
        public string EmployeeNum { get; set; }
        public DateTime StartDate { get; set; }
        //public int Shift { get; set; }
        public string nik {  get; set; }
        public string password {  get; set; }

    }
    public class LaborHedDs
    {
        public DateTime WorkDate { get; set; }
        public int? LaborHedSeq { get; set; } = 0;
        public int? Shift { get; set; }
        public double? PayHours { get; set; }
        public DateTime? ActualClockinDate { get; set; }
        public DateTime? ClockInDate { get; set; }
        public string? ActualClockInTime { get; set; }
        public string? ClockInTime { get; set; }
        public string? ActualClockOutTime { get; set; }
        public string? ClockOutTime { get; set; }
        public string? ActLunchOutTime { get; set; }
        public string? LunchOutTime { get; set; }
        public string? ActLunchInTime { get; set; }
        public string? LunchInTime { get; set; }
        public string nik { get; set; }
        public string password { get; set; }

    }
    public class LaborResponse
    {
        public Parameters parameters { get; set; }
    }
    public class LaborHed
    {
        public string? Company { get; set; } = "SAI";
        public string? EmployeeNum { get; set; } = "SAI00075";
        public int? LaborHedSeq { get; set; } = 0;
        public DateTime? PayrollDate { get; set; } = DateTime.Parse("2024-08-20T00:00:00");
        public int? Shift { get; set; } = 4;
        public DateTime? ClockInDate { get; set; } = DateTime.Parse("2024-08-20T00:00:00");
        public double? ClockInTime { get; set; } = 7.50;
        public string? DspClockInTime { get; set; } = "07:30";
        public double? ActualClockInTime { get; set; } = 7.50;
        public DateTime? ActualClockinDate { get; set; } = DateTime.Parse("2024-08-20T00:00:00");
        public string? LunchStatus { get; set; } = "N";
        public double? ActLunchOutTime { get; set; }
        public double? LunchOutTime { get; set; }
        public double? ActLunchInTime { get; set; }
        public double? LunchInTime { get; set; }
        public double? ClockOutTime { get; set; }
        public string? DspClockOutTime { get; set; } = "16:30";
        public double? ActualClockOutTime { get; set; }
        public double? PayHours { get; set; } = 8.25;
        public bool? FeedPayroll { get; set; } = true;
        public bool? TransferredToPayroll { get; set; } = false;
        public bool? LaborCollection { get; set; } = false;
        public string? TranSet { get; set; } = "";
        public bool? ActiveTrans { get; set; } = false;
        public string? ChkLink { get; set; } = "";
        public string? BatchTotalHrsDisp { get; set; } = "";
        public string? BatchHrsRemainDisp { get; set; } = "";
        public string? BatchHrsRemainPctDisp { get; set; } = "";
        public string? BatchSplitHrsMethod { get; set; } = "";
        public bool? BatchAssignTo { get; set; } = false;
        public bool? BatchComplete { get; set; } = false;
        public double? BatchStartHrs { get; set; } = null;
        public double? BatchEndHrs { get; set; } = null;
        public double? BatchTotalHrs { get; set; } = 0.0;
        public double? BatchHrsRemain { get; set; } = 0.0;
        public double? BatchHrsRemainPct { get; set; } = 0.0;
        public int? SysRevID { get; set; } = 0;
        public Guid? SysRowID { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public bool? Imported { get; set; } = false;
        public DateTime? ImportDate { get; set; } = null;
        public bool? BatchMode { get; set; } = false;
        public string? HCMPayHoursCalcType { get; set; } = "";
        public int? EmpBasicShift { get; set; } = 4;
        public string? EmpBasicSupervisorID { get; set; } = "";
        public bool? GetNewNoHdr { get; set; } = false;
        public double? HCMTotPayHours { get; set; } = 0.0;
        public string? ImagePath { get; set; } = "empphoto/SAI/.bmp";
        public bool? LunchBreak { get; set; } = true;
        public bool? MES { get; set; } = false;
        public string? PayrollValuesForHCM { get; set; } = "NON";
        public bool? TimeDisableDelete { get; set; } = false;
        public bool? TimeDisableUpdate { get; set; } = false;
        public double? TotBurHrs { get; set; } = 0.0;
        public double? TotLbrHrs { get; set; } = 0.0;
        public bool? WipPosted { get; set; } = false;
        public double? DspPayHours { get; set; } = 8.25;
        public bool? FullyPosted { get; set; } = false;
        public bool? PartiallyPosted { get; set; } = false;
        public bool? HCMExistsWithStatus { get; set; } = false;
        public DateTime? PayrollDateNav { get; set; } = DateTime.Parse("2024-08-20T00:00:00");
        public int? BitFlag { get; set; } = 0;
        public string? EmployeeNumFirstName { get; set; } = "";
        public string? EmployeeNumName { get; set; } = "";
        public string? EmployeeNumLastName { get; set; } = "";
        public string? HCMStatusStatus { get; set; } = "";
        public bool? PRSystHCMEnabled { get; set; } = false;
        public string? ShiftDescription { get; set; } = "NON-SHIFT";
        public string? RowMod { get; set; } = "A";
    }
    public class LaborDtl
    {
        public string? ABTUID { get; set; } = "";
        public int? ActID { get; set; } = 0;
        public string? ActiveTaskID { get; set; } = "";
        public bool? ActiveTrans { get; set; } = false;
        public bool? AddedOper { get; set; } = false;
        public bool? AllowDirLbr { get; set; } = true;
        public bool? AppliedToSchedule { get; set; } = false;
        public DateTime? AppointmentEnd { get; set; } = DateTime.Parse("2024-08-20T16:30:00");
        public DateTime? AppointmentStart { get; set; } = DateTime.Parse("2024-08-20T07:30:00");
        public string? AppointmentTitle { get; set; } = "Labor Hours: 8.25 | Job:  | Opr: 0 | Status: Entered | Clock In Date: 8/20/2024";
        public string? ApprovalPhaseDesc { get; set; } = "";
        public string? ApprovalPhaseID { get; set; } = "";
        public string? ApprovalProjectDesc { get; set; } = "";
        public string? ApprovalProjectID { get; set; } = "";
        public bool? ApprovalRequired { get; set; } = false;
        public string? ApprovedBy { get; set; } = "";
        public DateTime? ApprovedDate { get; set; } = null;
        public bool? ApprvrHasOpenTask { get; set; } = false;
        public DateTime? AsOfDate { get; set; } = null;
        public int? AsOfSeq { get; set; } = 0;
        public int? AssemblySeq { get; set; } = 0;
        public bool? AssignToBatch { get; set; } = false;
        public string? AttrClassID { get; set; } = "";
        public bool? BFLaborReq { get; set; } = false;
        public string? BaseCurrCodeDesc { get; set; } = "";
        public bool? BatchComplete { get; set; } = false;
        public double? BatchLaborHrs { get; set; } = 0;
        public bool? BatchMode { get; set; } = false;
        public double? BatchPctOfTotHrs { get; set; } = 0;
        public bool? BatchPrint { get; set; } = false;
        public double? BatchQty { get; set; } = 0;
        public bool? BatchRequestMove { get; set; } = false;
        public double? BatchTotalExpectedHrs { get; set; } = 0;
        public string? BatchWasSaved { get; set; } = "";
        public double? BillServiceRate { get; set; } = 0;
        public int? BitFlag { get; set; } = 0;
        public double? BurdenCost { get; set; } = 0;
        public double? BurdenHrs { get; set; } = 8.25;
        public double? BurdenRate { get; set; } = 0;
        public string? CallCode { get; set; } = "";
        public int? CallLine { get; set; } = 0;
        public int? CallNum { get; set; } = 0;
        public string? CapabilityDescription { get; set; } = "";
        public string? CapabilityID { get; set; } = "";
        public DateTime? ChangeDate { get; set; } = null;
        public int? ChangeTime { get; set; } = 0;
        public string? ChangedBy { get; set; } = "";
        public double? ChargeRate { get; set; } = 0;
        public string? ChargeRateSRC { get; set; } = "";
        public string? ChgRateCurrDesc { get; set; } = "";
        public string? ClaimRef { get; set; } = "";
        public DateTime? ClockInDate { get; set; } = DateTime.Parse("2024-08-20T00:00:00");
        public int? ClockInMInute { get; set; } = 37241730;
        public int? ClockOutMinute { get; set; } = 37242270;
        public double? ClockOutTime { get; set; } = 16.5;
        public double? ClockinTime { get; set; } = 7.5;
        public string? Company { get; set; } = "SAI";
        public bool? Complete { get; set; } = false;
        public bool? CompleteFlag { get; set; } = false;
        public string? ContractCode { get; set; } = "";
        public int? ContractNum { get; set; } = 0;
        public DateTime? CreateDate { get; set; } = null;
        public int? CreateTime { get; set; } = 0;
        public string? CreatedBy { get; set; } = "";
        public bool? CreatedViaTEWeekView { get; set; } = false;
        public string? CurrentWFStageID { get; set; } = "";
        public bool? DisLaborType { get; set; } = false;
        public bool? DisPrjRoleCd { get; set; } = true;
        public bool? DisTimeTypCd { get; set; } = true;
        public bool? DisableRecallAndDelete { get; set; } = false;
        public string? DiscrepAttributeSetDescription { get; set; } = "";
        public int? DiscrepAttributeSetID { get; set; } = 0;
        public string? DiscrepAttributeSetShortDescription { get; set; } = "";
        public double? DiscrepQty { get; set; } = 0;
        public string? DiscrepRevision { get; set; } = "";
        public string? DiscrepUOM { get; set; } = "";
        public string? DiscrpRsnCode { get; set; } = "";
        public string? DiscrpRsnDescription { get; set; } = "";
        public string? DisplayJob { get; set; } = "";
        public double? DocPBChargeAmt { get; set; } = 0;
        public double? DocPBChargeRate { get; set; } = 0;
        public bool? Downtime { get; set; } = false;
        public double? DowntimeTotal { get; set; } = 0;
        public string? DspChangeTime { get; set; } = "00:00";
        public string? DspClockInTime { get; set; } = "07:30";
        public string? DspClockOutTime { get; set; } = "16:30";
        public string? DspCreateTime { get; set; } = "00:00";
        public string? DspTotHours { get; set; } = "";
        public DateTime? DtClockIn { get; set; } = null;
        public DateTime? DtClockOut { get; set; } = null;
        public int? DtailID { get; set; } = 0;
        public double? EarnedHrs { get; set; } = 0;
        public double? EfficiencyPercentage { get; set; } = 0;
        public string? EmpBasicFirstName { get; set; } = "AL";
        public string? EmpBasicLastName { get; set; } = "KINDI";
        public string? EmpBasicName { get; set; } = "AL KINDI";
        public string? EmployeeName { get; set; } = "";
        public string? EmployeeNum { get; set; } = "SAI00075";
        public bool? EnableComplete { get; set; } = true;
        public bool? EnableCopy { get; set; } = true;
        public bool? EnableDiscrepQty { get; set; } = true;
        public bool? EnableInspection { get; set; } = false;
        public bool? EnableLaborQty { get; set; } = false;
        public bool? EnableLot { get; set; } = false;
        public bool? EnableNextOprSeq { get; set; } = false;
        public bool? EnablePCID { get; set; } = false;
        public bool? EnablePrintTagsList { get; set; } = false;
        public bool? EnableRecall { get; set; } = false;
        public bool? EnableRecallAprv { get; set; } = false;
        public bool? EnableRequestMove { get; set; } = false;
        public bool? EnableResourceGrpID { get; set; } = false;
        public bool? EnableSN { get; set; } = false;
        public bool? EnableScrapQty { get; set; } = true;
        public bool? EnableSubmit { get; set; } = true;
        public bool? EndActivity { get; set; } = false;
        public bool? EnteredOnCurPlant { get; set; } = true;
        public bool? EpicorFSA { get; set; } = false;
        public string? ExpenseCode { get; set; } = "";
        public string? ExpenseCodeDescription { get; set; } = "";
        public string? FSAAction { get; set; } = "";
        public string? FSACallCode { get; set; } = "";
        public string? FSAContractCode { get; set; } = "";
        public int? FSAContractNum { get; set; } = 0;
        public string? FSAEmpID { get; set; } = "";
        public int? FSAEquipmentInstallID { get; set; } = 0;
        public string? FSAEquipmentPartNum { get; set; } = "";
        public int? FSAServiceOrderNum { get; set; } = 0;
        public int? FSAServiceOrderResourceNum { get; set; } = 0;
        public string? FSAWarrantyCode { get; set; } = "";
        public bool? FSComplete { get; set; } = false;
        public string? FiscalCalendarID { get; set; } = "";
        public int? FiscalPeriod { get; set; } = 0;
        public int? FiscalYear { get; set; } = 0;
        public string? FiscalYearSuffix { get; set; } = "";
        public double? GLTranAmt { get; set; } = 0;
        public DateTime? GLTranDate { get; set; } = null;
        public bool? GLTrans { get; set; } = true;
        public bool? HCMIntregationHCMEnabled { get; set; } = false;
        public double? HCMPayHours { get; set; } = 8.25;
        public string? HCMStatusStatus { get; set; } = "";
        public bool? HH { get; set; } = false;
        public bool? HasAccessToRow { get; set; } = true;
        public bool? HasAttachments { get; set; } = false;
        public bool? HasComments { get; set; } = false;
        public bool? ISFixHourAndQtyOnly { get; set; } = false;
        public DateTime? ImportDate { get; set; } = null;
        public bool? Imported { get; set; } = false;
        public string? IndirectCode { get; set; } = "";
        public string? IndirectDescription { get; set; } = "";
        public bool? InspectionPending { get; set; } = false;
        public string? IntExternalKey { get; set; } = "";
        public string? JCDept { get; set; } = "";
        public string? JCDeptDescription { get; set; } = "";
        public bool? JCSystReworkReasons { get; set; } = true;
        public bool? JCSystScrapReasons { get; set; } = false;
        public string? JDFInputFiles { get; set; } = "";
        public string? JDFNumberOfPages { get; set; } = "";
        public string? JDFOpCompleted { get; set; } = "";
        public string? JobAsmblDescription { get; set; } = "";
        public string? JobAsmblPartNum { get; set; } = "";
        public string? JobNum { get; set; } = "";
        public bool? JobOperComplete { get; set; } = false;
        public string? JobType { get; set; } = "";
        public string? JobTypeCode { get; set; } = "MFG";
        public string? JournalCode { get; set; } = "";
        public int? JournalNum { get; set; } = 0;
        public string? LaborAttributeSetDescription { get; set; } = "";
        public int? LaborAttributeSetID { get; set; } = 0;
        public string? LaborAttributeSetShortDescription { get; set; } = "";
        public bool? LaborCollection { get; set; } = false;
        public double? LaborCost { get; set; } = 297412.5;
        public int? LaborDtlSeq { get; set; } = 4797;
        public string? LaborEntryMethod { get; set; } = "T";
        public int? LaborHedSeq { get; set; } = 1006;
        public double? LaborHrs { get; set; } = 8.25;
        public string? LaborNote { get; set; } = "";
        public double? LaborQty { get; set; } = 0;
        public double? LaborRate { get; set; } = 36050;
        public string? LaborRevision { get; set; } = "";
        public string? LaborType { get; set; } = "P";
        public string? LaborTypePseudo { get; set; } = "P";
        public string? LaborUOM { get; set; } = "";
        public string? LastTaskID { get; set; } = "";
        public string? LbrDay { get; set; } = "";
        public string? LbrMonth { get; set; } = "";
        public string? LbrWeek { get; set; } = "";
        public string? LotNum { get; set; } = "";
        public bool? MES { get; set; } = false;
        public string? MachineDescription { get; set; } = "";
        public string? MultipleEmployeesText { get; set; } = "";
        public int? NewDifDateFlag { get; set; } = 0;
        public string? NewPrjRoleCd { get; set; } = "";
        public string? NewRoleCdDesc { get; set; } = "";
        public int? NextAssemblySeq { get; set; } = 0;
        public string? NextOperDesc { get; set; } = "";
        public int? NextOprSeq { get; set; } = 0;
        public string? NextResourceDesc { get; set; } = "";
        public string? NonConfPCID { get; set; } = "";
        public bool? NonConfProcessed { get; set; } = false;
        public bool? NotSubmitted { get; set; } = true;
        public bool? OkToChangeResourceGrpID { get; set; } = false;
        public string? OpCode { get; set; } = "";
        public string? OpCodeOpDesc { get; set; } = "";
        public bool? OpComplete { get; set; } = false;
        public string? OpDescOpDesc { get; set; } = "";
        public int? OprSeq { get; set; } = 0;
        public string? OutputBin { get; set; } = "";
        public string? OutputWarehouse { get; set; } = "";
        public double? OverRidePayRate { get; set; } = 0;
        public bool? PBAllowModify { get; set; } = false;
        public double? PBChargeAmt { get; set; } = 0;
        public double? PBChargeRate { get; set; } = 0;
        public string? PBCurrencyCode { get; set; } = "";
        public double? PBHours { get; set; } = 0;
        public int? PBInvNum { get; set; } = 0;
        public string? PBRateFrom { get; set; } = "";
        public string? PCID { get; set; } = "";
        public int? PMUID { get; set; } = 0;
        public int? ParentLaborDtlSeq { get; set; } = 0;
        public string? PartDescription { get; set; } = "";
        public string? PartNum { get; set; } = "";
        public string? PayMethodName { get; set; } = "";
        public bool? PayMethodSummarizePerCustomer { get; set; } = false;
        public int? PayMethodType { get; set; } = 0;
        public DateTime? PayrollDate { get; set; } = DateTime.Parse("2024-08-20T00:00:00");
        public string? PendingApprovalBy { get; set; } = "";
        public string? PhaseID { get; set; } = "";
        public string? PhaseIDDescription { get; set; } = "";
        public string? PhaseJobNum { get; set; } = "";
        public int? PhaseOprSeq { get; set; } = 0;
        public bool? PostedToGL { get; set; } = false;
        public bool? PrintNCTag { get; set; } = false;
        public bool? PrintPCIDContents { get; set; } = false;
        public string? PrjUsedTran { get; set; } = "";
        public string? ProdDesc { get; set; } = "Production";
        public string? ProjPhaseID { get; set; } = "";
        public bool? ProjProcessed { get; set; } = false;
        public string? ProjectDescription { get; set; } = "";
        public string? ProjectID { get; set; } = "";
        public string? QuickEntryCode { get; set; } = "";
        public bool? ReWork { get; set; } = false;
        public string? ReWorkReasonDescription { get; set; } = "";
        public int? RefAssemblySeq { get; set; } = 0;
        public string? RefJobNum { get; set; } = "";
        public int? RefOprSeq { get; set; } = 0;
        public bool? ReportPartQtyAllowed { get; set; } = false;
        public bool? RequestMove { get; set; } = false;
        public string? ResReasonCode { get; set; } = "";
        public string? ResReasonDescription { get; set; } = "";
        public string? ResourceDesc { get; set; } = "";
        public string? ResourceGrpDescription { get; set; } = "";
        public string? ResourceGrpID { get; set; } = "";
        public string? ResourceID { get; set; } = "";
        public string? ReworkReasonCode { get; set; } = "";
        public string? RoleCd { get; set; } = "";
        public bool? RoleCdDisplayAll { get; set; } = false;
        public string? RoleCdList { get; set; } = "";
        public string? RoleCdRoleDescription { get; set; } = "";
        public string? RowMod { get; set; } = "A";
        public bool? RowSelected { get; set; } = false;
        public double? Rpt1PBChargeAmt { get; set; } = 0;
        public double? Rpt1PBChargeRate { get; set; } = 0;
        public double? Rpt2PBChargeAmt { get; set; } = 0;
        public double? Rpt2PBChargeRate { get; set; } = 0;
        public double? Rpt3PBChargeAmt { get; set; } = 0;
        public double? Rpt3PBChargeRate { get; set; } = 0;
        public string? ScrapAttributeSetDescription { get; set; } = "";
        public int? ScrapAttributeSetID { get; set; } = 0;
        public string? ScrapAttributeSetShortDescription { get; set; } = "";
        public double? ScrapQty { get; set; } = 0;
        public string? ScrapReasonCode { get; set; } = "";
        public string? ScrapReasonDescription { get; set; } = "";
        public string? ScrapRevision { get; set; } = "";
        public string? ScrapUOM { get; set; } = "";
        public bool? SentFromMES { get; set; } = false;
        public string? ServCode { get; set; } = "";
        public int? ServNum { get; set; } = 0;
        public double? SetupPctComplete { get; set; } = 0;
        public int? Shift { get; set; } = 1;
        public string? ShiftDescription { get; set; } = "SHIFT 1 (2 SHIFT)";
        public bool? StartActivity { get; set; } = false;
        public string? SubmittedBy { get; set; } = "";
        public int? SysRevID { get; set; } = 0;
        public Guid? SysRowID { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public string? TaskSetID { get; set; } = "";
        public string? TemplateID { get; set; } = "";
        public bool? TimeAutoSubmit { get; set; } = false;
        public bool? TimeDisableDelete { get; set; } = false;
        public bool? TimeDisableUpdate { get; set; } = false;
        public string? TimeStatus { get; set; } = "";
        public string? TimeTypCd { get; set; } = "";
        public string? TimeTypCdDescription { get; set; } = "";
        public bool? TrackInventoryByRevision { get; set; } = false;
        public string? TreeNodeImageName { get; set; } = "Entered";
        public bool? UnapprovedFirstArt { get; set; } = false;
        public bool? WFComplete { get; set; } = false;
        public string? WFGroupID { get; set; } = "";
        public bool? WIPTransaction { get; set; } = false;
        public string? WarrantyCode { get; set; } = "";
        public string? WeekDisplayText { get; set; } = "8/18/2024 - 8/24/2024";
        public bool? WipPosted { get; set; } = false;
    }

    public class GetLabor
    {
        public string LaborTypePseudo { get; set; }
        public int LaborHedSeq { get; set; }
        public int LaborDtlSeq { get; set; }
        public string JobNum { get; set; }
        public string OpSeq {  get; set; }
        public DateTime date { get;set; }
        public DateTime ClockInDate { get; set; }
        public string ClockinTime { get; set; }
        public string ClockOutTime { get; set; }
        public double LaborHrs { get; set; }
        public double BurdenHrs { get; set; }
        public int LaborQty { get; set; }
        public int ScrapQty { get; set; }
        public int DiscrepQty { get; set; }
        public string DiscrpRsnCode { get; set; }
        public string TimeStatus { get; set; }
        public string PartNum { get; set; }
        public string FieldName {  get; set; }
        public string TimeValue { get;set;}
        public int Shift { get; set; }
        public string ShiftDescription { get; set; }


        public string RowMod { get; set; }
        public string nik { get; set; }
        public string password { get; set; }
    }

    public class TimeDtlEntries
    {
        public string LaborTypePseudo {get; set;}
        public string JobNum { get; set; }
        public int AssemblySeq { get; set; }
        public int OprSeq { get; set; }
        public DateTime ClockInDate { get; set; }
        public string ClockinTime { get; set; }
        public string ClockOutTime { get; set; }
        public double LaborHrs { get; set; }
        public double BurdenHrs { get; set; }
        public string ResourceGrpID { get; set; }
        public string DiscrpRsnCode { get; set; }
        public string LaborNote { get; set; }
        public string nik { get; set; }
        public string password { get; set; }


    }
}

