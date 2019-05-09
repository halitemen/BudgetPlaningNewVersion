using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EBudgetPlaning.Data;

namespace EBudgetPlaning.Business.Model
{
    /// <summary>
    /// Gelirler database erişim classı
    /// </summary>
    public class GelirDbAccess
    {
        #region Constructor

        public GelirDbAccess()
        {
            gelirList = new ObservableCollection<GelirModel>();
            allgelirList = new ObservableCollection<GelirModel>();
            db = new DataBase();
        }

        #endregion

        #region Members
        DataBase db;
        ObservableCollection<GelirModel> gelirList;
        ObservableCollection<GelirModel> allgelirList;
        string gelirDBPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + @"\Data\gelirler.db";
        string dateSystem = DateTime.Now.ToShortDateString();

        #endregion

        #region Metods

        public ObservableCollection<GelirModel> AllGetGelir()
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("select * from gelirler", con);
                SQLiteDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    allgelirList.Add(new GelirModel
                    {
                        Id = Convert.ToInt32(dr[0]),
                        GelirAdi = dr[1].ToString(),
                        GelirMiktari = dr[2].ToString(),
                        GelirTarihi = dr[3].ToString()
                    });
                }
                con.Close();
                return allgelirList;
            }

        }
        public ObservableCollection<GelirModel> getGelir()
        {
            string[] month = dateSystem.Split('.');
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("select * from gelirler", con);
                SQLiteDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    string dbDate = dr[3].ToString();
                    string[] dbMonth = dbDate.Split('.');
                    if (dbMonth[1].ToString() == month[1].ToString() && dbMonth[2].ToString() == month[2].ToString())
                    {
                        gelirList.Add(new GelirModel
                        {
                            Id = Convert.ToInt32(dr[0]),
                            GelirAdi = dr[1].ToString(),
                            GelirMiktari = dr[2].ToString(),
                            GelirTarihi = dr[3].ToString()
                        });
                    }
                }
                con.Close();
                return gelirList;
            }
        }
        public void AddGelir(GelirModel gelir)
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("INSERT INTO gelirler(gelir_adi, gelir_miktari, gelir_tarihi) VALUES (@gAdi, @mik, @tarih)", con);
                command.Parameters.AddWithValue("gAdi", gelir.GelirAdi);
                command.Parameters.AddWithValue("mik", gelir.GelirMiktari);
                command.Parameters.AddWithValue("tarih", gelir.GelirTarihi);
                command.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DeleteGelir(GelirModel gelir)
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("Delete From gelirler Where gelir_id=@id", con);
                command.Parameters.AddWithValue("id", gelir.Id);
                command.ExecuteNonQuery();
                con.Close();
            }

        }

        public void UpdateGelir(GelirModel gelir)
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("UPDATE gelirler SET gelir_adi=@adi, gelir_miktari=@miktari, gelir_tarihi=@tarihi WHERE gelir_Id=@id", con);
                command.Parameters.AddWithValue("id", gelir.Id);
                command.Parameters.AddWithValue("adi", gelir.GelirAdi);
                command.Parameters.AddWithValue("miktari", gelir.GelirMiktari);
                command.Parameters.AddWithValue("tarihi", gelir.GelirTarihi);

                command.ExecuteNonQuery();
                con.Close();

            }

        }

        #endregion
    }
}
