namespace LibTimer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

        public sealed class clsTimerCore : ICore
        {
            public int Min { get; private set; }
            public int Sec { get; private set; }

          public int Hour { get; private set; }


        public clsTimerCore()
            {
                Min = 0;
                Sec = 0;
            Hour = 0;
            }

        public void UpdateHour()
        {
            if (Hour < 23) Hour++;
            else RestHour(); 
        }

            public void UpdateMin()
            {
                // min less than 60 then increment it 
                if (this.Min < 59) this.Min++;
                else this.Min = 0; // rest min to zero again 
            }

            public void UpdateSec()
            {
                if (this.Sec < 59) this.Sec++;
                else this.Sec = 0;
            }

            public void RestSec()
            {
                this.Sec = 0;
            }

            public void RestMin()
            {
                this.Min = 0;
            }

        public void RestHour()
        {
            this.Hour= 0;
        }


        }
    }


