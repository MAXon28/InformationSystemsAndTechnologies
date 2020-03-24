using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InformationSystemsAndTechnologies.Model;

namespace InformationSystemsAndTechnologies.ViewModel.Dialog
{
    public class RewriteRightsViewModelDialog : ViewModelBase
    {
        private StaffsLogic staffsLogic;
        private int id;
        private string text;
        private string startText;

        public RewriteRightsViewModelDialog() { }

        public RewriteRightsViewModelDialog(StaffsLogic staffsLogic, int id)
        {
            this.id = id;
            this.staffsLogic = staffsLogic;
            text = StaffsLogic.GetRights(this.id);
            startText = text;
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (value ==  "1" || value == "2" || value == "3")
                {
                    text = value;
                }
            }
        }

        public ICommand SaveRights
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (startText != Text)
                    {
                        staffsLogic.UpdateRightsAsync(id, Convert.ToInt32(Text));
                    }
                });
            }
        }
    }
}