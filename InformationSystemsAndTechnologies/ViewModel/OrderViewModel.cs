using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InformationSystemsAndTechnologies.DataBase;
using InformationSystemsAndTechnologies.Model;
using InformationSystemsAndTechnologies.View.Dialog;
using InformationSystemsAndTechnologies.ViewModel.Dialog;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class OrderViewModel : ViewModelBase
    {
        private HistoriesLogic historiesLogic;
        private ProductsLogic productsLogic;
        private AboutProductViewModelDialog aboutProductViewModelDialog;

        public OrderViewModel()
        {
            Condition = "Visible";
            historiesLogic = HistoriesLogic.GetInstance();
            productsLogic = ProductsLogic.GetInstance();
            Products = new ObservableCollection<Product>(productsLogic.GetListProducts());
            InCheck = new ObservableCollection<Product>();
            FinishSum = 0;
            ViewAboutProduct = new RelayCommand<int>(ViewProduct);
            AddToCheck = new RelayCommand<int>(AddToBuy);
            DeleteProduct = new RelayCommand<int>(DeleteAtCheck);
        }

        public string Condition { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public ObservableCollection<Product> InCheck { get; set; }

        public int FinishSum { get; set; }

        public RelayCommand<int> ViewAboutProduct { get; set; }

        public RelayCommand<int> AddToCheck { get; set; }

        public RelayCommand<int> DeleteProduct { get; set; }

        public ICommand ToPay
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (FinishSum > 0)
                    {
                        RewriteCountProducts();
                        historiesLogic.AddNewOrder(InCheck.ToList(), FinishSum, UsersLogic.GetId());
                        MessageBox.Show("Оплата прошла успешно! Спасибо, что покупаете у нас!", "Информация");
                        Condition = "Collapsed";
                    }
                });
            }
        }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(() =>
                {
                    foreach (var product in InCheck.ToList())
                    {
                        DeleteAtCheck(product.Id);
                    }
                    Condition = "Collapsed";
                });
            }
        }

        private void ViewProduct(int id)
        {
            int index = GetIndexByIdProducts(id);
            aboutProductViewModelDialog = new AboutProductViewModelDialog(
                (Products[index].Name, Products[index].Price, Products[index].Weight, Products[index].Color, Products[index].Description));
            AboutProductViewDialog aboutProductViewDialog = new AboutProductViewDialog(aboutProductViewModelDialog);
            aboutProductViewDialog.ShowDialog();
        }

        private void AddToBuy(int id)
        {
            int index = GetIndexByIdProducts(id);
            InCheck.Add(Products[index]);
            FinishSum += Products[index].Price;
            RewriteCount(index, -1);
        }

        private void DeleteAtCheck(int id)
        {
            int index = GetIndexByIdInCheck(id);
            FinishSum -= InCheck[index].Price;
            InCheck.RemoveAt(index);
            RewriteCount(GetIndexByIdProducts(id), 1);
        }

        private void RewriteCount(int index, int number)
        {
            Products[index].Count += number;
        }

        private int GetIndexByIdProducts(int id)
        {
            int index = 0;
            IEnumerator<Product> iterator = Products.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (iterator.Current.Id == id)
                {
                    return index;
                }

                index++;
            }
            return -1;
        }

        private int GetIndexByIdInCheck(int id)
        {
            int index = 0;
            IEnumerator<Product> iterator = InCheck.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (iterator.Current.Id == id)
                {
                    return index;
                }

                index++;
            }
            return -1;
        }

        private void RewriteCountProducts()
        {
            productsLogic.UpdateCount((from checks in InCheck
                                      select checks.Id).ToList());
        }
    }
}