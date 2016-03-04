using Norovirus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterDataParser
{
    public class TwitterParser
    {

        public IEnumerable<Mention> Parse(string jsonPath)
        {
            string text = System.IO.File.ReadAllText(jsonPath);

            JSON
        }
    }
}
