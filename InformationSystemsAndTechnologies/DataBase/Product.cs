using GalaSoft.MvvmLight;

namespace InformationSystemsAndTechnologies.DataBase
{
    public class Product : ViewModelBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }

        public double Weight { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }
    }
}