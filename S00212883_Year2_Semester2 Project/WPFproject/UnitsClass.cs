using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject
{
    public class UnitsClass : IComparable
    {
        public string UnitName { get; set; }
        public string UnitValue { get; set; }
        public int UnitType { get; set; }
        public string UnitImage { get; set; }

        int IComparable.CompareTo(object input)
        {
            UnitsClass toCompare = (UnitsClass)input;
            return this.UnitType.CompareTo(toCompare.UnitType);
        }

    }
}
