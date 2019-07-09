using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using EBudgetPlaning.Business.Helper;
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
            searchList = new List<string>();
        }

        #endregion

        #region Members

        List<string> searchList;
        DataBase db;
        ObservableCollection<GelirModel> gelirList;
        ObservableCollection<GelirModel> allgelirList;
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
            string[] getMontYear = gelir.GelirTarihi.Split('.');
            string montAndYear = getMontYear[1] + "." + getMontYear[2];
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("INSERT INTO gelirler(gelir_adi, gelir_miktari, gelir_tarihi,gelir_kisatarih)" +
                                                          "VALUES (@gAdi, @mik, @tarih,@ay)", con);
                command.Parameters.AddWithValue("gAdi", gelir.GelirAdi);
                command.Parameters.AddWithValue("mik", gelir.GelirMiktari);
                command.Parameters.AddWithValue("tarih", gelir.GelirTarihi);
                command.Parameters.AddWithValue("ay", montAndYear);
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

                string[] month = dateSystem.Split('.');
                string monthandyear = month[1] + "." + month[2];
                SQLiteCommand command = new SQLiteCommand("UPDATE gelirler " +
                    "SET gelir_adi=@adi, gelir_miktari=@miktari, gelir_tarihi=@tarihi,gelir_kisatarih=@ay" +
                    " WHERE gelir_Id=@id", con);
                command.Parameters.AddWithValue("id", gelir.Id);
                command.Parameters.AddWithValue("adi", gelir.GelirAdi);
                command.Parameters.AddWithValue("miktari", gelir.GelirMiktari);
                command.Parameters.AddWithValue("tarihi", gelir.GelirTarihi);
                command.Parameters.AddWithValue("ay", monthandyear);
                command.ExecuteNonQuery();
                con.Close();

            }

        }

        public List<string> getSearchGelirList()
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                SQLiteCommand command = new SQLiteCommand("select * from gelirler", con);
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

        public ObservableCollection<KategoriModel> getKategori()
        {
            string[] month = dateSystem.Split('.');
            string monthandyear = month[1] + "." + month[2];

            ObservableCollection<KategoriModel> modelKategori = new ObservableCollection<KategoriModel>();
            using (SQLiteConnection con = db.GetConnection())
            {
                string sqliteCommand = @"SELECT sum(gelir_miktari), gelir_adi, gelir_tarihi FROM gelirler
                                        WHERE gelir_tarihi LIKE '%"+monthandyear+"%' GROUP BY gelir_adi";
                SQLiteCommand komut = new SQLiteCommand
                    (con);
                komut.CommandText = sqliteCommand;
                SQLiteDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    modelKategori.Add(new KategoriModel
                    {
                        Degeri = dr[0].ToString(),
                        KategoriAdi = dr[1].ToString()
                    });
                }
                con.Close();
                return modelKategori;
            }
        }

        public ObservableCollection<string> getKategoriName()
        {
            ObservableCollection<string> kategori = new ObservableCollection<string>();
            using (SQLiteConnection con = db.GetConnection())
            {
                string sqliteCommand = @"SELECT gelir_adi FROM gelirler GROUP BY gelir_adi";
                SQLiteCommand komut = new SQLiteCommand
                    (con);
                komut.CommandText = sqliteCommand;
                SQLiteDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    kategori.Add(dr[0].ToString());
                }
                con.Close();
                return kategori;
            }
        }

        public List<GrafikModel> getTotalGelir()
        {
            List<GrafikModel> grafikModel = new List<GrafikModel>();
            using (SQLiteConnection con = db.GetConnection())
            {
                string sqliteCommand = @"SELECT sum(gelir_miktari), gelir_kisatarih FROM gelirler " +
                                        "GROUP BY gelir_kisatarih";
                SQLiteCommand komut = new SQLiteCommand(con);
                komut.CommandText = sqliteCommand;
                SQLiteDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    grafikModel.Add(new GrafikModel
                    {
                        Key = dr[1].ToString(),
                        Values = Convert.ToInt32(dr[0])
                    });
                }
                con.Close();
                return grafikModel;
            }
        }


        #endregion
    }
}
