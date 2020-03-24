using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using InformationSystemsAndTechnologies.Context;
using InformationSystemsAndTechnologies.DataBase;

namespace InformationSystemsAndTechnologies.Model
{
    class ProductsLogic : ViewModelBase
    {
        private static ProductsLogic productsLogic;
        private InternetMarketContext internetMarketContext;
        private List<Product> listProducts;

        private ProductsLogic() 
        {
            internetMarketContext = new InternetMarketContext();
            if (!internetMarketContext.Products.Any())
            {
                AddStartPersonsAsync();
            }
            else
            {
                LoadAsync();
            }
        }

        public List<Product> GetListProducts()
        {
            return listProducts;
        }

        public static ProductsLogic GetInstance()
        {
            if (productsLogic == null)
            {
                productsLogic = new ProductsLogic();
            }
            return productsLogic;
        }


        public void AddNewProductAsync(string name, int price, int count, double weight, string color, string description)
        {
            Product product = new Product();
            product.Name = name;
            product.Price = price;
            product.Count = count;
            product.Weight = weight;
            product.Color = color;
            product.Description = description;
            Task task = Task.Run(() => AddProductDb(product));
            task.Wait();
        }


        public async void UpdateNameAsync(int id, string name)
        {
            int index = listProducts.IndexOf(GetElementById(id));
            listProducts[index].Name = name;
            await Task.Run(() => UpdateNameDb(id, name));
        }

        public async void UpdatePriceAsync(int id, int price)
        {
            int index = listProducts.IndexOf(GetElementById(id));
            listProducts[index].Price = price;
            await Task.Run(() => UpdatePriceDb(id, price));
        }

        public async void UpdateCountAsync(int id, int count)
        {
            int index = listProducts.IndexOf(GetElementById(id));
            listProducts[index].Count = count;
            await Task.Run(() => UpdateCountDb(id, count));
        }

        public void UpdateCount(List<int> idList)
        {
            List<int> countProducts = new List<int>();
            foreach (var id in idList)
            {
                countProducts.Add(listProducts[listProducts.IndexOf(GetElementById(id))].Count);
            }
            Task.Run(() => UpdateCountDb(idList, countProducts));
        }

        public async void UpdateWeightAsync(int id, double weight)
        {
            int index = listProducts.IndexOf(GetElementById(id));
            listProducts[index].Weight = weight;
            await Task.Run(() => UpdateWeightDb(id, weight));
        }

        public async void UpdateColorAsync(int id, string color)
        {
            int index = listProducts.IndexOf(GetElementById(id));
            listProducts[index].Color = color;
            await Task.Run(() => UpdateColorDb(id, color));
        }

        public async void UpdateDescriptionAsync(int id, string description)
        {
            int index = listProducts.IndexOf(GetElementById(id));
            listProducts[index].Description = description;
            await Task.Run(() => UpdateDescriptionDb(id, description));
        }


        public async void DeleteProductAsync(int id)
        {
            listProducts.Remove(GetElementById(id));
            await Task.Run(() => DeleteProductDb(id));
        }


        private void AddProductDb(Product product)
        {
            internetMarketContext.Products.Add(product);
            internetMarketContext.SaveChanges();
            listProducts = internetMarketContext.Products.ToList();
        }


        private void UpdateNameDb(int id, string name)
        {
            Product product = internetMarketContext.Products.Find(id);
            product.Name = name;
            internetMarketContext.SaveChanges();
        }

        private void UpdatePriceDb(int id, int price)
        {
            Product product = internetMarketContext.Products.Find(id);
            product.Price = price;
            internetMarketContext.SaveChanges();
        }

        private void UpdateCountDb(int id, int count)
        {
            Product product = internetMarketContext.Products.Find(id);
            product.Count = count;
            internetMarketContext.SaveChanges();
        }

        private void UpdateCountDb(List<int> idList, List<int> countProducts)
        {
            int index = 0;
            foreach (var id in idList)
            {
                Product product = internetMarketContext.Products.Find(id);
                product.Count = countProducts[index];
                internetMarketContext.SaveChanges();
                index++;
            }
        }

        private void UpdateWeightDb(int id, double weight)
        {
            Product product = internetMarketContext.Products.Find(id);
            product.Weight = weight;
            internetMarketContext.SaveChanges();
        }

        private void UpdateColorDb(int id, string color)
        {
            Product product = internetMarketContext.Products.Find(id);
            product.Color = color;
            internetMarketContext.SaveChanges();
        }

        private void UpdateDescriptionDb(int id, string description)
        {
            Product product = internetMarketContext.Products.Find(id);
            product.Description = description;
            internetMarketContext.SaveChanges();
        }


        private void DeleteProductDb(int id)
        {
            Product product = internetMarketContext.Products.Find(id);
            internetMarketContext.Products.Remove(product);
            internetMarketContext.SaveChanges();
        }


        private Product GetElementById(int id)
        {
            return (from product in listProducts
                    where product.Id == id
                    select product).ToList()[0];
        }


        private async void LoadAsync()
        {
            await Task.Run(() => { listProducts = internetMarketContext.Products.ToList(); });
        }

        private async void AddStartPersonsAsync()
        {
            await Task.Run(() =>
            {
                internetMarketContext.Products.Add(new Product
                {
                    Name = "Смартфон Apple iPhone 11 Pro",
                    Price = 105000,
                    Count = 28,
                    Weight = 188,
                    Color = "Золотой",
                    Description = @"смартфон с iOS 13
                            поддержка двух SIM - карт(nano SIM + eSIM)
                            экран 5.8, разрешение 2436x1125
                            три камеры 12 МП / 12 МП / 12 МП,
                            автофокус
                            память 512 ГБ,
                            без слота для карт памяти
                            3G,
                            4G LTE,
                            LTE-A,
                            Wi-Fi,
                            Bluetooth,
                            NFC"
                });

                internetMarketContext.Products.Add(new Product
                {
                    Name = "Смартфон Apple iPhone 11",
                    Price = 70000,
                    Count = 28,
                    Weight = 194,
                    Color = "Белый",
                    Description = @"смартфон с iOS 13
                            поддержка двух SIM-карт (nano SIM+eSIM)
                            экран 6.1,
                            разрешение 1792x828
                            двойная камера 12 МП / 12 МП,
                            автофокус
                            память 256 ГБ,
                            без слота для карт памяти
                            3G,
                            4G LTE,
                            LTE-A,
                            Wi-Fi,
                            Bluetooth,
                            NFC"
                });

                internetMarketContext.Products.Add(new Product
                {
                    Name = "Смартфон Samsung Galaxy Note 10+",
                    Price = 70000,
                    Count = 28,
                    Weight = 196,
                    Color = "Чёрный",
                    Description = @"смартфон с Android 9.0 
                            поддержка двух SIM-карт
                            экран 6.8,
                            разрешение 3040x1440
                            четыре камеры 12 МП / 16 МП / 12 МП,
                            автофокус
                            память 512 ГБ,
                            слот для карты памяти
                            3G,
                            4G LTE,
                            LTE-A,
                            Wi-Fi,
                            Bluetooth,
                            NFC
                            объем оперативной памяти 12 ГБ
                            аккумулятор 4300 мА⋅ч"
                });

                internetMarketContext.Products.Add(new Product
                {
                    Name = "Смартфон Honor 20 Pro",
                    Price = 30000,
                    Count = 28,
                    Weight = 182,
                    Color = "Фантомный синий",
                    Description = @"смартфон с Android 9.0
                            поддержка двух SIM-карт
                            экран 6.26,
                            разрешение 2340x1080
                            четыре камеры 48 МП / 16 МП / 8 МП / 2 МП,
                            автофокус
                            память 256 ГБ,
                            без слота для карт памяти
                            3G,
                            4G LTE,
                            Wi-Fi,
                            Bluetooth,
                            NFC
                            объем оперативной памяти 8 ГБ
                            аккумулятор 4000 мА⋅ч"
                });

                internetMarketContext.Products.Add(new Product
                {
                    Name = "Смартфон Xiaomi Mi Note 10 Pro",
                    Price = 40000,
                    Count = 28,
                    Weight = 208,
                    Color = "Зелёный",
                    Description = @"смартфон с Android 9.0
                            поддержка двух SIM-карт
                            экран 6.26,
                            разрешение 2340x1080
                            четыре камеры 48 МП / 16 МП / 8 МП / 2 МП,
                            автофокус
                            память 256 ГБ,
                            без слота для карт памяти
                            3G,
                            4G LTE,
                            Wi-Fi,
                            Bluetooth,
                            NFC
                            объем оперативной памяти 8 ГБ
                            аккумулятор 4000 мА⋅ч"
                });
                internetMarketContext.SaveChanges();
                listProducts = internetMarketContext.Products.ToList();

            });
        }
    }
}
/**using (internetMarketContext = new InternetMarketContext())
            {
                if (!internetMarketContext.Products.Any())
                {
                    internetMarketContext.Products.Add(new Product
                    {
                        Name = "Смартфон Apple iPhone 11 Pro",
                        Price = 105000,
                        Count = 28,
                        Weight = 188,
                        Color = "Золотой",
                        Description = @"смартфон с iOS 13
                        поддержка двух SIM - карт(nano SIM + eSIM)
                        экран 5.8, разрешение 2436x1125
                        три камеры 12 МП / 12 МП / 12 МП,
                        автофокус
                        память 512 ГБ,
                        без слота для карт памяти
                        3G,
                        4G LTE,
                        LTE-A,
                        Wi-Fi,
                        Bluetooth,
                        NFC"
                    });

                    internetMarketContext.Products.Add(new Product
                    {
                        Name = "Смартфон Apple iPhone 11",
                        Price = 70000,
                        Count = 28,
                        Weight = 194,
                        Color = "Белый",
                        Description = @"смартфон с iOS 13
                        поддержка двух SIM-карт (nano SIM+eSIM)
                        экран 6.1,
                        разрешение 1792x828
                        двойная камера 12 МП / 12 МП,
                        автофокус
                        память 256 ГБ,
                        без слота для карт памяти
                        3G,
                        4G LTE,
                        LTE-A,
                        Wi-Fi,
                        Bluetooth,
                        NFC"
                    });

                    internetMarketContext.Products.Add(new Product
                    {
                        Name = "Смартфон Samsung Galaxy Note 10+",
                        Price = 70000,
                        Count = 28,
                        Weight = 196,
                        Color = "Чёрный",
                        Description = @"смартфон с Android 9.0 
                        поддержка двух SIM-карт
                        экран 6.8,
                        разрешение 3040x1440
                        четыре камеры 12 МП / 16 МП / 12 МП,
                        автофокус
                        память 512 ГБ,
                        слот для карты памяти
                        3G,
                        4G LTE,
                        LTE-A,
                        Wi-Fi,
                        Bluetooth,
                        NFC
                        объем оперативной памяти 12 ГБ
                        аккумулятор 4300 мА⋅ч"
                    });

                    internetMarketContext.Products.Add(new Product
                    {
                        Name = "Смартфон Honor 20 Pro",
                        Price = 30000,
                        Count = 28,
                        Weight = 182,
                        Color = "Фантомный синий",
                        Description = @"смартфон с Android 9.0
                        поддержка двух SIM-карт
                        экран 6.26,
                        разрешение 2340x1080
                        четыре камеры 48 МП / 16 МП / 8 МП / 2 МП,
                        автофокус
                        память 256 ГБ,
                        без слота для карт памяти
                        3G,
                        4G LTE,
                        Wi-Fi,
                        Bluetooth,
                        NFC
                        объем оперативной памяти 8 ГБ
                        аккумулятор 4000 мА⋅ч"
                    });

                    internetMarketContext.Products.Add(new Product
                    {
                        Name = "Смартфон Xiaomi Mi Note 10 Pro",
                        Price = 40000,
                        Count = 28,
                        Weight = 208,
                        Color = "Зелёный",
                        Description = @"смартфон с Android 9.0
                        поддержка двух SIM-карт
                        экран 6.26,
                        разрешение 2340x1080
                        четыре камеры 48 МП / 16 МП / 8 МП / 2 МП,
                        автофокус
                        память 256 ГБ,
                        без слота для карт памяти
                        3G,
                        4G LTE,
                        Wi-Fi,
                        Bluetooth,
                        NFC
                        объем оперативной памяти 8 ГБ
                        аккумулятор 4000 мА⋅ч"
                    });

                    internetMarketContext.SaveChanges();
                }*/