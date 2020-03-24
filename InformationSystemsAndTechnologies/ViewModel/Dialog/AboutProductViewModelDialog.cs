using GalaSoft.MvvmLight;

namespace InformationSystemsAndTechnologies.ViewModel.Dialog
{
    public class AboutProductViewModelDialog : ViewModelBase
    {
        public AboutProductViewModelDialog() { }

        public AboutProductViewModelDialog((string, int, double, string, string) data)
        {
            Name = data.Item1;
            Price = data.Item2;
            Weight = data.Item3;
            Color = data.Item4;
            Description = data.Item5;
        }

        public string Name { get; set; }

        public int Price { get; set; }

        public double Weight { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }
    }
}