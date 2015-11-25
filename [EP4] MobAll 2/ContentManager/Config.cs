using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace _EP4__MobAll_2.ContentManager
{
    public struct FILE_NAME
    {
        public string PosFolder { get; set; }
        public string MobLod { get; set; }
        public string ShopLod { get; set; }
        public string ItemLod { get; set; }
        public string MobName { get; set; }
        public string MobNameUSA { get; set; }
        public string ItemName { get; set; }
    }

    public struct CONFIG_MYSQL
    {
        public string IP { get; set; }
        public string USER { get; set; }
        public string PASS { get; set; }
        public string DATA { get; set; }
    }

    class Config
    {
        private static bool IsLoad { get; set; }
        private static bool IsLoadSQL { get; set; }
        private static FILE_NAME PosFile { get; set; } = new FILE_NAME();
        private static CONFIG_MYSQL ConfMysql { get; set; } = new CONFIG_MYSQL();
        // Создание Config.xml пустого
        private static void Create(string fileName, bool isWrite)
        {
            try
            {
                if (!File.Exists("Config.xml"))
                {
                    XmlTextWriter textWriter = new XmlTextWriter("Config.xml", Encoding.UTF8);
                    textWriter.WriteStartDocument();
                    textWriter.WriteStartElement("head");
                    textWriter.WriteEndElement();
                    textWriter.Close();
                    CreateXML(fileName);
                }
                if(isWrite)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("Config.xml");
                    XmlNode node;
                    node = doc.DocumentElement;
                    foreach(XmlNode node1 in node.ChildNodes)
                        foreach(XmlNode node2 in node1.ChildNodes)
                            if(node2.Name == "PosFolder")
                            {
                                node2.InnerText = fileName;
                            }
                    doc.Save("Config.xml");
                }
            }
            catch (Exception ex)
            {
                // TODO : Написать вызов окна
            }

        }
        // Заполнение Config.xml по дефолту с расположение игры
        public static void CreateXML(string fileName)
        {
            try
            {
                XmlDocument document = new XmlDocument();

                document.Load("Config.xml");
                XmlNode element = document.CreateElement("FolderFile");
                document.DocumentElement.AppendChild(element);

                CreateSubElement(ref document, ref element, "PosFolder", fileName);
                CreateSubElement(ref document, ref element, "MobLod", "\\mobAll.lod");
                CreateSubElement(ref document, ref element, "ShopLod", "\\shopAll.lod");
                CreateSubElement(ref document, ref element, "MobNameLod", "strNpcName_ru.lod");
                CreateSubElement(ref document, ref element, "MobNameUSALod", "strNpcName_us.lod");
                CreateSubElement(ref document, ref element, "ItemNameLod", "strItem_ru.lod");
                CreateSubElement(ref document, ref element, "ItemLod", "\\itemAll.lod");

                document.Save("Config.xml");
            }
            catch (Exception ex)
            {
                // TODO : Написать вызов окна
            }
        }
        public static void CreateSQL(CONFIG_MYSQL mysql)
        {
            try
            {
                XmlTextWriter textWriter = new XmlTextWriter("ConfigSQL.xml", Encoding.UTF8);
                XmlDocument document = new XmlDocument();
                textWriter.WriteStartDocument();
                textWriter.WriteStartElement("head");
                textWriter.WriteEndElement();
                textWriter.Close();

                document.Load("ConfigSQL.xml");
                XmlNode element = document.CreateElement("FolderFile");
                document.DocumentElement.AppendChild(element);

                CreateSubElement(ref document,ref element, "IP",  mysql.IP);
                CreateSubElement(ref document, ref element, "USER",  mysql.USER);
                CreateSubElement(ref document, ref element, "PASS",  mysql.PASS);
                CreateSubElement(ref document, ref element, "DATA",  mysql.DATA);
                document.Save("ConfigSQL.xml");
            }
            catch (Exception ex)
            {
                // TODO: Написать окно
            }
            
        }

        public static bool GetLoadConfig()
        {
            return IsLoad;
        }
        public static bool GetLoadConfigSQL()
        {
            return IsLoadSQL;
        }
        public static FILE_NAME GetInfo()
        {
            return PosFile;
        }
        public static CONFIG_MYSQL GetMySQL()
        {
            return ConfMysql;
        }
        public static FILE_NAME GetInfo(string fileName, bool isWrite)
        {
            if (IsLoad)
                return PosFile;

            Create(fileName, isWrite);

            XmlReaderSettings setting = new XmlReaderSettings();
            setting.ConformanceLevel = ConformanceLevel.Fragment;
            setting.IgnoreWhitespace = true;
            setting.IgnoreComments = true;

            using (XmlReader reader = XmlReader.Create("Config.xml", setting))
            {
                FILE_NAME f_name = new FILE_NAME();
                reader.Read();
                reader.ReadStartElement("head");
                reader.ReadStartElement("FolderFile");
                reader.ReadStartElement("PosFolder");
                f_name.PosFolder = reader.Value;
                reader.Read();
                reader.ReadEndElement();
                reader.ReadStartElement("MobLod");
                f_name.MobLod = reader.Value;
                reader.Read();
                reader.ReadEndElement();
                reader.ReadStartElement("ShopLod");
                f_name.ShopLod = reader.Value;
                reader.Read();
                reader.ReadEndElement();
                reader.ReadStartElement("MobNameLod");
                f_name.MobName = reader.Value;
                reader.Read();
                reader.ReadEndElement();
                reader.ReadStartElement("MobNameUSALod");
                f_name.MobNameUSA = reader.Value;
                reader.Read();
                reader.ReadEndElement();
                reader.ReadStartElement("ItemNameLod");
                f_name.ItemName = reader.Value;
                reader.Read();
                reader.ReadEndElement();
                reader.ReadStartElement("ItemLod");
                f_name.ItemLod = reader.Value;
                reader.Close();
                PosFile = f_name;
                IsLoad = true;
                return f_name;
            }
            
        }
        public static CONFIG_MYSQL GetInfoSQL()
        {
            if (IsLoadSQL)
                return ConfMysql;

            XmlReaderSettings setting = new XmlReaderSettings();
            setting.ConformanceLevel = ConformanceLevel.Fragment;
            setting.IgnoreWhitespace = true;
            setting.IgnoreComments = true;
            if (!File.Exists("ConfigSQL.xml"))
            {
                return new CONFIG_MYSQL();
            }
                using (XmlReader reader = XmlReader.Create("ConfigSQL.xml", setting))
            {
                CONFIG_MYSQL f_name = new CONFIG_MYSQL();
                reader.Read();
                reader.ReadStartElement("head");
                reader.ReadStartElement("FolderFile");
                reader.ReadStartElement("IP");
                f_name.IP = reader.Value;
                reader.Read();
                reader.ReadEndElement();
                reader.ReadStartElement("USER");
                f_name.USER = reader.Value;
                reader.Read();
                reader.ReadEndElement();
                reader.ReadStartElement("PASS");
                f_name.PASS = reader.Value;
                reader.Read();
                if(f_name.PASS != "")
                    reader.ReadEndElement();
                reader.ReadStartElement("DATA");
                f_name.DATA = reader.Value;
                reader.Close();
                ConfMysql = f_name;
                IsLoadSQL = true;
                return f_name;
            }

        }

        private static void CreateSubElement(ref XmlDocument document, ref XmlNode element, string name, string name2)
        {
            XmlNode subElement1 = document.CreateElement(name);
            subElement1.InnerText = name2;
            element.AppendChild(subElement1);
        }
    }
}
