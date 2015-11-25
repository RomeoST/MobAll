using System;
using LinqToDB.DataProvider.MySql;
using _EP4__MobAll_2.DataBase.sqlTable.Data;
using _EP4__MobAll_2.ContentManager;
using MySql.Data.MySqlClient;

namespace _EP4__MobAll_2.DataBase
{
    public class DBInfoConnection
    {
        private static string _connectionName { get; set; } = "";

        public dataDB CreateMySQL
        {
            get
            {
                return new dataDB(new MySqlDataProvider(), _connectionName);
            }
        }

        public void SetConName(CONFIG_MYSQL mysql)
        {
            _connectionName = String.Format(@"server={0};Uid={2};database={1};port=3306;password={3};charset=utf8; Convert Zero DateTime=True;", mysql.IP, mysql.DATA, mysql.USER, mysql.PASS);
        }

        public bool TestConnection()
        {
            MySqlConnection coneection = new MySqlConnection(_connectionName);
            try
            {
                coneection.Open();
                coneection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
