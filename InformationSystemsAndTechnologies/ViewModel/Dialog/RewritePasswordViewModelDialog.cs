using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InformationSystemsAndTechnologies.Model;

namespace InformationSystemsAndTechnologies.ViewModel.Dialog
{
    public class RewritePasswordViewModelDialog : ViewModelBase
    {
        private StaffsLogic staffsLogic;
        private int id;
        private string text;
        private string startText;

        public RewritePasswordViewModelDialog() { }

        public RewritePasswordViewModelDialog(StaffsLogic staffsLogic, int id)
        {
            this.id = id;
            this.staffsLogic = staffsLogic;
            Text = StaffsLogic.GetPassword(this.id);
            startText = Text;
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (value.Length <= 19)
                {
                    text = value;
                }
            }
        }

        public ICommand SavePassword
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (startText != Text)
                    {
                        if (StaffsLogic.IsUniquePassword(Text))
                        {
                            staffsLogic.UpdatePasswordAsync(id, Text);
                        }
                        else
                        {
                            MessageBox.Show("Пароль не уникален!", "Ошибка");
                        }
                    }
                });
            }
        }
    }
}