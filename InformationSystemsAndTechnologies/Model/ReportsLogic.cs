using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using InformationSystemsAndTechnologies.Context;
using InformationSystemsAndTechnologies.DataBase;
using InformationSystemsAndTechnologies.SideClasses;

namespace InformationSystemsAndTechnologies.Model
{
    class ReportsLogic
    {
        private static ReportsLogic reportsLogic;
        private InternetMarketContext internetMarketContext;
        private List<Report> dataOfReport;

        public ReportsLogic()
        {
            internetMarketContext = new InternetMarketContext();
            if (!internetMarketContext.Reports.Any())
            {
                dataOfReport = new List<Report>();
            }
            else
            {
                dataOfReport = internetMarketContext.Reports.ToList();
            }
        }

        public int GetCountProducts()
        {
            return dataOfReport.Count;
        }

        public void AddNewProductInReport(Product product)
        {
            if (GetId(product.Id) == null)
            {
                Report report = new Report();
                report.ProductId = product.Id;
                dataOfReport.Add(report);
                Task.Run(() =>
                {
                    internetMarketContext.Reports.Add(report);
                    internetMarketContext.SaveChanges();
                });
            }
        }

        public void DeleteProductInReport(int idProduct)
        {
            for (int i = 0; i < dataOfReport.Count; i++)
            {
                if (dataOfReport[i].ProductId == idProduct)
                {
                    int id = dataOfReport[i].Id;
                    Task.Run(() => DeleteProductInReportDb(id));
                    dataOfReport.RemoveAt(i);
                    break;
                }
            }
        }

        public async void DeleteProductsAsync()
        {
            dataOfReport.Clear();
            await Task.Run(() =>
            {
                internetMarketContext.Reports.RemoveRange(internetMarketContext.Reports);
                internetMarketContext.SaveChanges();
            });
        }

        public (List<string>, List<string>) GetListsForReport()
        {
            List<string> zeroProducts = (from report in internetMarketContext.Reports
                join product in internetMarketContext.Products on report.ProductId equals product.Id
                where product.Count == 0
                select product.Name.Replace("Смартфон ", "")).ToList();
            List<string> smallCountProducts = (from report in internetMarketContext.Reports
                join product in internetMarketContext.Products on report.ProductId equals product.Id
                where product.Count > 0
                select product.Name.Replace("Смартфон ", "") + ": осталось " + product.Count + "шт.").ToList();
            return (zeroProducts, smallCountProducts);
        }

        public static ReportsLogic GetInstance()
        {
            if (reportsLogic == null)
            {
                reportsLogic = new ReportsLogic();
            }
            return reportsLogic;
        }

        private void DeleteProductInReportDb(int id)
        {
           Report report = internetMarketContext.Reports.Find(id);
           internetMarketContext.Reports.Remove(report);
           internetMarketContext.SaveChanges();
        }

        private int? GetId(int productId)
        {
            foreach (var data in dataOfReport)
            {
                if (data.ProductId == productId)
                {
                    return data.Id;
                }
            }
            return null;
        }
    }
}
/*<ContentControl
            Panel.ZIndex="1"
            Focusable="False"
            Width="722"
            Height="699"
            Content="{Binding AddNewProduct}"/>
*/