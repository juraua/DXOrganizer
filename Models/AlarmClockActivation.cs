using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace AlarmClock.Models
{
    public class AlarmClockActivation
    {
        private static Timer tmrShow;

        public static void StartTimer()
        {
            tmrShow = new Timer(tmrShow_Tick, null, 0, 5000);
            
        }

        public static void StopTimer()
        {
            tmrShow.Dispose();
        }

 
         private static void tmrShow_Tick(object sender)
        {
            //TODO: Do smtng
        }
    }
}