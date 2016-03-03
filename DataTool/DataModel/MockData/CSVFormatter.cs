using Norovirus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockData
{
    public class CSVFormatter :IMentionFormatter
    {

        public string Columns()
        {
            return "MessageID,PersonaID,PersonID,PlatformID,MessageID,TimeOfMessage,Latitude,Longitude,Message,Provenance";
        }

        public string Format(Mention m)
        {

            return $"{m.MessageID},{m.PersonaID},{m.PersonID},{m.PlatformID},{m.MessageID},{m.TimeOfMessage},{m.LocationOfMessage?.Latitude},{m.LocationOfMessage?.Longitude},{m.Message},{m.Provenance}";

        }
    }
}
