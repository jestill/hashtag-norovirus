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
        private static string query = String.Format("q=%norovirus&count={0}", batchSize);

        static void Main(string[] args)
        {
            var responseBatch = GetDataByBatch();
            TwitterResponseMapper mapper = new TwitterResponseMapper();
            IEnumerable<Mention> mentions = mapper.MapMany(responseBatch, query);

            Console.WriteLine("Generating CSV...");
            CSVFormatter formatter = new CSVFormatter();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\TwitterDump.csv"))
            {
                file.WriteLine(formatter.Columns());
                foreach (Mention m in mentions)
                {
                    file.WriteLine(formatter.Format(m));
                }
            }
            Console.WriteLine("Done");
        }

        static IEnumerable<TwitterResponse> GetDataByBatch()
        {
            var stopwatch = new Stopwatch();
            IList<TwitterResponse> responseList = new List<TwitterResponse>();
            TwitterManager manager = new TwitterManager();

            // Fetch initial batch
            stopwatch.Start();
            var response = GetData(manager);
            responseList.Add(response);
            var sinceId = response.statuses.Max(r => r.id); //TODO sinceId does not seem to play nice
            var maxId = response.statuses.Min(r => r.id) - 1;

            // Fetch remaining batches
            for (int i = 0; i < batchCount; i++)
            {
                response = GetData(manager, maxId.Value.ToString());
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

        static TwitterResponse GetData(TwitterManager manager, string maxId = null, string sinceId = null)
        {
            var baseQuery = query;

            if (maxId != null)
                baseQuery += String.Format("&max_id={0}", maxId);
            if (sinceId != null)
                baseQuery += String.Format("&since_id={0}", sinceId);

            Console.WriteLine("Fetching batch... (query: \"" + baseQuery + "\")");

            var result = manager.GetApiData(baseQuery).Result;
            return result;
        }
    }
}
