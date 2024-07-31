using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionHelper
{
    public interface IPermissionService
    {
        Task<bool> CheckAuthority(string authToken, PermissionVm permissionVm);
    }
}
