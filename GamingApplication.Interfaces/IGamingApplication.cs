using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingApplication.Interfaces
{
    public interface IGamingApplication
    {
        bool InsertGamingData(string rou_rolled, int money, string player, ref string error);
    }
}
