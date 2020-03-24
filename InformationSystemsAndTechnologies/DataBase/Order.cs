namespace InformationSystemsAndTechnologies.DataBase
{
    public class Order
    {
        public int Id { get; set; }

        public string Product { get; set; }
        
        public int ProductPrice { get; set; }

        public int HistoryId { get; set; }

        public History History { get; set; }
    }
}