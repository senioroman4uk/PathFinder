//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PathFinder.Cars.DAL.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int ModelId { get; set; }
        public int Comfort { get; set; }
        public int ColorId { get; set; }
        public int Capacity { get; set; }
    
        public virtual User Owner { get; set; }
        public virtual CarColor Color { get; set; }
        public virtual CarModel Model { get; set; }
    }
}
