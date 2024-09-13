using EPAPI.Repository.JobReceipt;
using EPAPI.Repository.Purchase;

namespace EPAPI.Repository
{
    public class Parameters
    {
        public Ds ds { get; set; }
        public string? pcMessage { get; set; }
    }
    public class Ds
    {
        public List<LaborHed> LaborHed { get; set; }
        public List<LaborDtl> LaborDtl { get; set; }
        public List<PartTran> PartTran { get; set; }
        public List<LegalNumGenOpts> LegalNumGenOpts { get; set; }


    }
}
