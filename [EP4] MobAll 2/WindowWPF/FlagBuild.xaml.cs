using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MahApps.Metro.Controls;

namespace _EP4__MobAll_2.WindowWPF
{
    /// <summary>
    /// Логика взаимодействия для FlagBuild.xaml
    /// </summary>
    public partial class FlagBuild : MetroWindow
    {
        private ObservableCollection<BoolList> ListBool = new ObservableCollection<BoolList>();
        public int ItemFlag;

        public FlagBuild(int flag)
        {
            InitializeComponent();
            BuildFlag(flag);
        }

        private void BuildFlag(int flag)
        {
            for (int i = 0; i < L_Box.Items.Count; i++)
            {
                if ((flag & (((int)1) << i)) != 0)
                {
                    StackPanel a = (StackPanel)L_Box.Items[i];
                    CheckBox b = (CheckBox)a.Children[0];
                    b.IsChecked = true;
                    //checkedListBox1.SetItemChecked(i, true);
                }
                else
                {
                    StackPanel a = (StackPanel)L_Box.Items[i];
                    CheckBox b = (CheckBox)a.Children[0];
                    b.IsChecked = false;
                    //checkedListBox1.SetItemChecked(i, false);
                }
            }
            ItemFlag = flag;
            T_Build.Text = ItemFlag.ToString();
        }

        private void L_Box_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Calculate();
            T_Build.Text = ItemFlag.ToString();
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
            Close();
        }

        private void Calculate()
        {
            int num = 0;
            for (int i = 0; i < L_Box.Items.Count; i++)
            {
                StackPanel a = (StackPanel)L_Box.Items[i];
                CheckBox b = (CheckBox)a.Children[0];
                if (b.IsChecked == true)
                {
                    num += ((int)1) << i;
                }
            }
            ItemFlag = num;
        }
    }

    class BoolList
    {
        public bool isChecked { get; set; }
        public string Name { get; set; }
    }
}
