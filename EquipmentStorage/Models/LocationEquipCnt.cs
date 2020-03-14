using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class LocationEquipCnt : Location
    {
        public int EquipCnt { get; set; }
        public string NameWithCnt => Name + "(" + EquipCnt + ")";
    }
}
