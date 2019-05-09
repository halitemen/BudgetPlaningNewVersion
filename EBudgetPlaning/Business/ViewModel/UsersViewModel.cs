using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EBudgetPlaning.Business.Model;
using EBudgetPlaning.Business.View;

namespace EBudgetPlaning.Business.VİewModel
{
    /// <summary>
    /// Kullanıcı Girişi BackEnd Classı
    /// </summary>
    public class UsersViewModel : INotifyPropertyChanged
    {
        #region Cpnstructor

        public UsersViewModel(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            _userDatabase = new UsersDBAccess();
            UserName = "admin";
            UserPass = "admin";
        }

        #endregion

        #region Members

        /// <summary>
        /// Kullanıcı Girişi penceresi
        /// </summary>
        private MainWindow MainWindow;

        /// <summary>
        /// Kullanıcılar database classı
        /// </summary>
        UsersDBAccess _userDatabase;

        /// <summary>
        /// Kullanıcılar modeli
        /// </summary>
        UsersModel User;

        /// <summary>
        /// Kullanıcı adını tutar
        /// </summary>
        private string _userName;

        /// <summary>
        /// Şifreyi tutar
        /// </summary>
        private string _passWord;

        /// <summary>
        /// Kullanıcılar listesi
        /// </summary>
        private ObservableCollection<UsersModel> _usersList;


        #endregion

        #region Properties

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string UserPass
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                OnPropertyChanged(nameof(UserPass));
            }
        }

        public ObservableCollection<UsersModel> UsersList
        {
            get
            {
                return _usersList;
            }
            set
            {
                _usersList = value;
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ICommand

        private ICommand _checkUser;

        #endregion

        #region Command

        public ICommand CheckUser
        {
            get
            {
                if (_checkUser == null)
                    _checkUser = new RelayCommand(CheckUsers);
                return _checkUser;
            }
        }

        #endregion

        #region Metods

        /// <summary>
        /// Kullanıcı databasede var mı yok mu kontrol eden metot
        /// </summary>
        private void CheckUsers()
        {
            User = new UsersModel();
            User.UserName = UserName;
            User.PassWord = UserPass;
            _userDatabase = new UsersDBAccess();
            if (_userDatabase.SelectUsers(User.UserName, User.PassWord) == true)
            {
                AnaSayfa anaSayfa = new AnaSayfa();
                anaSayfa.Show();
                MainWindow.Close();
                MainWindow.Dispose();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adi veya Şifre hatali", "HATA", MessageBoxButton.OK);
            }
        }

        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((prop)));
        }

        #endregion
    }
}
