using System;
using System.Net.Mime;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InformationSystemsAndTechnologies.Model;

namespace InformationSystemsAndTechnologies.ViewModel.Dialog
{
    public class RewriteLoginViewModelDialog : ViewModelBase
    {
        private StaffsLogic staffsLogic;
        private int id;
        private string text;
        private string startText;

        public RewriteLoginViewModelDialog() { }

        public RewriteLoginViewModelDialog(StaffsLogic staffsLogic, int id)
        {
            this.id = id;
            this.staffsLogic = staffsLogic;
            Text = StaffsLogic.GetLogin(this.id);
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

        public ICommand SaveLogin
        {
            get 
            { 
                return new RelayCommand(() =>
                {
                    if (startText != Text)
                    {
                        if (StaffsLogic.IsUniqueLogin(Text))
                        {
                            staffsLogic.UpdateLoginAsync(id, Text);
                        }
                        else
                        {
                            MessageBox.Show("Данный логин занят!", "Ошибка");
                        }
                    }
                });
            }
        }
    }
}