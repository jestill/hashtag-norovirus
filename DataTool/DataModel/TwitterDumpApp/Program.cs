using Norovirus;
using Norovirus.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterDumpApp
{
    class Program
    {
        private const int batchCount = 100;
        private const int batchSize = 100;


        static void Main(string[] args)
        {
            string query = String.Format("q=Ebola&count={0}", batchSize);

            Dictionary<string, Tuple<double, double>> geoLocations = new Dictionary<string, Tuple<double, double>>();
            geoLocations.Add("Ann Arbor", new Tuple<double, double>(42.2808, -83.7430));
            geoLocations.Add("East Lansing", new Tuple<double, double>(42.7325, -84.5555));
            geoLocations.Add("Athens", new Tuple<double, double>(33.9519, -83.3576));
            geoLocations.Add("Arlington", new Tuple<double, double>(32.7357, -97.1080));


            List<Tuple<string, string>> queries = new List<Tuple<string, string>>();
            queries.Add(new Tuple<string, string>("trump", "50mi"));
            queries.Add(new Tuple<string, string>("snow", "50mi"));
            queries.Add(new Tuple<string, string>("norovirus", "500mi"));
            queries.Add(new Tuple<string, string>("ebola", "50mi"));
            queries.Add(new Tuple<string, string>("zika", "100mi"));
            queries.Add(new Tuple<string, string>("baseball", "50mi"));
            queries.Add(new Tuple<string, string>("gun", "50mi"));
            queries.Add(new Tuple<string, string>("coke", "100mi"));
            //queries.Add(new Tuple<string, string>("pop", "100mi"));
            //queries.Add(new Tuple<string, string>("soda", "100mi"));
            //queries.Add(new Tuple<string, string>("flu", "50mi"));
            //queries.Add(new Tuple<string, string>("flint water", "50mi"));
            //queries.Add(new Tuple<string, string>("NFL", "50mi"));
            //queries.Add(new Tuple<string, string>("ESPN", "50mi"));
            //queries.Add(new Tuple<string, string>("jobs", "50mi"));
            //queries.Add(new Tuple<string, string>("spring break", "50mi"));
            //queries.Add(new Tuple<string, string>("Harbaugh", "50mi"));
            


            List<TwitterResponse> responses = new List<TwitterResponse>();
            List<Mention> mentions = new List<Mention>();
            TwitterResponseMapper mapper = new TwitterResponseMapper();
            foreach( var queryInfo in queries) { 
                foreach (var key in geoLocations.Keys)
                {
                    Console.WriteLine($"Querying Twitter for '{queryInfo.Item1}' within {queryInfo.Item2} of {key}");
                    var localQuery = $"q={queryInfo.Item1}"+ $"&geocode={geoLocations[key].Item1},{geoLocations[key].Item2},{queryInfo.Item2}";
                    var subset = GetDataByBatch(localQuery);

                    mentions.AddRange(mapper.MapMany(subset, localQuery));
                }
            }

            Console.WriteLine("Generating CSV...");
            CSVFormatter formatter = new CSVFormatter();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\TwitterDumpNew.csv",false))
            {

                file.WriteLine(formatter.Columns());
                foreach (Mention m in mentions)
                {
                    file.WriteLine(formatter.Format(m));
                }
            }
            Console.WriteLine("Done");
        }

        static IEnumerable<TwitterResponse> GetDataByBatch(string query)
        {
            var stopwatch = new Stopwatch();
            IList<TwitterResponse> responseList = new List<TwitterResponse>();
            TwitterManager manager = new TwitterManager();

            // Fetch initial batch
            stopwatch.Start();
            var response = GetData(query,manager);
            responseList.Add(response);
            var sinceId = response.statuses.Max(r => r.id); //TODO sinceId does not seem to play nice
            var maxId = response.statuses.Min(r => r.id) - 1;

            // Fetch remaining batches
            for (int i = 0; maxId!=null && i < batchCount; i++)
            {
                response = GetData(query, manager, maxId.Value.ToString());
                if (response.statuses.Count() < 1)
                    break;
                responseList.Add(response);
                sinceId = response.statuses.Max(r => r.id);
                maxId = response.statuses.Min(r => r.id) - 1;
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed time: " + stopwatch.ElapsedMilliseconds);
            return responseList;
        }

        static TwitterResponse GetData(string query, TwitterManager manager, string maxId = null, string sinceId = null)
        {
            var baseQuery = query;

            if (maxId != null)
                baseQuery += String.Format("&max_id={0}", maxId);
            if (sinceId != null)
                baseQuery += String.Format("&since_id={0}", sinceId);

            //Console.WriteLine("Fetching batch... (query: \"" + baseQuery + "\")");

            var result = manager.GetApiData(baseQuery).Result;
            return result;
        }
    }
}
