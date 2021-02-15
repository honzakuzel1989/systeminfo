using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace System.Core.Services
{
    public interface ISystemInfoProvider
    {
        Task<SystemInfo> Get();
    }
}