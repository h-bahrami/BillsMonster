using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Common
{
    public interface IDateTime
    {
        DateTime Now { get;}

        int CurrentYear { get;}

        int CurrentMonth { get; }

        int CurrentDay { get;}       
        
    }
}
