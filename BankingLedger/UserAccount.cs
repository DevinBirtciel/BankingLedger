
namespace BankingLedger
{
    internal class UserAccount
    {
        private double balance;
        private string history;
        private UserInfo userInfo;

        public UserAccount(UserInfo userInfo)
        {
            this.userInfo = userInfo;
            balance = 0;
            history = "New Account Created\n";
        }

        public double Balance { get => balance; set => balance = value; }
        public string History { get => history; set => history = value; }
        public UserInfo UserInfo { get => userInfo; set => userInfo = value; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}