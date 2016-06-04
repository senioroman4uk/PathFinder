using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Trips.WebApi.Models
{
    public class DistanseMatrixResponseModel
    {
        public List<Row> Rows { get; set; }
        
        public string Status { get; set; }
    }
}
