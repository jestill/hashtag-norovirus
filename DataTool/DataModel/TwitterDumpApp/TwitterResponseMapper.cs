using Norovirus.DataModel;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterDumpApp
{
    class TwitterResponseMapper
    {

        private static DateTime fromString(string s)
        {

            //Thu Mar 03 16:51:13 +0000 2016
            return DateTime.ParseExact(s, "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture);
        }

        public IEnumerable<Mention> Map(TwitterResponse responses, string provenance)
        {
            List<Mention> result = new List<Mention>();
            foreach (Status status in responses.statuses)
            {
                Mention m = new Mention();
                m.PersonID = status.user.screen_name;
                m.PlatformID = "Twitter";
                m.TimeOfMessage =   fromString(status.created_at);
                //GeoCoordinate location = new GeoCoordinate();
                m.LocationOfMessage = null; //TODO
                m.Message = status.text;
                m.Provenance = provenance;
                result.Add(m);
            }

            return result;

        }
    }
}
