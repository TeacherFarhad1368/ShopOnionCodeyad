using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class FileSecurity
    {
        public static bool IsImage(this IFormFile file)
        {
            return true;
        }
    }
}
