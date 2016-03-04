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
            var sinceId = response.search_metadata.since_id_str;

            // Fetch remaining batches
            for (int i = 0; i < batchCount; i++)
            {
                response = GetData(manager, sinceId);
                responseList.Add(response);
                sinceId = response.search_metadata.since_id_str;
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed time: " + stopwatch.ElapsedMilliseconds);
            return responseList;
        }

        static TwitterResponse GetData(TwitterManager manager, string maxId = null)
        {
            Console.WriteLine("Fetching batch...");
            var result = manager.GetApiData(query).Result;
            return result;
        }
    }
}
