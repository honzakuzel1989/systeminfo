using System;

namespace systeminfo.Core.Entities
{
    public class SystemInfo
    {
        public SystemInfo(UsageInfo usageInfo, NetworkInfo networkInfo, UpdatesInfo updatesInfo)
        {
            UsageInfo = usageInfo;
            NetworkInfo = networkInfo;
            UpdatesInfo = updatesInfo;
        }
        
        public NetworkInfo NetworkInfo { get; }
        public UpdatesInfo UpdatesInfo { get; }
        public UsageInfo UsageInfo { get; set; }
    }

    public class NetworkInfo
    {
        public NetworkInfo(string ipAddress, string @interface)
        {
            IpAddress = ipAddress;
            Interface = @interface;
        }

        public string IpAddress { get; set; }
        public string Interface { get; set; }

        public override string ToString()
        {
            return $"{Interface}: {IpAddress}";
        }
    }

    public class UsageInfo
    {
        public UsageInfo(CpuUsageInfo cpuInfo, MemoryUsageInfo memoryInfo, DiskUsageInfo diskInfo)
        {
            CpuInfo = cpuInfo;
            MemoryInfo = memoryInfo;
            DiskInfo = diskInfo;
        }

        public CpuUsageInfo CpuInfo { get; }
        public MemoryUsageInfo MemoryInfo { get; }
        public DiskUsageInfo DiskInfo { get; }
    }

    public class CpuUsageInfo
    {
        public CpuUsageInfo(Percentage value)
        {
            Usage = value;
        }

        public Percentage Usage { get; }
    }

    public class MemoryUsageInfo
    {
        public MemoryUsageInfo(Percentage value)
        {
            Usage = value;
        }

        public Percentage Usage { get; }
    }

    public class DiskUsageInfo
    {
        public DiskUsageInfo(Percentage value)
        {
            Usage = value;
        }

        public Percentage Usage { get; }
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
