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
        private List<CheckForPersonal> listOfChecks;

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

        public List<CheckForPersonal> GetChecksForPersonal()
        {
            internetMarketContext = new InternetMarketContext();
            IEnumerable list = from h in internetMarketContext.Histories
                join u in internetMarketContext.Users on h.UserId equals u.Id
                select new CheckForPersonal { FullName = u.Surname + " " + u.Name, Id = h.Id, Price = h.Price };
            listOfChecks = list.Cast<CheckForPersonal>().ToList();
            return listOfChecks;
        }

        public List<CheckForPersonal> GetSearchResponse(string searchRequest)
        {
            if (searchRequest == "")
            {
                return listOfChecks;
            }
            List<CheckForPersonal> listResult = new List<CheckForPersonal>();
            for (int i = 0; i < listOfChecks.Count; i++)
            {
                if (listOfChecks[i].FullName.ToUpper().Contains(searchRequest.ToUpper()))
                {
                    listResult.Add(listOfChecks[i]);
                }
            }
            return listResult;
        }

        public List<CheckForPersonal> GetSearchResponse(int searchRequest)
        {
            List<CheckForPersonal> listResult = new List<CheckForPersonal>();
            for (int i = 0; i < listOfChecks.Count; i++)
            {
                if (listOfChecks[i].Id == searchRequest)
                { 
                    listResult.Add(listOfChecks[i]);
                    return listResult;
                }
            }
            return listResult;
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