using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Trips.WebApi.Models
{
    public class DistanseMatrixResponseModel
    {
//        public List<string> destination_addresses { get; set; }
//        public List<string> origin_addresses { get; set; }
        public List<Row> Rows { get; set; }
        
        public string Status { get; set; }
    }
}
