namespace systeminfo.Core.Entities
{
    public class CpuMetrics
    {
        public CpuMetrics(Percentage usedPerc)
        {
            UsedPerc = usedPerc;
        }

        public Percentage UsedPerc { get; set; }

        public override string ToString()
        {
            return $"{UsedPerc}";
        }
    }
}
