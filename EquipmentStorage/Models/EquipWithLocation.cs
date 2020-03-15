using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class EquipWithLocation : Equipment
    {
        public Location Room { get; set; }
    }
}
