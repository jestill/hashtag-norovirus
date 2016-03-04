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
                m.LocationOfMessage = null; //TODO
                m.Message = status.text;
                m.Provenance = provenance;
                result.Add(m);
            }
            return result;
        }
    }
}
