using Norovirus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockData
{
    interface IMentionFormatter
    {
       string Columns();
       string Format(Mention m);
    }
}
