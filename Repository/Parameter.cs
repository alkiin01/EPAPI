using EPAPI.Repository.Purchase;

namespace EPAPI.Repository
{
    public class Parameters
    {
        public Ds ds { get; set; }
    }
    public class Ds
    {
        public List<LaborHed> LaborHed { get; set; }
        public List<LaborDtl> LaborDtl { get; set; }

    }
}
