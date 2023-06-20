using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationsForExcelXMLGenerator
{
    class LocationData
    {
        public double id { get; set; }
        public String label {get; set;}
        public String type { get; set; }
        public double parentID { get; set; }

        public LocationData (double id, String label, String type, double parentID)
        {
            this.id = id;
            this.label = label;
            this.type = type;
            this.parentID = parentID;
        
        }
        public LocationData() { }

        public override bool Equals(object obj)
        {
            return this.id == ((LocationData)obj).id;
        }

    }
}
