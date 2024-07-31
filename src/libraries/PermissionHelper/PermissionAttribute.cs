using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionHelper
{
    public class PermissionAttribute : Attribute
    {
        public string[] Name;

        public PermissionAttribute(string[] name)
        {
            Name = name;
        }
    }
}
