using Norovirus;
using Norovirus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockData
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator dataGen = new DataGenerator();
            dataGen.BatchSize = 5;
            IEnumerable<Mention> generated = dataGen.Generate();

            CSVFormatter formatter = new CSVFormatter();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\MockData.csv"))
            {
                file.WriteLine(formatter.Columns());
                foreach (Mention m in generated)
                {
                    file.WriteLine(formatter.Format(m));
                }
            }
        }
    }
}
