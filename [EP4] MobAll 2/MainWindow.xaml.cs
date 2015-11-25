using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using _EP4__MobAll_2.ContentManager;
using _EP4__MobAll_2.WindowWPF;
using FieryLib.Models;
using System.IO;

namespace _EP4__MobAll_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //WindowInfo info = new WindowInfo(WindowInfo.TypeWindow.InfoContent, this.Top, this.Left, "Обновлено");
        public MainWindow()
        {
            InitializeComponent();
            InitFind();
            L_Box.IsEnabled = false;
            tabControl.IsEnabled = false;
            DBInfoConnection = new DataBase.DBInfoConnection();
            InitConfigMySQL();
        }
        
        private bool captureChanges { get; set; }   // Разрешение на изменение значений
        private bool selectList { get; set; }       // Проверка, загрузились ли полностью новые данные с массива
        private List<object> ChangeComponent { get; set; } = new List<object>();
        private ObservableCollection<ItemAllLod> SelectedForShopAll { get; set; } = new ObservableCollection<ItemAllLod>();
        private DataBase.DBInfoConnection DBInfoConnection;

        #region ChangeValue Color
        private void ValueChanget(object sender, EventArgs e)
        {
            if(captureChanges && selectList)
            {
                if (sender is TextBox)
                    ChangeColor((sender as TextBox));
                if (sender is ComboBox)
                {
                    (sender as ComboBox).Background = System.Windows.Media.Brushes.Pink;
                }
                if (sender is DataGrid)
                    ChangeColor((sender as DataGrid));

                ChangeComponent.Add(sender);
                L_Box.IsEnabled = false;
                B_SaveThis.Visibility = Visibility.Visible;
                B_Cancel.Visibility = Visibility.Visible;
            }
            selectList = false;
        }
        private void KeyChange(object sender, EventArgs e)
        {
            if (sender is TextBox)
                selectList = true;
        }
        private void SelectionChange(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                if ((sender as ComboBox).IsDropDownOpen)
                {
                    selectList = true;
                    ValueChanget(sender, e);
                }
            }   
        }
        private void ChangeColor(Control a)
        {
            a.Background = System.Windows.Media.Brushes.Pink;
        }
        private void AllWhite()
        {
            foreach(var item in ChangeComponent)
            {
                if (item is TextBox)
                    (item as TextBox).Background = System.Windows.Media.Brushes.White;
                if (item is ComboBox)
                    (item as ComboBox).Background = System.Windows.Media.Brushes.White;
            }
        }
        #endregion
        #region ShopMake
        private void SelectedMobWithShop()
        {
            int idx = GetIdxShopMob();
            C_CheckActiveShop.IsChecked = false;
            SelectedForShopAll.Clear();
            if (idx != -1)
            {
                foreach (var i in FileManager.ShopAllLod)
                {
                    if (i.Index == FileManager.MobAllLod[idx].NpcID)
                    {
                        C_CheckActiveShop.IsChecked = true;
                        foreach (var z in i.SellItems)
                        {
                            ItemAllLod a = FileManager.ItemAllLod.Where(s => s.ItemID == z).FirstOrDefault();
                            SelectedForShopAll.Add(a);
                        }
                        break;
                    }
                }
            }
        }

        private void B_ShopAdd_Click(object sender, RoutedEventArgs e)
        {
            if (C_CheckActiveShop.IsChecked != true) return;
            Picker pick = new Picker(TypeFile.ItemALL);
            pick.ShowDialog();
            if (pick.SelectItem == -2) return;
            ItemAllLod item = new ItemAllLod()
            {
                ItemID = pick.SelectItem
            };
            SelectedForShopAll.Add(item);
            FileManager.AddShop(pick.SelectItem, ((MobAllLod)L_Box.SelectedItem).NpcID);
        }

        private void B_ShopRemove_Click(object sender, RoutedEventArgs e)
        {
            if (L_Shop.SelectedIndex == -1 && C_CheckActiveShop.IsChecked != true) return;
            int idx = GetIdxShopMob();
            if (idx != -1)
            {
                SelectedForShopAll.RemoveAt(L_Shop.SelectedIndex);
            }
        }

        private int GetIdxShopMob()
        {
            if (L_Box.SelectedIndex == -1) return -1;
            var ItemSelect = (MobAllLod)L_Box.SelectedItem;
            return FileManager.MobAllLod.IndexOf(FileManager.MobAllLod.Where(z => z.NpcID == ItemSelect.NpcID).FirstOrDefault());
        }

        private void B_ShopUp_Click(object sender, RoutedEventArgs e)
        {
            if (L_Shop.SelectedIndex - 1 == -1) return;
            int select = L_Shop.SelectedIndex - 1;
            SelectedForShopAll = (ObservableCollection<ItemAllLod>)FileManager.Swap<ItemAllLod>(SelectedForShopAll, L_Shop.SelectedIndex, L_Shop.SelectedIndex - 1);
            L_Shop.SelectedIndex = select;
        }
        private void B_ShopDown_Click(object sender, RoutedEventArgs e)
        {
            if (L_Shop.SelectedIndex + 1 >= L_Shop.Items.Count) return;
            int select = L_Shop.SelectedIndex + 1;
            SelectedForShopAll = (ObservableCollection<ItemAllLod>)FileManager.Swap<ItemAllLod>(SelectedForShopAll, L_Shop.SelectedIndex, L_Shop.SelectedIndex + 1);
            L_Shop.SelectedIndex = select;
        }
        private void B_SaveShop_Click(object sender, RoutedEventArgs e)
        {
            if (L_Shop.SelectedIndex == -1 && C_CheckActiveShop.IsChecked != true) return;
            FileManager.FileSaveShop();
            ShowWindow("Магазин сохранен");
        }
        #endregion Действия с шопом
        #region ClickButton
        // Сохранить файл
        private void B_SaveAuto_Click(object sender, RoutedEventArgs e)
        {
            FileManager.SaveFile();
            ShowWindow("Файл сохранен");
        }
        // Открыть файл с записью в конфиг
        private void B_OpenREG_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog open = new System.Windows.Forms.FolderBrowserDialog();
            open.ShowDialog();
            if (open.SelectedPath == "")
                return;
            FileManager.OpenFile(open.SelectedPath);
            FLYOUT_OpenFile.IsOpen = false;
            L_Box.ItemsSource = FileManager.MobAllLod;
            L_Shop.ItemsSource = SelectedForShopAll;
            if (L_Box.Items.Count > 0)
            {
                ShowWindow("Файлы открыты");
                L_Box.IsEnabled = true;
            }
        }
        // Открыть файл автоматом
        private void B_OpenAuto_Click(object sender, RoutedEventArgs e)
        {
            FileManager.OpenFile();
            FLYOUT_OpenFile.IsOpen = false;
            L_Box.ItemsSource = FileManager.MobAllLod;
            L_Shop.ItemsSource = SelectedForShopAll;
            if (L_Box.Items.Count > 0)
            {
                ShowWindow("Файлы открыты");
                L_Box.IsEnabled = true;
            }
        }
        // Кликает по главному листу
        private void L_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (L_Box.SelectedIndex != -1)
            {
                captureChanges = true;
                tabControl.IsEnabled = true;
                SelectedMobWithShop();  // Подгузить в лист магазин нпс, если есть
            }
        }
        // Сохранить запись
        private void B_SaveThis_Click(object sender, RoutedEventArgs e)
        {
            MakeInData(sender, true);
            ShowWindow("NPC сохранен");
        }
        // Открыть SMC редактор
        private void T_SMC_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string path = Path.GetDirectoryName((Config.GetLoadConfig() ? Config.GetInfo().PosFolder : "")) + "\\" + T_SMC.Text;
            if (File.Exists(path))
            {
                new SMCEditor(path).Show();
            }
            else
                ShowWindow("Файл не найден");
        }
        // Отменить изменения
        private void B_Cancel_Click(object sender, RoutedEventArgs e)
        {
            MakeInData(sender, false);
            ShowWindow("Изменения отменены");
        }
        // Добавить Npc
        private void MI_Add_Click(object sender, RoutedEventArgs e)
        {
            if (FileManager.MobAllLod.Count < 1) return;
            FileManager.AddNpc();
        }
        // Удаление Npc
        private void MI_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (L_Box.SelectedIndex == -1) return;
            FileManager.RemoveNpc(L_Box.SelectedIndex);
            ShowWindow("NPC удален");
        }
        // Скопировать Npc новый
        private void B_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (L_Box.SelectedIndex == -1) return;
            FileManager.Copy(L_Box.SelectedIndex);
        }
        // Открыть FlagBuild
        private void T_Flag1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FlagBuild build = new FlagBuild(Convert.ToInt32(T_Flag1.Text));
            build.ShowDialog();
            selectList = true;
            T_Flag1.Text = build.ItemFlag.ToString();
        }
        // Создания/ удаление у нпс магазина
        private void C_CheckActiveShop_Click(object sender, RoutedEventArgs e)
        {
            MobAllLod idx = (MobAllLod)L_Box.SelectedItem;
            if (C_CheckActiveShop.IsChecked == true)
            {
                FileManager.AddInShopNpc(idx.NpcID);
                ShowWindow("У NPC создан магазин");
            }
            else
            {
                int i = FileManager.MobAllLod.IndexOf(FileManager.MobAllLod.Where(z => z.NpcID == idx.NpcID).FirstOrDefault());
                if(i != -1)
                    FileManager.ShopAllLod.RemoveAt(i);
                SelectedForShopAll.Clear();
                ShowWindow("У NPC удален магазин");
            }
        }
        // Сохранить настройки MySQL
        private void B_Save_Conf_Click(object sender, RoutedEventArgs e)
        {
            CONFIG_MYSQL mysql = new CONFIG_MYSQL()
            {
                IP = T_IP.Text,
                USER = T_Login.Text,
                PASS = T_Pass.Text,
                DATA = T_Data.Text
            };
            DBInfoConnection.SetConName(mysql);
            Config.CreateSQL(mysql);
            ShowWindow("Настройки SQL сохранены");
        }
        private void B_TestCon_Click(object sender, RoutedEventArgs e)
        {
            CONFIG_MYSQL mysql = new CONFIG_MYSQL()
            {
                IP = T_IP.Text,
                USER = T_Login.Text,
                PASS = T_Pass.Text,
                DATA = T_Data.Text
            };
            DBInfoConnection.SetConName(mysql);
            bool res = DBInfoConnection.TestConnection();
            if(res)
                ShowWindow("SQL подключен удачно");
            else
                ShowWindow("SQL не подключен");
        }
        // Поиск по клику (flag)
        private void B_Find_Click(object sender, RoutedEventArgs e)
        {
            FindFlag();
        }
        // Поиск по клику клавиш
        private void T_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindText();
        }
        // Выбор анимации
        private void PickAnimation(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string str = Path.GetDirectoryName(Config.GetInfo().PosFolder + "\\").Replace("Data", "").Replace("data", "");
            if (File.Exists(str + T_SMC.Text))
            {
                string path = "";
                string[] strArray = File.ReadAllLines(str + T_SMC.Text);
                foreach (string str2 in strArray)
                {
                    if (!str2.Contains("ANIMSET"))
                    {
                        continue;
                    }
                    path = str + str2.Split(new char[] { '"' })[1];
                }
                if (File.Exists(path))
                {
                    AnimPicker picker = null;
                    picker = new AnimPicker(path, (sender as TextBox).Tag.ToString());
                    picker.ShowDialog();
                    if (picker.Animation == null) return;
                    selectList = true;
                    switch ((sender as TextBox).Tag.ToString())
                    {
                        case "Attack":
                            T_Attack.Text = picker.Animation;
                            break;

                        case "Attack2":
                            T_Attack2.Text = picker.Animation;
                            break;

                        case "Damage":
                            T_Damage.Text = picker.Animation;
                            break;

                        case "Die":
                            T_Die.Text = picker.Animation;
                            break;

                        case "Idle":
                            T_Idle.Text = picker.Animation;
                            break;

                        case "Idle2":
                            T_Idle2.Text = picker.Animation;
                            break;

                        case "Run":
                            T_Run.Text = picker.Animation;
                            break;

                        case "Walk":
                            T_Walk.Text = picker.Animation;
                            break;
                    }
                }
                else
                {
                    ShowWindow("Не найдена анимация");
                }
            }
            else
            {
                ShowWindow("SMC не найдено");
            }
        }
        #endregion
        #region Find
        private void InitFind()
        {
            C_FindFlag.Items.Add("IsShopper");
            C_FindFlag.Items.Add("CanAttack");
            C_FindFlag.Items.Add("isAgressive");
            C_FindFlag.Items.Add("IsMoveAble");
            C_FindFlag.Items.Add("IsPeaceful");
            C_FindFlag.Items.Add("IsZoneMoving");
            C_FindFlag.Items.Add("IsCastleGuard");
            C_FindFlag.Items.Add("IsRefiner");
            C_FindFlag.Items.Add("IsQuest");
            C_FindFlag.Items.Add("IsCastleTower");
            C_FindFlag.Items.Add("IsMineral");
            C_FindFlag.Items.Add("IsCrops");
            C_FindFlag.Items.Add("IsEnergy");
            C_FindFlag.Items.Add("IsEternal");
            C_FindFlag.Items.Add("IsLordSymbol");
            C_FindFlag.Items.Add("IsRemission");
            C_FindFlag.Items.Add("IsEvent");
            C_FindFlag.Items.Add("IsGuard");
            C_FindFlag.Items.Add("IsWareHouse");
            C_FindFlag.Items.Add("IsGuild");
            C_FindFlag.Items.Add("IsBoss (Serverside only)");
            C_FindFlag.Items.Add("IsRaidBoss(Serverside only)");
            C_FindFlag.Items.Add("IsResetStat");
            C_FindFlag.Items.Add("IsChangeWeapon");
            C_FindFlag.Items.Add("IsWarCastle");
            C_FindFlag.Items.Add("NPC_DISPLAY_MAP");
            C_FindFlag.Items.Add("IsCollect & IsCollectQuest");
            C_FindFlag.Items.Add("IsPartyMob");
            C_FindFlag.Items.Add("IsRareMob (Serverside only)");
            C_FindFlag.Items.Add("NPC_SUBCITY");
            C_FindFlag.Items.Add("IsChaoVill");
            C_FindFlag.Items.Add("IsHunterVill");
        }
        private void FindText()
        {
            FileManager.MobAllLod = new ObservableCollection<MobAllLod>(FileManager.reserveMobAllLod);
            l_find_num.Content = 0;
            if (T_Search.Text != "")
            {
                FileManager.MobAllLod.Clear();
                foreach (var str in FileManager.reserveMobAllLod)
                {
                    if (str.Name == null)
                        continue;
                    if (str.Name.ToLower().IndexOf(T_Search.Text.ToLower()) > -1 || str.NpcID.ToString().IndexOf(T_Search.Text) > -1)
                        FileManager.MobAllLod.Add(str);
                }
                l_find_num.Content = FileManager.MobAllLod.Count;
            }
            L_Box.ItemsSource = FileManager.MobAllLod;
        }
        private void FindFlag()
        {
            FileManager.MobAllLod = new ObservableCollection<MobAllLod>(FileManager.reserveMobAllLod);
            l_find_num.Content = 0;
            if(C_FindFlag.SelectedIndex != -1)
            {
                FileManager.MobAllLod.Clear();
                foreach (var it in FileManager.reserveMobAllLod.ToList())
                {
                    if (BuildSearch(it.Flag))
                    {
                        FileManager.MobAllLod.Add(it);
                    }
                }
                l_find_num.Content = FileManager.MobAllLod.Count;
            }
            L_Box.ItemsSource = FileManager.MobAllLod;
        }
        private bool BuildSearch(long flag2)
        {
            for (int i = 0; i < C_FindFlag.Items.Count; i++)
            {
                if (Convert.ToBoolean((long)((((long)1) << i) & flag2)))
                    if (i == C_FindFlag.SelectedIndex)
                        return true;
            }
            return false;
        }
        #endregion
        private void MakeInData(object sender, bool isSave)
        {
            BindingExpression be = null;
            try
            {
                foreach (var it in ChangeComponent)
                {
                    if (it is TextBox)
                        be = (it as TextBox).GetBindingExpression(TextBox.TextProperty);
                    else if (it is ComboBox)
                        be = (it as ComboBox).GetBindingExpression(System.Windows.Controls.Primitives.Selector.SelectedIndexProperty);
                    if (be != null)
                    {
                        if (isSave)
                            be.UpdateSource();
                        else
                            be.UpdateTarget();
                    }
                    if (be.Status == BindingStatus.UpdateSourceError)
                    {
                        ShowWindow("Ошибка в биндинге =/");
                        return;
                        // TODO: Написать окно
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO : Написать окно
            }
            
            AllWhite();
            ChangeComponent.Clear();
            L_Box.IsEnabled = true;
            B_SaveThis.Visibility = Visibility.Hidden;
            B_Cancel.Visibility = Visibility.Hidden;
        }
        private void InitConfigMySQL()
        {
            CONFIG_MYSQL mysql = Config.GetInfoSQL();
            T_IP.Text = mysql.IP;
            T_Login.Text = mysql.USER;
            T_Pass.Text = mysql.PASS;
            T_Data.Text = mysql.DATA;
        }
        private void ShowWindow(string Content)
        {
            var win = new WindowInfo(WindowInfo.TypeWindow.InfoContent, this, "Предмет не существует");
            win.Show();
        }
        #region SQL
        // Запрос таблицы t_npc
        private void B_SQL_Get_Click(object sender, RoutedEventArgs e)
        {
            if(DBInfoConnection.TestConnection())
            {
                try
                {
                    using (var db = DBInfoConnection.CreateMySQL)
                    {
                        MobAllLod lod = (MobAllLod)L_Box.SelectedItem;
                        var query = from p in db.t_npc where p.a_index == lod.NpcID select p ;
                        FileManager.SelectedForSQL = query.ToList();
                        T_SQL_AttackLevel.Text = FileManager.SelectedForSQL[0].a_attacklevel.ToString();
                        T_SQL_Attack.Text = FileManager.SelectedForSQL[0].a_attack.ToString();
                        T_SQL_AttackMagicAtt.Text = FileManager.SelectedForSQL[0].a_magic.ToString();
                        T_SQL_AttackAccuracy.Text = FileManager.SelectedForSQL[0].a_hit.ToString();
                        T_SQL_DeffenceLevel.Text = FileManager.SelectedForSQL[0].a_defenselevel.ToString();
                        T_SQL_Deffence.Text = FileManager.SelectedForSQL[0].a_defense.ToString();
                        T_SQL_DeffenceResist.Text = FileManager.SelectedForSQL[0].a_resist.ToString();
                        T_SQL_DeffenceDodge.Text = FileManager.SelectedForSQL[0].a_dodge.ToString();
                        T_SQL_DeffenceMagRes.Text = FileManager.SelectedForSQL[0].a_magicavoid.ToString();
                        T_SQL_DeffenceClassAtrib.Text = FileManager.SelectedForSQL[0].a_attribute.ToString();
                        T_SQL_str.Text = FileManager.SelectedForSQL[0].a_str.ToString();
                        T_SQL_dex.Text = FileManager.SelectedForSQL[0].a_dex.ToString();
                        T_SQL_int.Text = FileManager.SelectedForSQL[0].a_int.ToString();
                        T_SQL_con.Text = FileManager.SelectedForSQL[0].a_con.ToString();
                        C_AI_Type.SelectedIndex = FileManager.SelectedForSQL[0].a_aitype;
                        T_SQL_AI_Flag.Text = FileManager.SelectedForSQL[0].a_aiflag.ToString();
                        T_SQL_AI_LFlag.Text = FileManager.SelectedForSQL[0].a_aileader_flag.ToString();
                        T_SQL_AI_SumHP.Text = FileManager.SelectedForSQL[0].a_ai_summonHp.ToString();
                        T_SQL_AI_LIDX.Text = FileManager.SelectedForSQL[0].a_aileader_idx.ToString();
                        T_SQL_AI_LCount.Text = FileManager.SelectedForSQL[0].a_aileader_count.ToString();
                        T_SQL_SkillID1.Text = FileManager.SelectedForSQL[0].a_skill0.ToString().Split(new char[] { ' '})[0] != "" ? FileManager.SelectedForSQL[0].a_skill0.ToString().Split(new char[] { ' ' })[0] : "-1";
                        T_SQL_SkillID2.Text = FileManager.SelectedForSQL[0].a_skill1.ToString().Split(new char[] { ' ' })[0] != "" ? FileManager.SelectedForSQL[0].a_skill1.ToString().Split(new char[] { ' ' })[0] : "-1";
                        T_SQL_SkillID3.Text = FileManager.SelectedForSQL[0].a_skill2.ToString().Split(new char[] { ' ' })[0] != "" ? FileManager.SelectedForSQL[0].a_skill2.ToString().Split(new char[] { ' ' })[0] : "-1";
                        T_SQL_SkillID4.Text = FileManager.SelectedForSQL[0].a_skill3.ToString().Split(new char[] { ' ' })[0] != "" ? FileManager.SelectedForSQL[0].a_skill3.ToString().Split(new char[] { ' ' })[0] : "-1";
                        T_SQL_SkillLvl1.Text = FileManager.SelectedForSQL[0].a_skill0.ToString().Split(new char[] { ' ' })[1] != "" ? FileManager.SelectedForSQL[0].a_skill0.ToString().Split(new char[] { ' ' })[1] : "0";
                        T_SQL_SkillLvl2.Text = FileManager.SelectedForSQL[0].a_skill1.ToString().Split(new char[] { ' ' })[1] != "" ? FileManager.SelectedForSQL[0].a_skill1.ToString().Split(new char[] { ' ' })[1] : "0";
                        T_SQL_SkillLvl3.Text = FileManager.SelectedForSQL[0].a_skill2.ToString().Split(new char[] { ' ' })[1] != "" ? FileManager.SelectedForSQL[0].a_skill2.ToString().Split(new char[] { ' ' })[1] : "0";
                        T_SQL_SkillLvl4.Text = FileManager.SelectedForSQL[0].a_skill3.ToString().Split(new char[] { ' ' })[1] != "" ? FileManager.SelectedForSQL[0].a_skill3.ToString().Split(new char[] { ' ' })[1] : "0";
                        T_SQL_SkillChance1.Text = FileManager.SelectedForSQL[0].a_skill0.ToString().Split(new char[] { ' ' })[2] != "" ? FileManager.SelectedForSQL[0].a_skill0.ToString().Split(new char[] { ' ' })[2] : "0";
                        T_SQL_SkillChance2.Text = FileManager.SelectedForSQL[0].a_skill1.ToString().Split(new char[] { ' ' })[2] != "" ? FileManager.SelectedForSQL[0].a_skill1.ToString().Split(new char[] { ' ' })[2] : "0";
                        T_SQL_SkillChance3.Text = FileManager.SelectedForSQL[0].a_skill2.ToString().Split(new char[] { ' ' })[2] != "" ? FileManager.SelectedForSQL[0].a_skill2.ToString().Split(new char[] { ' ' })[2] : "0";
                        T_SQL_SkillChance4.Text = FileManager.SelectedForSQL[0].a_skill3.ToString().Split(new char[] { ' ' })[2] != "" ? FileManager.SelectedForSQL[0].a_skill3.ToString().Split(new char[] { ' ' })[2] : "0";
                        T_SQL_RecHP.Text = FileManager.SelectedForSQL[0].a_recover_hp.ToString();
                        T_SQL_RecMP.Text = FileManager.SelectedForSQL[0].a_recover_mp.ToString();
                        T_SQL_MArea.Text = FileManager.SelectedForSQL[0].a_move_area.ToString();
                        T_SQL_AArea.Text = FileManager.SelectedForSQL[0].a_attack_area.ToString();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        // Запись в таблицу
        private void B_SQL_SET_Click(object sender, RoutedEventArgs e)
        {
            if(DBInfoConnection.TestConnection())
            {
                if(FileManager.SelectedForSQL.Count == 1 && L_Box.SelectedIndex != -1)
                {
                    DataBase.InquirySQL sql = new DataBase.InquirySQL();
                    sql.UpdateExtraInfo(FileManager.SelectedForSQL[0], DBInfoConnection);
                    ShowWindow("SQL обновление завершено");
                }
            }
        }
        // Добавить NPC в таблицу
        private void MI_SQL_Ins_Click(object sender, RoutedEventArgs e)
        {
            if(DBInfoConnection.TestConnection())
            {
                if(L_Box.SelectedIndex != -1)
                {
                    var mob = new DataBase.sqlTable.Data.t_npc(); 
                    DataBase.InquirySQL sql = new DataBase.InquirySQL();
                    sql.GenerateListForTable(ref mob, (MobAllLod)L_Box.SelectedItem);
                    sql.AddThis(mob, DBInfoConnection);
                    ShowWindow("SQL добавление завершено");
                }
            }
        }
        // Обновляет NPC в таблицу
        private void MI_SQL_Update_Click(object sender, RoutedEventArgs e)
        {
            if (DBInfoConnection.TestConnection())
            {
                if (L_Box.SelectedIndex != -1)
                {
                    var mob = new DataBase.sqlTable.Data.t_npc();
                    DataBase.InquirySQL sql = new DataBase.InquirySQL();
                    sql.GenerateListForTable(ref mob, (MobAllLod)L_Box.SelectedItem);
                    sql.UpdateThis(mob, DBInfoConnection);
                    ShowWindow("SQL обновление завершено");
                }
            }
        }
        // Открыть окно с дополнительными настройками SQL
        private void B_SQL_Inform_Click(object sender, RoutedEventArgs e)
        {
            OtherDB db = new OtherDB(DBInfoConnection);
            db.Show();
        }
        // Открыть окно действий ОтДо
        private void MI_SQL_FromTo_Click(object sender, RoutedEventArgs e)
        {
            if(L_Box.SelectedIndex != -1)
                new SQLFromTo(DBInfoConnection).Show();
        }
        #endregion
    }
}
