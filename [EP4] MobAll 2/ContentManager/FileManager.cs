using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using FieryLib.IO;
using FieryLib.Models;
using System.IO;

namespace _EP4__MobAll_2.ContentManager
{
    public enum TypeFile
    {
        ItemALL,
        SkillALL,
        MobAll
    }
    class FileManager
    {
        public static ObservableCollection<MobAllLod> MobAllLod;
        public static ObservableCollection<MobAllLod> reserveMobAllLod;
        private static List<StrModel> MobNameLod;
        //private static List<StrModel> MobNameUSALod;              TODO : Для USA файла
        public static List<ShopAllLod> ShopAllLod;
        public static List<ItemAllLod> ItemAllLod;
        private static List<StrModel> ItemNameLod;

        public static List<DataBase.sqlTable.Data.t_npc> SelectedForSQL { get; set; } = new List<DataBase.sqlTable.Data.t_npc>();


        public static void OpenFile()
        {
            FILE_NAME file = Config.GetInfo("NOT", false);
            OpenFile(file);
        }

        public static void OpenFile(string fileName)
        {
            FILE_NAME file = Config.GetInfo(fileName, true);
            file.PosFolder = fileName;
            OpenFile(file);
        }

        // Главная функция инициализации
        private static void OpenFile(FILE_NAME fileName)
        {
            try
            {
                string strPathName = fileName.PosFolder + "\\..\\Local\\ru\\string\\";
                MobAllLod = new ObservableCollection<MobAllLod>();
                reserveMobAllLod = new ObservableCollection<MobAllLod>();
                ItemAllLod = new List<ItemAllLod>();
                MobNameLod = new List<StrModel>();
                ShopAllLod = new List<ShopAllLod>();
                ItemNameLod = new List<StrModel>();

                MobAllLod = LodReader.ReadLod_<MobAllLod>(fileName.PosFolder + fileName.MobLod);
                ShopAllLod = LodReader.ReadLod<ShopAllLod>(fileName.PosFolder + fileName.ShopLod);
                ItemAllLod = LodReader.ReadLod<ItemAllLod>(fileName.PosFolder + fileName.ItemLod);
                MobNameLod = StrLoader.LoadStringFile(StrFileType.NPCNAME, strPathName + fileName.MobName);
                ItemNameLod = StrLoader.LoadStringFile(StrFileType.ITEM, strPathName + fileName.ItemName);

                GenericInMobName();
                GenericInItemName();
                reserveMobAllLod = new ObservableCollection<MobAllLod>(MobAllLod);

                Optimiz();
            }
            catch (Exception ex)
            {
                // TODO : Написать окно
            }
        }
        public static void SaveFile()
        {
            if (MobAllLod.Count == 0)
                return;
            try
            {
                string strPathName = Config.GetInfo().PosFolder + "\\..\\Local\\ru\\string\\";
                LodReader.SaveLod<MobAllLod>(Config.GetInfo().PosFolder + Config.GetInfo().MobLod, MobAllLod.ToList());
                StrLoader.WriteStringFile(StrFileType.NPCNAME, strPathName + Config.GetInfo().MobName, MobNameLod);
            }
            catch (Exception ex)
            {
                // TODO : Написать окно
            }
        }
        // Оптимизация листов
        private static void Optimiz()
        {
            List<StrModel> list = new List<StrModel>();
            ObservableCollection<MobAllLod> mob_new = new ObservableCollection<FieryLib.Models.MobAllLod>();
            foreach(var npc in MobAllLod)
            {
                foreach(var name in MobNameLod)
                {
                    if(npc.NpcID == name.m_index)
                    {
                        StrModel str = new StrModel();
                        MobAllLod lod = new FieryLib.Models.MobAllLod();
                        lod = npc;
                        str = name;
                        list.Add(str);
                        mob_new.Add(lod);
                        break;
                    }
                }
            }
            MobAllLod = mob_new;
            MobNameLod = list;
        }
        // Добавить предмет в шоп магазин
        static public void AddShop(int idxitem, int idxmob)
        {
            int count = ShopAllLod[idxmob].SellItems.Length;
            int[] zo = new int[count + 1];
            for (int i = 0; i < count; i++)
                zo[i] = ShopAllLod[idxmob].SellItems[i];
            ShopAllLod[idxmob].SellItems = zo;
            ShopAllLod[idxmob].SellItems[count] = idxitem;
        }
        // Сохранить в файл шоп 
        static public void FileSaveShop()
        {
            //LodReader.SaveLod<ShopAllLod>(openedshop, shop);
            List<int> allIds = new List<int>();
            List<int> SavedIDs = new List<int>();
            for (int j = 0; j < ShopAllLod.Count<ShopAllLod>(); j++)
            {
                allIds.Add(ShopAllLod[j].Index);
            }
            allIds.Sort();
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Create(Config.GetInfo().ShopLod)))
                {
                    writer.Write(ShopAllLod.Max<ShopAllLod>((Func<ShopAllLod, int>)(p => p.Index)));
                    Predicate<ShopAllLod> match = null;
                    for (int i = 0; i < allIds.Count<int>(); i++)
                    {
                        if (match == null)
                        {
                            match = p => p.Index.Equals(allIds[i]);
                        }
                        int num2 = ShopAllLod.FindIndex(match);
                        if (num2 != -1)
                        {

                            ShopAllLod shops = ShopAllLod[num2];
                            if (SavedIDs.Contains(shops.Index))
                                continue;
                            writer.Write(shops.Index);
                            writer.Write(0);
                            writer.Write(shops.SellRate);
                            writer.Write(shops.BuyRate);
                            writer.Write(shops.SellItems.Count<int>());
                            for (int k = 0; k < shops.SellItems.Count<int>(); k++)
                            {
                                writer.Write(shops.SellItems[k]);
                            }
                            SavedIDs.Add(shops.Index);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO : Дописать окно
            }
        }
        // Присвоить имена к главному массиву
        private static void GenericInMobName()
        {
            foreach(var mob in MobAllLod)
            {
                foreach(var name in MobNameLod)
                {
                    if(mob.NpcID == name.m_index)
                    {
                        mob.Name = name.m_name;
                        mob.Desc = name.m_descs[0];
                        break;
                    }
                } // FF 2
            } // FF 1
        }
        private static void GenericInItemName()
        {
            foreach (var mob in ItemAllLod)
            {
                foreach (var name in ItemNameLod)
                {
                    if (mob.ItemID == name.m_index)
                    {
                        mob.Name = name.m_name;
                        mob.Desc = name.m_descs[0];
                        break;
                    }
                } // FF 2
            } // FF 1
        }
        // Добавление NPC
        public static void AddNpc()
        {
            MobAllLod np = new MobAllLod();
            StrModel str = new StrModel();
            //StrModel strUSA = new StrModel();
            np.NpcID = MobAllLod.Max<MobAllLod>((Func<MobAllLod, int>)(p => p.NpcID)) + 1;
            str.m_name = "Новый НПС";
            str.m_index = np.NpcID;
            str.m_descs = new string[] { "" };
            /*strUSA.m_name = "New NPC";
            strUSA.m_index = np.NpcID;
            strUSA.m_descs = new string[] { "Desc NPC" };*/
            np.Level = 1;
            np.HP = 10000;
            np.MP = 10000;
            np.Name = "Новый НПС";
            np.Flag = MobAllLod[MobAllLod.Count - 1].Flag;
            np.Flag1 = MobAllLod[MobAllLod.Count - 1].Flag1;
            np.AttackSpeed = MobAllLod[MobAllLod.Count - 1].AttackSpeed;
            np.WalkSpeed = MobAllLod[MobAllLod.Count - 1].WalkSpeed;
            np.RunSpeed = MobAllLod[MobAllLod.Count - 1].RunSpeed;
            np.Scale = MobAllLod[MobAllLod.Count - 1].Scale;
            np.AttackArea = MobAllLod[MobAllLod.Count - 1].Scale;
            np.Size = MobAllLod[MobAllLod.Count - 1].Scale;
            np.SSkillMaster = MobAllLod[MobAllLod.Count - 1].SSkillMaster;
            np.SkillMaster = MobAllLod[MobAllLod.Count - 1].SkillMaster;
            np.SkillEffects = MobAllLod[MobAllLod.Count - 1].SkillEffects;
            np.AttackType = MobAllLod[MobAllLod.Count - 1].AttackType;
            np.FireDelayCount = MobAllLod[MobAllLod.Count - 1].FireDelayCount;
            np.FireDelay0 = MobAllLod[MobAllLod.Count - 1].FireDelay0;
            np.FireDelay1 = MobAllLod[MobAllLod.Count - 1].FireDelay0;
            np.FireDelay2 = MobAllLod[MobAllLod.Count - 1].FireDelay0;
            np.FireDelay3 = MobAllLod[MobAllLod.Count - 1].FireDelay0;
            np.FireObject = MobAllLod[MobAllLod.Count - 1].FireObject;
            np.FireSpeed = MobAllLod[MobAllLod.Count - 1].FireSpeed;
            np.SkillId1 = -1;
            np.SkillLevel1 = 0;
            np.SkillId2 = -1;
            np.SkillLevel2 = 0;
            np.RvRValue = MobAllLod[MobAllLod.Count - 1].RvRValue;
            np.RvRGrade = MobAllLod[MobAllLod.Count - 1].RvRGrade;
            np.SMC = "";
            np.Idle = "";
            np.Walk = "";
            np.Damage = "";
            np.Attack = "";
            np.Die = "";
            np.Run = "";
            np.Idle2 = "";
            np.Attack2 = "";
            np.FireEffect0 = "";
            np.FireEffect1 = "";
            np.FireEffect2 = "";
            //str.make = String.Format("{0} - {1}", np.NpcID, str.Name);
            MobAllLod.Add(np);
            MobNameLod.Add(str);
            //strNPCUSA.Add(strUSA);
        }
        // Удаление NPC
        public static void RemoveNpc(int idx)
        {
            MobAllLod.RemoveAt(idx);
            MobNameLod.RemoveAt(idx);
        }
        // Копия NPC
        public static void Copy(int num)
        {
            MobAllLod np = new MobAllLod();
            StrModel str = new StrModel();
            //StrModel strUSA = new StrModel();
            np.NpcID = MobAllLod.Max<MobAllLod>((Func<MobAllLod, int>)(p => p.NpcID)) + 1;
            str.m_name = MobNameLod[num].m_name + " (Копия)";
            str.m_index = np.NpcID;
            str.m_descs = new string[] { "" };
            /*strUSA.m_name = strNPCUSA[num].m_name + " (Copy)";
            strUSA.m_index = np.NpcID;
            strUSA.m_descs = new string[] { "Desc NPC" };*/
            np.Level = MobAllLod[num].Level;
            np.HP = 10000;
            np.MP = 10000;
            np.Name = MobNameLod[num].m_name + " (Копия)";
            np.Flag = MobAllLod[num].Flag;
            np.Flag1 = MobAllLod[num].Flag1;
            np.AttackSpeed = MobAllLod[num].AttackSpeed;
            np.WalkSpeed = MobAllLod[num].WalkSpeed;
            np.RunSpeed = MobAllLod[num].RunSpeed;
            np.Scale = MobAllLod[num].Scale;
            np.AttackArea = MobAllLod[num].Scale;
            np.Size = MobAllLod[num].Scale;
            np.SSkillMaster = MobAllLod[num].SSkillMaster;
            np.SkillMaster = MobAllLod[num].SkillMaster;
            np.SkillEffects = MobAllLod[num].SkillEffects;
            np.AttackType = MobAllLod[num].AttackType;
            np.FireDelayCount = MobAllLod[num].FireDelayCount;
            np.FireDelay0 = MobAllLod[num].FireDelay0;
            np.FireDelay1 = MobAllLod[num].FireDelay0;
            np.FireDelay2 = MobAllLod[num].FireDelay0;
            np.FireDelay3 = MobAllLod[num].FireDelay0;
            np.FireObject = MobAllLod[num].FireObject;
            np.FireSpeed = MobAllLod[num].FireSpeed;
            np.SkillId1 = MobAllLod[num].SkillId1;
            np.SkillLevel1 = MobAllLod[num].SkillLevel1;
            np.SkillId2 = MobAllLod[num].SkillId2;
            np.SkillLevel2 = MobAllLod[num].SkillLevel2;
            np.RvRValue = MobAllLod[num].RvRValue;
            np.RvRGrade = MobAllLod[num].RvRGrade;
            np.SMC = MobAllLod[num].SMC;
            np.Idle = MobAllLod[num].Idle;
            np.Walk = MobAllLod[num].Walk;
            np.Damage = MobAllLod[num].Damage;
            np.Attack = MobAllLod[num].Attack;
            np.Die = MobAllLod[num].Die;
            np.Run = MobAllLod[num].Run;
            np.Idle2 = MobAllLod[num].Idle2;
            np.Attack2 = MobAllLod[num].Attack2;
            np.FireEffect0 = MobAllLod[num].FireEffect0;
            np.FireEffect1 = MobAllLod[num].FireEffect1;
            np.FireEffect2 = MobAllLod[num].FireEffect2;

            MobAllLod.Add(np);
            MobNameLod.Add(str);
            //strNPCUSA.Add(strUSA);
        }
        public static void AddInShopNpc(int idx)
        {
            ShopAllLod sho = new ShopAllLod();
            sho.Index = idx;
            sho.Name = "";
            sho.SellItems = new int[0];
            sho.BuyRate = 100;
            sho.SellRate = 40;
            ShopAllLod.Add(sho);
        }

        public static IList<T> Swap<T>( IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
    }
}
