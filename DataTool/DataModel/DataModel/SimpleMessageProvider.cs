using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Norovirus
{
    class FlatFileStringProviderProvider : IStringProvider
    {
        Random r = new Random();
        private string[] messages;
        public FlatFileStringProviderProvider(string dataPath )
        {
            messages = System.IO.File.ReadAllLines(dataPath);
        }

        public string GetString()
        {
            int index = r.Next(0, messages.Count());
            return messages[index];
        }
    }
}
