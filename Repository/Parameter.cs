using EPAPI.Repository.IssueMtl;
using EPAPI.Repository.JobReceipt;
using EPAPI.Repository.Purchase;
using EPAPI.Repository.ReceiptEntry;

namespace EPAPI.Repository
{
    public class ResponseObj
    {
        public string ReturnObj { get; set; }
    }
    public class Responder
    {
        public Parameters Parameters { get; set; }
    }
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
        public List<RcvHead> RcvHead { get; set; }
        public List<RcvDtl> RcvDtl { get; set; }
        public List<RcvHeadAttch> RcvHeadAttch { get; set; }
        public List<IssueReturn> IssueReturn { get; set; }
        public List<SelectedJobAsmbl> SelectedJobAsmbl { get; set; }
    }
    public class ReturnObj
    {
        public List<IssueReturn> IssueReturn { get; set; }
        public List<IssueReturnJobAsmbl> IssueReturnJobAsmbl { get; set; }

    }
}
