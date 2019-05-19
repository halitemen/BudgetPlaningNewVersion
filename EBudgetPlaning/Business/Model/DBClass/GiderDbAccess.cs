using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using EBudgetPlaning.Data;

namespace EBudgetPlaning.Business.Model
{
    public class GiderDbAccess
    {
        #region Constructor

        public GiderDbAccess()
        {
            db = new DataBase();
            giderList = new ObservableCollection<GiderModel>();
            allgiderList = new ObservableCollection<GiderModel>();
            searchList = new List<string>();
        }

        #endregion

        #region Members
        List<string> searchList;
        DataBase db;
        ObservableCollection<GiderModel> giderList;
        ObservableCollection<GiderModel> allgiderList;
        string giderDBPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + @"\Data\giderler.db";
        string dateSystem = DateTime.Now.ToShortDateString();

        #endregion

        #region Metods

        public ObservableCollection<GiderModel> allGider()
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                string comText = "SELECT * FROM giderler";
                SQLiteCommand command = new SQLiteCommand(con);
                command.CommandText = comText;
                SQLiteDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    allgiderList.Add(new GiderModel
                    {
                        Id = Convert.ToInt32(dr[0]),
                        GiderAdi = dr[1].ToString(),
                        GiderMiktari = dr[2].ToString(),
                        GiderTarihi = dr[3].ToString()
                    });
                }
                con.Close();
                return allgiderList;
            }
        }

        public ObservableCollection<GiderModel> getGider()
        {
            string[] month = dateSystem.Split('.');
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("select * from giderler", con);
                SQLiteDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    string dbDate = dr[3].ToString();
                    string[] dbMonth = dbDate.Split('.');
                    if (dbMonth[1].ToString() == month[1].ToString() && dbMonth[2].ToString() == month[2].ToString())
                    {
                        giderList.Add(new GiderModel
                        {
                            Id = Convert.ToInt32(dr[0]),
                            GiderAdi = dr[1].ToString(),
                            GiderMiktari = dr[2].ToString(),
                            GiderTarihi = dr[3].ToString()
                        });
                    }
                }
                con.Close();

                return giderList;
            }
        }

        public void addGider(GiderModel gider)
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("INSERT INTO giderler(gider_adi, gider_miktari, gider_tarihi) VALUES (@gAdi, @mik, @tarih)", con);
                command.Parameters.AddWithValue("gAdi", gider.GiderAdi);
                command.Parameters.AddWithValue("mik", gider.GiderMiktari);
                command.Parameters.AddWithValue("tarih", gider.GiderTarihi);
                command.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DeleteGider(GiderModel gider)
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("Delete From giderler Where gider_id=@id", con);
                command.Parameters.AddWithValue("id", gider.Id);
                command.ExecuteNonQuery();
                con.Close();
            }

        }

        public void UpdateGider(GiderModel gider)
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("UPDATE giderler SET gider_adi=@adi, gider_miktari=@miktari, gider_tarihi=@tarihi WHERE gider_Id=@id", con);
                command.Parameters.AddWithValue("id", gider.Id);
                command.Parameters.AddWithValue("adi", gider.GiderAdi);
                command.Parameters.AddWithValue("miktari", gider.GiderMiktari);
                command.Parameters.AddWithValue("tarihi", gider.GiderTarihi);

                command.ExecuteNonQuery();
                con.Close();
            }

        }

        public List<string> getSearchGiderList()
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("select * from giderler", con);
                SQLiteDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    string dbDate = dr[3].ToString();
                    string[] dbMonth = dbDate.Split('.');
                    string val = dbMonth[1] + "." + dbMonth[2];
                    if (!searchList.Contains(val))
                    {
                        searchList.Add(val);
                    }
                }
                con.Close();
                return searchList;
            }
        }



        #endregion
    }
}
