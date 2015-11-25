using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using System.Windows.Threading;
using System.ComponentModel;
using ItemAllEP4.FileSystem;

namespace ItemAllEP4
{
    /// <summary>
    /// Логика взаимодействия для ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : MetroWindow
    {
        public ProgressBar()
        {
            InitializeComponent();
        }


        public void Show(bool isActiv)
        {
            if (isActiv)
            {
                this.Show();
                this.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Action act = () =>
                {
                    this.Close();

                };
                act.Invoke();
            }
        }

        public void Set(string cont)
        {
            Label.Content = cont;
        }

    }
}
