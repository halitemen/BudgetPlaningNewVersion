namespace EBudgetPlaning.Business.Model
{
    /// <summary>
    /// User modeli
    /// </summary>
    public class UsersModel
    {
        private int _id;
        private string _userName;
        private string _password;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string PassWord
        {
            get { return _password; }
            set { _password = value; }
        }
     
    }
}
