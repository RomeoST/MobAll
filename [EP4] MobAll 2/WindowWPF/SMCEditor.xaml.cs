using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using System.IO;
using System.Windows.Documents;

namespace _EP4__MobAll_2.WindowWPF
{
    /// <summary>
    /// Логика взаимодействия для SMCEditor.xaml
    /// </summary>
    public partial class SMCEditor : MetroWindow
    {
        public SMCEditor(string fileName)
        {
            InitializeComponent();
            this.fileName = fileName;
            InitText();
        }

        private string fileName { get; set; }

        private void InitText()
        {
            if(File.Exists(fileName))
            {
                R_Box.Text = File.ReadAllText(fileName);
                // TODO : Дописать стили отображения
                /*int posStart = R_Box.
                Span span = new Span(R_Box.ContentStart, R_Box.ContentEnd);
                span.Foreground = System.Windows.Media.Brushes.Blue;*/
            }
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(fileName, R_Box.Text);
            // TODO: Написать вызов окна
        }

        private void B_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
