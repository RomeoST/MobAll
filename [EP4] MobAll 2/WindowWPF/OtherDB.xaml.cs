using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _EP4__MobAll_2.ContentManager;
using MahApps.Metro.Controls;
using FieryLib.Models;
using _EP4__MobAll_2.DataBase;

namespace _EP4__MobAll_2.WindowWPF
{
    /// <summary>
    /// Логика взаимодействия для OtherDB.xaml
    /// </summary>
    public partial class OtherDB : MetroWindow
    {
        public OtherDB(DBInfoConnection conn)
        {
            InitializeComponent();
            // Привязка данных
            L_Drop.ItemsSource = ItemAllInNpc;
            L_DropAll_NPC.ItemsSource = DropAll;
            L_DropAllItems.ItemsSource = Items;

            GenerateList(); // Для Drop

            DBInfoConnection = conn;
            L_Drop.SelectedIndex = 0;
        }

        private DBInfoConnection DBInfoConnection; // Метод подключения к DB

        #region DropAll Переменные
        private ObservableCollection<DropAll> DropAll = new ObservableCollection<DropAll>();
        private ObservableCollection<DropAll.Items> Items = new ObservableCollection<DropAll.Items>();
        #endregion

        #region Drop Переменные
        private ObservableCollection<ItemInfo> ItemAllInNpc = new ObservableCollection<ItemInfo>();
        #endregion 
        #region Drop Методы
        private void GenerateList()
        {
            ItemAllInNpc.Clear();
            if (FileManager.SelectedForSQL.Count == 0) return;
            FindItemForList(FileManager.SelectedForSQL[0].a_item_0, FileManager.SelectedForSQL[0].a_item_percent_0);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_1, FileManager.SelectedForSQL[0].a_item_percent_1);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_2, FileManager.SelectedForSQL[0].a_item_percent_2);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_3, FileManager.SelectedForSQL[0].a_item_percent_3);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_4, FileManager.SelectedForSQL[0].a_item_percent_4);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_5, FileManager.SelectedForSQL[0].a_item_percent_5);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_6, FileManager.SelectedForSQL[0].a_item_percent_6);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_7, FileManager.SelectedForSQL[0].a_item_percent_7);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_8, FileManager.SelectedForSQL[0].a_item_percent_8);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_9, FileManager.SelectedForSQL[0].a_item_percent_9);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_10, FileManager.SelectedForSQL[0].a_item_percent_10);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_11, FileManager.SelectedForSQL[0].a_item_percent_11);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_12, FileManager.SelectedForSQL[0].a_item_percent_12);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_13, FileManager.SelectedForSQL[0].a_item_percent_13);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_14, FileManager.SelectedForSQL[0].a_item_percent_14);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_15, FileManager.SelectedForSQL[0].a_item_percent_15);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_16, FileManager.SelectedForSQL[0].a_item_percent_16);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_17, (int)FileManager.SelectedForSQL[0].a_item_percent_17);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_18, FileManager.SelectedForSQL[0].a_item_percent_18);
            FindItemForList(FileManager.SelectedForSQL[0].a_item_19, FileManager.SelectedForSQL[0].a_item_percent_19);
        }
        private void FindItemForList(int idx, int percent)
        {
            if (idx == -1)
            {
                ItemInfo ab = new ItemInfo();
                ab.Idx = -1;
                ab.Name = "No Name";
                ab.MaxPlus = 0;
                ab.MinPlus = 0;
                ab.Percent = 0;
                ab.TexCol = 0;
                ab.TexID = 0;
                ab.TexRow = 0;
                ab.ProbPlus = 0;
                ab.Gold = FileManager.SelectedForSQL[0].a_prize;
                ab.OY = (int)FileManager.SelectedForSQL[0].a_skill_point;
                ab.Exp = FileManager.SelectedForSQL[0].a_exp;
                ab.MaxPlus = FileManager.SelectedForSQL[0].a_maxplus;
                ab.MinPlus = FileManager.SelectedForSQL[0].a_minplus;
                ab.ProbPlus = FileManager.SelectedForSQL[0].a_probplus;
                ItemAllInNpc.Add(ab);
                return; 
            }
            ItemInfo c = new ItemInfo(); 
            ItemAllLod a = FileManager.ItemAllLod.Where(s => s.ItemID == idx).FirstOrDefault();
            c.Idx = a.ItemID;
            c.Name = a.Name;
            c.MaxPlus = FileManager.SelectedForSQL[0].a_maxplus;
            c.MinPlus = FileManager.SelectedForSQL[0].a_minplus;
            c.Percent = percent;
            c.TexCol = a.TexCol;
            c.TexID = a.TexID;
            c.TexRow = a.TexRow;
            c.ProbPlus = FileManager.SelectedForSQL[0].a_probplus;
            c.Gold = FileManager.SelectedForSQL[0].a_prize;
            c.OY = (int)FileManager.SelectedForSQL[0].a_skill_point;
            c.Exp = FileManager.SelectedForSQL[0].a_exp;
            ItemAllInNpc.Add(c);
        }
        private void B_SQL_Update_Click(object sender, RoutedEventArgs e)
        {
            if (DBInfoConnection.TestConnection())
            {
                if (FileManager.SelectedForSQL.Count == 1)
                {
                    FileManager.SelectedForSQL[0].a_maxplus = Convert.ToInt32(T_Max.Text);
                    FileManager.SelectedForSQL[0].a_minplus = Convert.ToInt32(T_Min.Text);
                    FileManager.SelectedForSQL[0].a_probplus = Convert.ToInt32(T_Chance.Text);
                    FileManager.SelectedForSQL[0].a_prize = Convert.ToInt32(T_Gold.Text);
                    FileManager.SelectedForSQL[0].a_skill_point = Convert.ToInt32(T_OY.Text);
                    FileManager.SelectedForSQL[0].a_exp = Convert.ToInt32(T_Exp.Text);

                    FileManager.SelectedForSQL[0].a_item_0 = ItemAllInNpc[0].Idx;
                    FileManager.SelectedForSQL[0].a_item_1 = ItemAllInNpc[1].Idx;
                    FileManager.SelectedForSQL[0].a_item_2 = ItemAllInNpc[2].Idx;
                    FileManager.SelectedForSQL[0].a_item_3 = ItemAllInNpc[3].Idx;
                    FileManager.SelectedForSQL[0].a_item_4 = ItemAllInNpc[4].Idx;
                    FileManager.SelectedForSQL[0].a_item_5 = ItemAllInNpc[5].Idx;
                    FileManager.SelectedForSQL[0].a_item_6 = ItemAllInNpc[6].Idx;
                    FileManager.SelectedForSQL[0].a_item_7 = ItemAllInNpc[7].Idx;
                    FileManager.SelectedForSQL[0].a_item_8 = ItemAllInNpc[8].Idx;
                    FileManager.SelectedForSQL[0].a_item_9 = ItemAllInNpc[9].Idx;
                    FileManager.SelectedForSQL[0].a_item_10 = ItemAllInNpc[10].Idx;
                    FileManager.SelectedForSQL[0].a_item_11 = ItemAllInNpc[11].Idx;
                    FileManager.SelectedForSQL[0].a_item_12 = ItemAllInNpc[12].Idx;
                    FileManager.SelectedForSQL[0].a_item_13 = ItemAllInNpc[13].Idx;
                    FileManager.SelectedForSQL[0].a_item_14 = ItemAllInNpc[14].Idx;
                    FileManager.SelectedForSQL[0].a_item_15 = ItemAllInNpc[15].Idx;
                    FileManager.SelectedForSQL[0].a_item_16 = ItemAllInNpc[16].Idx;
                    FileManager.SelectedForSQL[0].a_item_17 = ItemAllInNpc[17].Idx;
                    FileManager.SelectedForSQL[0].a_item_18 = ItemAllInNpc[18].Idx;
                    FileManager.SelectedForSQL[0].a_item_19 = ItemAllInNpc[19].Idx;

                    FileManager.SelectedForSQL[0].a_item_percent_0 = ItemAllInNpc[0].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_1 = ItemAllInNpc[1].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_2 = ItemAllInNpc[2].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_3 = ItemAllInNpc[3].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_4 = ItemAllInNpc[4].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_5 = ItemAllInNpc[5].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_6 = ItemAllInNpc[6].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_7 = ItemAllInNpc[7].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_8 = ItemAllInNpc[8].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_9 = ItemAllInNpc[9].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_10 = ItemAllInNpc[10].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_11 = ItemAllInNpc[11].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_12 = ItemAllInNpc[12].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_13 = ItemAllInNpc[13].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_14 = ItemAllInNpc[14].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_15 = ItemAllInNpc[15].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_16 = ItemAllInNpc[16].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_17 = ItemAllInNpc[17].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_18 = ItemAllInNpc[18].Percent;
                    FileManager.SelectedForSQL[0].a_item_percent_19 = ItemAllInNpc[19].Percent;

                    InquirySQL sql = new InquirySQL();

                    if (sql.UpdateExtraInfo(FileManager.SelectedForSQL[0], DBInfoConnection))
                        ShowWindow("Данные обновлены");
                    else
                        ShowWindow("Ошибка обновления");
                }
            }
        }
        #endregion
        #region DropAll Методы
        private void DropAllListNPC()
        {
            if(DBInfoConnection.TestConnection())
            {
                try
                {
                    using (var db = DBInfoConnection.CreateMySQL)
                    {
                        var query = (from p in db.t_npc_drop_all select p).ToList();

                        foreach(var npc in query)
                        {
                            if(DropAll.Where(p => p.NpcID == npc.a_npc_idx).FirstOrDefault() == null)
                            {
                                DropAll drop = new DropAll();
                                drop.NpcID = npc.a_npc_idx;
                                drop.NpcName = FileManager.MobAllLod.Where(p => p.NpcID == drop.NpcID).FirstOrDefault().Name;
                                foreach (var item in query.Where(p => p.a_npc_idx == npc.a_npc_idx).ToList())
                                {
                                    ItemAllLod lod = FileManager.ItemAllLod.Where(p => p.ItemID == item.a_item_idx).FirstOrDefault();
                                    drop.Item.Add(new DropAll.Items
                                    { idx = item.a_item_idx, percent = item.a_prob,
                                        name = lod.Name, TexCol = lod.TexCol, TexID = lod.TexID, TexRow = lod
                                    .TexRow
                                    });
                                }
                                DropAll.Add(drop);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ShowWindow("Ошибка загрузки");
                }
            }
        }
        private void DropAllAddSQL(ref List<DropAll> list)
        {
            if (!DBInfoConnection.TestConnection()) { ShowWindow("Нет подключения"); }
            foreach (var npc_list in DropAll)
            {
                if (npc_list.NpcID == list[0].NpcID)
                {
                    var npc = new DataBase.sqlTable.Data.t_npc_drop_all();
                    var db = new InquirySQL();

                    npc_list.Item = list[0].Item;
                    npc.a_npc_idx = npc_list.NpcID;

                    db.DellDropAll_NPC(npc, DBInfoConnection);
                    foreach (var item in npc_list.Item)
                    {
                        npc.a_npc_idx = npc_list.NpcID;
                        npc.a_item_idx = item.idx;
                        npc.a_prob = item.percent;
                        db.DropAll_NPC_Add(npc, DBInfoConnection);
                    }
                    break;
                }
            }
            ShowWindow("Готово");

        }
        private void DropAllAction(InquirySQL.ActionSQL action, int npcId, int item = 0, int prob = 0)
        {
            var npc = new DataBase.sqlTable.Data.t_npc_drop_all();
            npc.a_npc_idx = npcId;
            npc.a_item_idx = item;
            npc.a_prob = prob;

            InquirySQL sql = new InquirySQL();
            if (action == InquirySQL.ActionSQL.DEL)
                sql.DellDropAll_NPC(npc, DBInfoConnection);
            else if (action == InquirySQL.ActionSQL.ADD)
                sql.DropAll_NPC_Add(npc, DBInfoConnection);
        }
        #endregion

        #region Click Forms
        private void MetroWindow_Deactivated(object sender, EventArgs e)
        {
            L_Drop.IsEnabled = false;
            B_SQL_Update.IsEnabled = false;
            this.Title = "Other DB SQL | RomeoST  (Данные требуют обновления)";
        }
        private void B_Restart_Click(object sender, RoutedEventArgs e)
        {
            GenerateList();
            this.Title = "Other DB SQL | RomeoST";
            L_Drop.IsEnabled = true;
            B_SQL_Update.IsEnabled = true;
            L_Drop.SelectedIndex = 0;
        }
        #endregion
        #region Click DropAll
        // Click по NPC
        private void L_DropAll_NPC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (L_DropAll_NPC.SelectedIndex != -1)
            {
                Items = new ObservableCollection<DropAll.Items>(((DropAll)L_DropAll_NPC.SelectedItem).Item);
                L_DropAllItems.ItemsSource = Items;
                T_DropAll_ID_NPC.Text = ((DropAll)L_DropAll_NPC.SelectedItem).NpcID.ToString();
            }
        }
        // Click по Предметам
        private void L_DropAllItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (L_DropAllItems.SelectedIndex != -1)
            {
                T_DropALL_ID_Item.Text = ((DropAll.Items)L_DropAllItems.SelectedItem).idx.ToString();
                T_DropAll_Percent.Text = ((DropAll.Items)L_DropAllItems.SelectedItem).percent.ToString();
            }
        }
        // Кнопка SET DB
        private void B_SQL_Update_DropAll_Click(object sender, RoutedEventArgs e)
        {
            if(DropAll.Count > 0 && L_DropAllItems.SelectedItem!= null)
            {
                try
                {
                    var list = DropAll.Where(p => p.NpcID == ((DropAll)L_DropAll_NPC.SelectedItem).NpcID).ToList();
                    int idx = Items.IndexOf(((DropAll.Items)L_DropAllItems.SelectedItem));

                    ItemAllLod lod = FileManager.ItemAllLod.Where(p => p.ItemID == Convert.ToInt32(T_DropALL_ID_Item.Text)).FirstOrDefault();
                    if (lod == null)
                    {
                        ShowWindow("Предмет не существует");
                        return;
                    }

                    list[0].Item[idx].idx = Convert.ToInt32(T_DropALL_ID_Item.Text);
                    list[0].Item[idx].percent = Convert.ToInt32(T_DropAll_Percent.Text);
                    list[0].Item[idx].name = lod.Name;
                    list[0].Item[idx].TexCol = lod.TexCol;
                    list[0].Item[idx].TexID = lod.TexID;
                    list[0].Item[idx].TexRow = lod.TexRow;

                    DropAllAddSQL(ref list);

                    Items = new ObservableCollection<DropAll.Items>(list[0].Item);
                    L_DropAllItems.ItemsSource = Items;
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ShowWindow("Не правильные данные");
                    return;
                }
            }
        }
        // Кнопка GET DB
        private void B_SQL_Get_DropAll_Click(object sender, RoutedEventArgs e)
        {
            DropAllListNPC();
        }
        // Добавить предмет в DropAll
        private void MI_Add_Click(object sender, RoutedEventArgs e)
        {
            if(DropAll.IndexOf((DropAll)L_DropAll_NPC.SelectedItem) != -1)
            {
                DropAll drop = (DropAll)L_DropAll_NPC.SelectedItem;
                Picker pick = new Picker(TypeFile.ItemALL);
                pick.ShowDialog();
                ItemAllLod lod = FileManager.ItemAllLod.Where(p => p.ItemID == pick.SelectItem).FirstOrDefault();
                if (lod == null)
                    return;
                
                DropAll.Items item = new DropAll.Items()
                { idx = lod.ItemID, name = lod.Name, percent = 0, TexCol = lod.TexCol, TexID = lod.TexID, TexRow = lod.TexRow};
                drop.Item.Add(item);
                Items.Add(item);
                L_DropAllItems.ItemsSource = Items;
            }
        }
        // Удалить предмет в DropAll
        private void MI_Remove_Click(object sender, RoutedEventArgs e)
        {
            if(Items.IndexOf((DropAll.Items)L_DropAllItems.SelectedItem) != -1)
            {
                int idx = Items.IndexOf((DropAll.Items)L_DropAllItems.SelectedItem);
                Items.RemoveAt(idx);
                L_DropAllItems.ItemsSource = Items;
                ((DropAll)L_DropAll_NPC.SelectedItem).Item = Items.ToList();
            }
        }
        // Добавить NPC в DropAll
        private void MI_Add_npc_Click(object sender, RoutedEventArgs e)
        {
            if (!DBInfoConnection.TestConnection())
            {
                ShowWindow("Нет подключения");
                return;
            }
            DropAllListNPC(); 
            Picker pick = new Picker(TypeFile.MobAll);
            pick.ShowDialog();
            if (pick.SelectItem == -2 || pick.SelectItem == -1)
                return;

            DropAll npc = new DropAll();
            npc.NpcID = pick.SelectItem;
            npc.NpcName = pick.SelectName;
            npc.Item = new List<DropAll.Items>() { new DropAll.Items() { idx = 85, name = "Камень Богов", percent = 0, TexID = 0, TexCol = 8, TexRow = 1 } };

            DropAllAction(InquirySQL.ActionSQL.ADD, npc.NpcID, 85, 0); // Добавить в БД

            DropAll.Add(npc);
            L_DropAll_NPC.ItemsSource = DropAll;
        }
        // Удалить NPC с DropAll
        private void MI_Remove_npc_Click(object sender, RoutedEventArgs e)
        {
            int idx = DropAll.IndexOf((DropAll)L_DropAll_NPC.SelectedItem);
            if (idx != -1)
            {
                int id = DropAll[idx].NpcID;
                DropAll.RemoveAt(idx);
                L_DropAll_NPC.ItemsSource = DropAll;
                DropAllAction(InquirySQL.ActionSQL.DEL, id);
            }
        }
        // Меню выбора изменения NPC
        private void T_DropAll_ID_NPC_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (L_DropAll_NPC.SelectedItem == null) return;
            Picker pick = new Picker(TypeFile.MobAll);
            pick.ShowDialog();
            if (pick.SelectItem == -2 || pick.SelectItem == -1)
                return;
            var oldNpc = (DropAll)L_DropAll_NPC.SelectedItem;

            if (oldNpc != null)
            {
                DropAllAction(InquirySQL.ActionSQL.DEL, oldNpc.NpcID); // Удалить старый НПС

                DropAll.Where(p => p.NpcID == oldNpc.NpcID).FirstOrDefault().NpcID = pick.SelectItem; // Изменить ид старого нпс на новый
                DropAll.Where(p => p.NpcID == oldNpc.NpcID).FirstOrDefault().NpcName = pick.SelectName;

                foreach (var item in Items)
                {
                    DropAllAction(InquirySQL.ActionSQL.ADD, pick.SelectItem, item.idx, item.percent); // Запись нового нпс со всеми предметами
                }
                ShowWindow("NPC изменен на " + pick.SelectName);
            }
        }
        // Обработка клавишь по полю id npc (block)
        private void T_DropAll_ID_NPC_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (L_DropAll_NPC.SelectedItem == null) return;
            T_DropAll_ID_NPC.Text = ((DropAll)L_DropAll_NPC.SelectedItem).NpcID.ToString();
        }
        // Меню выбора Item
        private void T_DropALL_ID_Item_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (L_DropAllItems.SelectedItem == null) return;
            Picker picker = new Picker(TypeFile.ItemALL);
            picker.ShowDialog();

            if (picker.SelectItem == -2 || picker.SelectItem == -1) return;

            T_DropALL_ID_Item.Text = picker.SelectItem.ToString();
        }
        #endregion

        private void ShowWindow(string Content)
        {
            var win = new WindowInfo(WindowInfo.TypeWindow.InfoContent, this, "Предмет не существует");
            win.Show();
        }
    }

    public class ItemInfo
    {
        public int Idx { get; set; }
        public string Name { get; set; }
        public int Percent { get; set; }
        public int TexCol { get; set; }
        public int TexID { get; set; }
        public int TexRow { get; set; }
        public int MinPlus { get; set; }
        public int MaxPlus { get; set; }
        public int ProbPlus { get; set; }
        public int Gold { get; set; }
        public int OY { get; set; }
        public int Exp { get; set; }
    }
    public class DropAll
    {
        public class Items
        {
            public int idx { get; set; }
            public int percent { get; set; }
            public string name { get; set; }
            public int TexCol { get; set; }
            public int TexID { get; set; }
            public int TexRow { get; set; }
        }
        public int NpcID { get; set; }
        public string NpcName { get; set; }
        public List<Items> Item { get; set; } = new List<Items>();

    }
}
