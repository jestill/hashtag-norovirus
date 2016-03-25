using Newtonsoft.Json.Linq;
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
    public class TwitterResponseMapper
    {
        private static DateTime fromString(string s)
        {
            //Thu Mar 03 16:51:13 +0000 2016
            return DateTime.ParseExact(s, "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture);
        }

        public IEnumerable<Mention> MapMany(IEnumerable<TwitterResponse> responses, string provenance)
        {
            var mentions = new List<Mention>();
            foreach (var response in responses)
            {
                mentions.AddRange(Map(response, provenance));
            }
            return mentions;
        }

        private void parseLocation(Mention m, Status s)
        {

            var geoEnabled = s.user.geo_enabled;
            var coordinates = s.coordinates;
            var place = s.place;
            if ( s.coordinates !=null )
            {
                var c = (s.coordinates as JObject).Last.First as JToken;
                m.LocationOfMessage = new GeoCoordinate( Double.Parse(c.Last.ToString()), Double.Parse(c.First.ToString()));
            }
            if ( place != null)
            {
                m.NamedLocation = place.full_name;
            }

        }


        public IEnumerable<Mention> Map(TwitterResponse response, string provenance)
        {
            IList<Mention> result = new List<Mention>();
            foreach (Status status in response.statuses)
            {
                Mention m = new Mention();
                m.PersonID = status.user.screen_name;
                m.PlatformID = "Twitter";
                m.TimeOfMessage =   fromString(status.created_at);
                //GeoCoordinate location = new GeoCoordinate();
                parseLocation(m, status); //TODO
                m.Message = status.text;
                m.Provenance = provenance;
                result.Add(m);
            }
            return result;
        }
    }
}
