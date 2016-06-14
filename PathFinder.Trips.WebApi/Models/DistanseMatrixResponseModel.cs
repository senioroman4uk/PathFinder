using System.Collections.Generic;

namespace PathFinder.Trips.WebApi.Models
{
    public class DistanseMatrixResponseModel
    {
        public List<Row> Rows { get; set; }
        
        public string Status { get; set; }
    }
}
