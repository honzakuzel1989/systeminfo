using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services
{
    public interface IUpdatesInfoProvider
    {
        Task<UpdatesInfo> GetUpdatesInfo();
    }
}
