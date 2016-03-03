using Norovirus;
using Norovirus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterDumpApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterManager manager = new TwitterManager();

            string query = "q=%norovirus&count=100";

            var result = manager.GetApiData(query).Result;

            TwitterResponseMapper mapper = new TwitterResponseMapper();
            IEnumerable<Mention> mentions = mapper.Map(result, query);


            CSVFormatter formatter = new CSVFormatter();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\TwitterDump.csv"))
            {
                file.WriteLine(formatter.Columns());
                foreach (Mention m in mentions)
                {
                    file.WriteLine(formatter.Format(m));
                }
            }

        }
    }
}
