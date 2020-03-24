using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InformationSystemsAndTechnologies.Context;
using InformationSystemsAndTechnologies.DataBase;
using InformationSystemsAndTechnologies.SideClasses;

namespace InformationSystemsAndTechnologies.Model
{
    public class HistoriesLogic
    {
        private static HistoriesLogic historiesLogic;
        private InternetMarketContext internetMarketContext;

        public HistoriesLogic() { }

        public List<Check> GetChecks(int userId)
        {
            internetMarketContext = new InternetMarketContext();
            IEnumerable list = from h in internetMarketContext.Histories
                join u in internetMarketContext.Users on h.UserId equals u.Id
                where u.Id == userId
                select new Check {Id = h.Id, Price = h.Price};
            return list.Cast<Check>().ToList();
        }

        public List<CheckInformation> GetCheckInformation(int historyId)
        {
            IEnumerable list = from o in internetMarketContext.Orders
                join h in internetMarketContext.Histories on o.HistoryId equals h.Id
                where h.Id == historyId
                select new CheckInformation {Product = o.Product, ProductPrice = o.ProductPrice};
            CheckInformation.DateBuy = (from h in internetMarketContext.Histories
                where h.Id == historyId
                select h.Date).ToList()[0];
            CheckInformation.FinishSum = (from h in internetMarketContext.Histories
                where h.Id == historyId
                select h.Price).ToList()[0];
            return list.Cast<CheckInformation>().ToList();
        }

        public void AddNewOrder(List<Product> products, int finishSum, int userId)
        {
            History history = new History();
            history.Date = DateTime.Now;
            history.Price = finishSum;
            history.UserId = userId;
            internetMarketContext.Histories.Add(history);

            foreach (var product in products)
            {
                Order order = new Order();
                order.Product = product.Name;
                order.ProductPrice = product.Price;
                order.History = history;
                internetMarketContext.Orders.Add(order);
            }

            internetMarketContext.SaveChanges();
        }

        public static HistoriesLogic GetInstance()
        {
            if (historiesLogic == null)
            {
                historiesLogic = new HistoriesLogic();
            }
            return historiesLogic;
        }
    }
}