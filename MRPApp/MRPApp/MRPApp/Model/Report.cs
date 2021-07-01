using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRPApp.Model
{
    public class Report
    {
        public int SchIdx { get; set; }
        public string PlantCode { get; set; }
        public System.DateTime PrcDate { get; set; }
        public Nullable<int> SchAmount { get; set; }
        public Nullable<int> PrcOkAmount { get; set; }
        public Nullable<int> PrcFailAmount { get; set; }
    }
}
