namespace PathFinder.Cars.WebApi.Models
{
    class CarReadModel
    {
        public int Id { get; set; }
        
        public int Comfort { get; set; }

        public int Capacity { get; set; }

        public UserModel Owner { get; set; }

        public ColorReadModel Color { get; set; }

        public CarModelReadModel Model { get; set; }
    }
}
