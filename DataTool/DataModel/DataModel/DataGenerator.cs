using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Norovirus.DataModel
{
    public class DataGenerator
    {

        public int BatchSize { get; set; } = 5;

        public IEnumerable<string> PersonIDs { get; set; } = new List<string> { "0123456789" };

        public IStringProvider PersonaProvider { get; set; } = new FlatFileStringProviderProvider(@"C:\Users\its-rowc\Source\Repos\hashtag-norovirus\DataTool\DataModel\DataModel\Personas.txt");

        public IEnumerable<string> PlatformIDs { get; set; } = new List<string> { "Twitter", "Facebook", "YikYak" };

        public GeoCoordinate BaseLocation { get; set; } = new GeoCoordinate(42.2814, 83.7483);

        public IStringProvider MessageProvider { get; set; } = new FlatFileStringProviderProvider(@"C:\Users\its-rowc\Source\Repos\hashtag-norovirus\DataTool\DataModel\DataModel\Messages.txt");

        private Random r = new Random();
        

        public IEnumerable<Mention> Generate()
        {

            List<Mention> result = new List<Mention>(BatchSize);
            for (int index=0; index<BatchSize; ++index)
            {
                result.Add(GenerateMention(index));
            }
            return result;
        }

        private Mention GenerateMention(int id)
        {
            Mention m = new Mention() { MessageID = id };
            m.PersonaID = PersonaProvider.GetString();
            m.PersonID = "";
            m.PlatformID = GetRandomFromEnumerable(PlatformIDs);
            m.TimeOfMessage = GetRandomTime(14);
            m.LocationOfMessage = GetRandomLocation(BaseLocation);
            m.Message = MessageProvider.GetString();
            m.Provenance = "";
            return m;
        }


        private string GetRandomFromEnumerable(IEnumerable<string> source)
        {
            int rInt = r.Next(0, source.Count()); //for ints
            return source.ElementAt(rInt);
        }

        private GeoCoordinate GetRandomLocation(GeoCoordinate basis)
        {

            double latOffset = (r.NextDouble() * .01 )* (r.Next(2)>0?1:-1);
            double longOffset = (r.NextDouble() * .01)* (r.Next(2) > 0 ? 1 : -1);


            return new GeoCoordinate(basis.Latitude+latOffset, basis.Longitude+longOffset);

        }

        public DateTime GetRandomTime(int daysAgo)
        {

            DateTime start = DateTime.Now.AddDays(-daysAgo).AddSeconds(-(r.Next(0,60*60*24)));
            int range = (DateTime.Today - start).Days;
            return start.AddDays(r.Next(range));

        }
    }
}
