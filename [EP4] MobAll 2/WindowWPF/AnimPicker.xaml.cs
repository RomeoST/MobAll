using System.Windows.Controls;
using MahApps.Metro.Controls;
using _EP4__MobAll_2.D3D;

namespace _EP4__MobAll_2.WindowWPF
{
    /// <summary>
    /// Логика взаимодействия для AnimPicker.xaml
    /// </summary>
    public partial class AnimPicker : MetroWindow
    {
        public string Animation;
        public AnimPicker(string FileName, string Animation)
        {
            InitializeComponent();
            cAnimation[] animationArray = AnimReader.ReadFile(FileName).Animation;
            foreach (cAnimation animation in animationArray)
            {
                L_BoxAnim.Items.Add(animation.AnimeName);
            }
        }

        private void L_BoxAnim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Animation = L_BoxAnim.Items[L_BoxAnim.SelectedIndex].ToString();
            Close();
        }
    }
}
