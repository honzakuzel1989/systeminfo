using System.Core.Entities;
using System.Threading.Tasks;

namespace System.Core.Services
{
    public interface ISystemInfoProvider
    {
        Task<SystemInfo> Get();
    }
}