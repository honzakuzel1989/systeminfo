using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services
{
    public interface ISystemInfoProvider
    {
        Task<SystemInfo> Get();
    }
}