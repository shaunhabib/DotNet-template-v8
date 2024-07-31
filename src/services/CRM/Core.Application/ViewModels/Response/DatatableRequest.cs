using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Response
{
    public class Dt_Search
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }


    public class Dt_Query
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Dt_Search search { get; set; }
    }

    public class Dt_Root
    {
        public Dt_Query query { get; set; }
    }
}
