using System;

namespace systeminfo.Core.Entities
{
    public class DiskMetrics
    {
        public DiskMetrics(Percentage usedPerc, DiskSize size, DiskSize used, DiskSize avail, string name)
        {
            UsedPerc = usedPerc;
            Size = size;
            Used = used;
            Avail = avail;
            Name = name;
        }

        public Percentage UsedPerc { get; }
        public DiskSize Size { get; }
        public DiskSize Used { get; }
        public DiskSize Avail { get; }
        public string Name { get; }

        public override string ToString()
        {
            return $"{Name}\t{Size}G {Used}G {Avail}G {UsedPerc}";
        }
    }

    public class DiskSize
    {
        private const double KB = 1024.0;

        public int MiB { get; }
        public int GiB { get; }

        private DiskSize(int kilobyte)
        {
            MiB = (int)Math.Round(kilobyte / KB);
            GiB = (int)Math.Round(kilobyte / KB);
        }

        public static implicit operator DiskSize(int size)
        {
            return new DiskSize(size);
        }

        public override string ToString()
        {
            return $"{GiB}G";
        }
    }
}
