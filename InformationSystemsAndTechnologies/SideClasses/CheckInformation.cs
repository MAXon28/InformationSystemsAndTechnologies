using System;
using GalaSoft.MvvmLight;

namespace InformationSystemsAndTechnologies.SideClasses
{
    public class CheckInformation : ViewModelBase
    {
        public string Product { get; set; }

        public int ProductPrice { get; set; }

        public static DateTime DateBuy { get; set; }

        public static int FinishSum { get; set; }
    }
}