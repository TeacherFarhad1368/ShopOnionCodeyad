using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application
{
    public static class GenerateRandomCode
    {
        public static int GenerateUserRegisterCode()
        {
            Random random = new Random();
            return random.Next(00000, 99999);
        }
    }
}
