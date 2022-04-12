using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject
{
    class SubFactionClass
    {
        public int SubfactionID { get; set; }
        public string SubFactionName { get; set; }

        public override string ToString()
        {
            return SubFactionName;
        }
    }
}
