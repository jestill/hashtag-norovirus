using Norovirus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Norovirus
{
    public class CSVFormatter :IMentionFormatter
    {
        private static char SEP = '|';
        public string Columns()
        {
            return "MessageID" + SEP + "PersonaID" + SEP + "PersonID" + SEP + "PlatformID" + SEP + "MessageID" + SEP + "TimeOfMessage" + SEP + "Latitude" + SEP + "Longitude" + SEP + "Message" + SEP + "Provenance";
        }

        public string Format(Mention m)
        {

            var f = $"{m.MessageID}{SEP}{m.PersonaID}{SEP}{m.PersonID}{SEP}{m.PlatformID}{SEP}{m.MessageID}{SEP}{m.TimeOfMessage}{SEP}{m.LocationOfMessage?.Latitude}{SEP}{m.LocationOfMessage?.Longitude}{SEP}{m.Message}{SEP}{m.Provenance}";
            f = f.Replace("\n", " ");
            return f;

        }
    }
}
