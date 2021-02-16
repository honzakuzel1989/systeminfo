using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace System.Core.Services
{
    public interface IUsageInfoProvider
    {
        Task<UsageInfo> GetUsageInfo(string fs);
    }
}