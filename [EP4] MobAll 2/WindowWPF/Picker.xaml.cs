using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using _EP4__MobAll_2.ContentManager;
using FieryLib.Models;

namespace _EP4__MobAll_2.WindowWPF
{
    /// <summary>
    /// Логика взаимодействия для Picker.xaml
    /// </summary>
    public partial class Picker : MetroWindow
    {
        public int SelectItem { get; private set; } = -1;
        public string SelectName { get; private set; } = "";
        private TypeFile type;

        public Picker(TypeFile type)
        {
            InitializeComponent();
            this.type = type;
            UpdateList();
        }

        private void UpdateList()
        {
            L_Box.Items.Clear();
            switch (type)
            {
                case TypeFile.ItemALL:
                    foreach (var item in FileManager.ItemAllLod)
                        L_Box.Items.Add(item.ItemID + " - " + item.Name);
                    break;
                case TypeFile.SkillALL:

                    break;
                case TypeFile.MobAll:
                    foreach (var npc in FileManager.MobAllLod)
                        L_Box.Items.Add(npc.NpcID + " - " + npc.Name);
                    break;
            }
        }

        private void L_Box_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int i = Convert.ToInt32(L_Box.SelectedItem.ToString().Split(new char[] { ' '})[0]);
            switch(type)
            {
                case TypeFile.ItemALL:
                    int idx = FileManager.ItemAllLod.FindIndex(p => p.ItemID.Equals(i));
                    if (idx != -1)
                    {
                        textBox.Text = FileManager.ItemAllLod[idx].Desc;
                        I_Mage.Source = Converters.IconConverter.Icon(FileManager.ItemAllLod[idx].TexID, FileManager.ItemAllLod[idx].TexRow, FileManager.ItemAllLod[idx].TexCol);
                        SelectItem = FileManager.ItemAllLod[idx].ItemID;
                    }
                    break;
                case TypeFile.MobAll:
                    int idx2 = FileManager.MobAllLod.IndexOf(FileManager.MobAllLod[L_Box.SelectedIndex]);
                    if (idx2 != -1)
                    {
                        textBox.Text = FileManager.MobAllLod[idx2].Desc;
                        SelectItem = FileManager.MobAllLod[idx2].NpcID;
                        SelectName = FileManager.MobAllLod[idx2].Name;
                    }
                    break;
            }      
        }

        private void B_Add_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void B_Cancel_Click(object sender, RoutedEventArgs e)
        {
            SelectItem = -2;
            Close();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Close();
        }

        private void T_Search_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (T_Search.Text != "")
            {
                switch (type)
                {
                    case TypeFile.ItemALL:
                        L_Box.Items.Clear();
                        foreach (var item in FileManager.ItemAllLod)
                        {
                            if (item.Name.ToLower().IndexOf(T_Search.Text.ToLower()) > -1 || item.ItemID.ToString().IndexOf(T_Search.Text) > -1)
                                L_Box.Items.Add(item.ItemID + " - " + item.Name);
                        }
                        break;
                    case TypeFile.MobAll:
                        L_Box.Items.Clear();
                        foreach (var mob in FileManager.MobAllLod)
                        {
                            if (mob.Name.ToLower().IndexOf(T_Search.Text.ToLower()) > -1 || mob.NpcID.ToString().IndexOf(T_Search.Text) > -1)
                                L_Box.Items.Add(mob.NpcID + " - " + mob.Name);
                        }
                        break;
                }
            }
            else
                UpdateList();
        }
    }
}
