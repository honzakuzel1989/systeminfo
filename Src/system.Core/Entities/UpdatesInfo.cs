using System;
using System.Collections.Generic;
using System.Text;

namespace systeminfo.Core.Entities
{
    public class UpdatesInfo
    {
        public UpdatesInfo(Package[] packages)
        {
            Packages = packages;
            PackagesCount = packages.Length;
        }

        public Package[] Packages { get; }
        public int PackagesCount { get; }
    }

    public class Package
    {
        public Package(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
