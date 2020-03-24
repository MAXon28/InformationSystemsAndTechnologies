using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InformationSystemsAndTechnologies.Model;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class NewProductViewModel : ViewModelBase
    {
        private ProductsLogic productsLogic;

        public NewProductViewModel()
        {
            productsLogic = ProductsLogic.GetInstance();
            Condition = "Visible";
        }

        public string Condition { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }

        public double Weight { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(() => Condition = "Collapsed");
            }
        }

        public ICommand SaveNewProduct
        {
            get
            {
                return new RelayCommand(() =>
                {
                    productsLogic.AddNewProductAsync(Name, Price, Count, Weight, Color, Description);
                    Condition = "Collapsed";
                });
            }
        }
    }
}