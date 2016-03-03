using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Norovirus.DataModel
{
    public class Mention
    {
        public string PersonID { get; set; }
        public string PersonaID { get; set; }
        public string PlatformID { get; set; }
        public int MessageID { get; set; }
        public DateTime TimeOfMessage { get; set; }
        public GeoCoordinate LocationOfMessage { get; set; }
        public string Message { get; set; }
        public string Provenance { get; set; }

        public override string ToString()
        {
            return $"MessageID:{MessageID} PersonaID:{PersonaID} Timestamp:{TimeOfMessage} Long:{LocationOfMessage?.Longitude} Lat:{LocationOfMessage?.Latitude} Message:{Message}";
        }
    }
}
