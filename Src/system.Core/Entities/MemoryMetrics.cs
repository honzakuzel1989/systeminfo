using System;
using System.Collections.Generic;
using System.Core.Entities;
using System.Text;

namespace systeminfo.Core.Entities
{
    public class MemoryMetrics
    {
        public MemoryMetrics(double total, double free, double used)
        {
            Total = total;
            Free = free;
            Used = used;

            FreePerc = new Percentage(free / total * 100);
            UsedPerc = new Percentage(used / total * 100);
        }

        public double Total { get; }
        public double Free { get; }
        public double Used { get; }
        public Percentage FreePerc { get; }
        public Percentage UsedPerc { get; }

        public override string ToString()
        {
            return $"{Total}, {Used}, {Free}";
        }
    }
}
