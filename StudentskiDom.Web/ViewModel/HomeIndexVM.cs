using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentskiDom.Web.ViewModel
{
    public class HomeIndexVM
    {
        public List<Row> Rows { get; set; }

        public class Row {
          public  int DrzavaID { get; set; }
           public  string NazivDrzave { get; set; }
        }
    }
}
