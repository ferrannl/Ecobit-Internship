using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcobitStage.Offline
{
    public static class ConnectionCheck
    {
        public static bool Online;
        public static string errorMsg = "Sorry u bent offline";
        public static void IsOnline()
        {
            while (true)
            {
                Online = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            }
        }
    }
}
