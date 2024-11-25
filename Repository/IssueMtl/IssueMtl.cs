namespace EPAPI.Repository.IssueMtl
{
    public class IssueMtl
    {
        public Parameters parameters { get; set; }
        public ReturnObj returnObj { get; set; }
    }
   
    public class IssueReturnJobAsmbl
    {
        public string Company { get; set; }
        public string JobNum { get; set; }
        public int AssemblySeq { get; set; }
        public string PartNum { get; set; }
        public string Description { get; set; }
        public string JobHeadPartNum { get; set; }
        public string JobHeadPartDescription { get; set; }
        public string EquipID { get; set; }
        public Guid SysRowID { get; set; }
        public string RowMod { get; set; }
    }
    public class SelectedJobAsmbl {
        public int AssemblySeq { get; set; } = 0;
        public string Company { get; set; } = "SAI";
        public string JobNum { get; set; } = "";
        public int JobMtlSeq {  get; set; } 
        public string SysRowID { get; set; } = "00000000-0000-0000-0000-000000000000";
        public string RowMod { get; set; } = "";
        public string BinNum { get; set; } = "GENERAL";
        public string BinNumDescription { get; set; } = "GENEREAL BIN";
        public string Qty { get; set; } = "0";
        public string WarehouseCode { get; set; } = "";
        public string WarehouseDescription { get; set; }
        public string LotNum { get; set; } = "A";
        public string nik { get; set; } 
        public string password { get; set; } 
        public bool IsReturn { get; set; }

    }
    public class IssueReturn
    {
        public string? AttributeSetDescription { get; set; }
        public int? AttributeSetID { get; set; }
        public string? AttributeSetShortDescription { get; set; }
        public string? Company { get; set; }
        public string? DUM { get; set; }
        public string? DimCode { get; set; }
        public string? DimCodeDimCodeDescription { get; set; }
        public double? DimConvFactor { get; set; }
        public bool? DisablePCIDRelatedFields { get; set; }
        public double? DispNumberOfPieces { get; set; }
        public string? DummyKeyField { get; set; }
        public bool? EnableAttributeSetSearch { get; set; }
        public bool? EnableFromFields { get; set; }
        public bool? EnableFromPCID { get; set; }
        public bool? EnablePCIDGen { get; set; }
        public bool? EnablePCIDPrint { get; set; }
        public bool? EnableSN { get; set; }
        public bool? EnableToFields { get; set; }
        public bool? EnableToPCID { get; set; }
        public bool? EpicorFSA { get; set; }
        public string? FromAssemblyPartDesc { get; set; }
        public string? FromAssemblyPartDescription { get; set; }
        public string? FromAssemblyPartNum { get; set; }
        public string? FromAssemblyRevisionNum { get; set; }
        public int? FromAssemblySeq { get; set; }
        public string? FromBinNum { get; set; }
        public string? FromBinNumDescription { get; set; }
        public string? FromJobNum { get; set; }
        public string? FromJobPartDesc { get; set; }
        public string? FromJobPartDescription { get; set; }
        public string? FromJobPartNum { get; set; }
        public string? FromJobPlant { get; set; }
        public string? FromJobRevisionNum { get; set; }
        public int? FromJobSeq { get; set; }
        public string? FromJobSeqPartDesc { get; set; }
        public string? FromJobSeqPartDescription { get; set; }
        public string? FromJobSeqPartNum { get; set; }
        public string? FromPCID { get; set; }
        public int? FromPCIDItemSeq { get; set; }
        public string? FromWarehouseCode { get; set; }
        public string? FromWarehouseCodeDescription { get; set; }
        public string? InvAdjReason { get; set; }
        public bool? IssuedComplete { get; set; }
        public string? LotNum { get; set; }
        public string? LotNumPartLotDescription { get; set; }
        public Guid? MtlQueueRowId { get; set; }
        public decimal? OnHandQty { get; set; }
        public string? OnHandUM { get; set; }
        public int? OrderLine { get; set; }
        public int? OrderNum { get; set; }
        public int? OrderRel { get; set; }
        public string? PartAttrClassID { get; set; }
        public string? PartIUM { get; set; }
        public string? PartNum { get; set; }
        public string? PartPartDescription { get; set; }
        public string? PartPricePerCode { get; set; }
        public string? PartSalesUM { get; set; }
        public double? PartSellingFactor { get; set; }
        public bool? PartTrackDimension { get; set; }
        public bool? PartTrackInventoryAttributes { get; set; }
        public bool? PartTrackInventoryByRevision { get; set; }
        public bool? PartTrackLots { get; set; }
        public bool? PartTrackSerialNum { get; set; }
        public Guid? PartWipSysRowID { get; set; }
        public bool? PkgControlHeaderDeleted { get; set; }
        public string? PkgControlID { get; set; }
        public string? Plant { get; set; }
        public bool? PlantConfCtrlEnablePackageControl { get; set; }
        public string? ProcessID { get; set; }
        public decimal? QtyPreviouslyIssued { get; set; }
        public decimal? QtyRequired { get; set; }
        public string? ReasonCode { get; set; }
        public string? ReasonCodeDescription { get; set; }
        public string? ReasonType { get; set; }
        public bool? ReassignSNAsm { get; set; }
        public bool? ReplenishDecreased { get; set; }
        public decimal? RequirementQty { get; set; }
        public string? RequirementUOM { get; set; }
        public string? RevisionNum { get; set; }
        public string? RowMod { get; set; }
        public string? SerialControlPlant { get; set; }
        public string? SerialControlPlant2 { get; set; }
        public bool? SerialControlPlantIsFromPlt { get; set; }
        public int? SerialNoQty { get; set; }
        public Guid? SysRowID { get; set; }
        public int? TFOrdLine { get; set; }
        public string? TFOrdNum { get; set; }
        public string? ToAssemblyPartDesc { get; set; }
        public string? ToAssemblyPartDescription { get; set; }
        public string? ToAssemblyPartNum { get; set; }
        public int? ToAssemblySeq { get; set; }
        public string? ToBinNum { get; set; }
        public string? ToBinNumDescription { get; set; }
        public string? ToJobNum { get; set; }
        public string? ToJobPartDesc { get; set; }
        public string? ToJobPartDescription { get; set; }
        public string? ToJobPartNum { get; set; }
        public string? ToJobPlant { get; set; }
        public int? ToJobSeq { get; set; }
        public string? ToJobSeqPartDesc { get; set; }
        public string? ToJobSeqPartDescription { get; set; }
        public string? ToJobSeqPartNum { get; set; }
        public string? ToPCID { get; set; }
        public int? ToPCIDItemSeq { get; set; }
        public string? ToWarehouseCode { get; set; }
        public string? ToWarehouseCodeDescription { get; set; }
        public bool? TrackDimension { get; set; }
        public DateTime? TranDate { get; set; }
        public string? TranDocTypeID { get; set; }
        public decimal? TranQty { get; set; }
        public string? TranReference { get; set; }
        public string? TranType { get; set; }
        public string? TreeDisplay { get; set; }
        public string? UM { get; set; }
    }
}
