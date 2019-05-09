using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO;
using EBudgetPlaning.Data;

namespace EBudgetPlaning.Business.Model
{
    public class UsersDBAccess
    {
        public UsersDBAccess()
        {
            _selectUser = new ObservableCollection<UsersModel>();
            db = new DataBase();
        }

        DataBase db;
        ObservableCollection<UsersModel> _selectUser;
        public bool SelectUsers(string name, string pass)
        {
            using (SQLiteConnection con = db.GetConnection())
            {
                string conText = "SELECT * FROM users WHERE user_name = @name and user_password=@pass";
                SQLiteCommand command = new SQLiteCommand(con);
                command.CommandText = conText;
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("pass", pass);
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
                con.Close();
            }          
        }
    }
}
