using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibTimer
{
    internal interface ICore
    {
        int Min { get; }
        int Sec { get; }

        int Hour { get; }

        void UpdateMin();
        void UpdateHour();

        void UpdateSec();

        void RestSec();

        void RestMin();

        void RestHour();

    }


}
