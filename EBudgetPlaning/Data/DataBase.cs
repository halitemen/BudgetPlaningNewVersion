using System;
using System.Data.SQLite;
using System.IO;

namespace EBudgetPlaning.Data
{
    /// <summary>
    /// DataBase BackEnd Classı
    /// </summary>
    public class DataBase
    {
        #region Members

        /// <summary>
        /// Database'in localdaki uzantısını tutar.
        /// </summary>
        public static string DatabasePath { get; set; }

        /// <summary>
        /// gelirler tablosunu oluşturur.
        /// </summary>
        private string CreateTableGelir = @"CREATE TABLE IF NOT EXISTS [gelirler](
                                                    [gelir_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                    [gelir_adi] TEXT DEFAULT '',
                                                    [gelir_miktari] TEXT DEFAULT '',
                                                    [gelir_tarihi] TEXT DEFAULT '',
                                                    [gelir_kisatarih] TEXT DEFAULT '')";
        /// <summary>
        ///  giderler tablosunu oluşturur.
        /// </summary>
        private string CreateTableGider = @"CREATE TABLE IF NOT EXISTS [giderler](
                                                    [gider_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                    [gider_adi] TEXT DEFAULT '',
                                                    [gider_miktari] TEXT DEFAULT '',
                                                    [gider_tarihi] TEXT DEFAULT '',
                                                    [gider_kisatarih] TEXT DEFAULT '')";

        /// <summary>
        /// User tablosunu oluşturur.
        /// </summary>
        private string CreateTableUsers = @"CREATE TABLE IF NOT EXISTS [users] (
                                    [user_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                    [user_name] NVARCHAR(30)  DEFAULT '',
                                    [user_password] NVARCHAR(40)  DEFAULT '')";


        /// <summary>
        /// User tablosuna kayıt ekler.
        /// </summary>
        string AddTableUsers = @"INSERT INTO users(user_name,user_password)
                                 VALUES('admin','admin')";

        #endregion

        #region Methods

        /// <summary>
        /// Veri Tabanını kontrol eder. Yoksa ekler ve default kayıtları ekler.
        /// </summary>
        public void CheckDatabase()
        {
            string path = string.Format("{0}/EBudget/{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Data");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            DatabasePath = string.Format("{0}/EBudget.db", path);

            if (!File.Exists(DatabasePath))
            {
                File.Create(DatabasePath).Close();

                using (SQLiteConnection conn = GetConnection())
                {
                    using (SQLiteCommand command = new SQLiteCommand(conn))
                    {
                        command.CommandText = CreateTableUsers;
                        command.ExecuteNonQuery();

                        command.CommandText = CreateTableGelir;
                        command.ExecuteNonQuery();

                        command.CommandText = CreateTableGider;
                        command.ExecuteNonQuery();

                        command.CommandText = AddTableUsers;
                        command.ExecuteNonQuery();

                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// SQLiteConnection Bağlantısı oluşturur.
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetConnection()
        {
            if (string.IsNullOrEmpty(DatabasePath))
                CheckDatabase();

            SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0}", DatabasePath));
            con.Open();

            return con;
        }

        #endregion
    }
}
