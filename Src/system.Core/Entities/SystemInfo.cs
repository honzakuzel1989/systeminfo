using System;

namespace systeminfo.Core.Entities
{
    public class SystemInfo
    {
        public SystemInfo(CpuUsageInfo cpuInfo, MemoryUsageInfo memoryInfo, DiskUsageInfo diskInfo)
        {
            CpuInfo = cpuInfo;
            MemoryInfo = memoryInfo;
            DiskInfo = diskInfo;
        }

        public CpuUsageInfo CpuInfo { get; }
        public MemoryUsageInfo MemoryInfo { get; }
        public DiskUsageInfo DiskInfo { get; }
    }

    public abstract class UsageInfo
    {
        protected UsageInfo(Percentage usage)
        {
            Usage = usage;
        }

        public Percentage Usage { get; }
    }

    public class CpuUsageInfo : UsageInfo
    {
        public CpuUsageInfo(Percentage value) : base(value)
        {
        }
    }

    public class MemoryUsageInfo : UsageInfo
    {
        public MemoryUsageInfo(Percentage value) : base(value)
        {
        }
    }

    public class DiskUsageInfo : UsageInfo
    {
        public DiskUsageInfo(Percentage value) : base(value)
        {
        }
    }

    public class Percentage
    {
        public Percentage(byte value)
        {
            if (value < 0)
                throw new ArgumentException($"Input value {value} is lesser than 0%", nameof(value));

            if (value > 100)
                throw new ArgumentException($"Input value {value} is bigger than 100%", nameof(value));

            Value = value;
        }

        public Percentage(double value) : this((byte)value)
        {

        }

        public byte Value { get; }

        public static implicit operator Percentage(int x)
        {
            return new Percentage(x);
        }

        public static implicit operator Percentage(double x)
        {
            return new Percentage(x);
        }

        public override string ToString()
        {
            return $"{Value}%";
        }
    }
}
