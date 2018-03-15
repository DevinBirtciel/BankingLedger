using System;

namespace BankingLedger
{
    public class UserInfo
    {
        private String username, password;
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => base.ToString();
    }
}
