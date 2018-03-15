using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger
{
    class Program
    {

        /*
            -Create a new account
            -Login
            -Record a deposit
            -Record a withdrawal
            -Check balance
            -See transaction history
            -Log out
        */
        static void Main(string[] args)
        {
            // Give option to create account or login
            Console.WriteLine(" -=Welcome to Local Bank!=- ");
            Console.WriteLine("Login              : Press 1");
            Console.WriteLine("Create New Account : Press 2");
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
                if(cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2)
                {
                    Console.WriteLine(cki.Key.ToString() + " is an invalid selection.");
                }
                
            } while (cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2);

            // TODO this needs to happen in a better place with more control on when it runs
            UserInfo userInfo = getUserInfoForNewAccount();
            // TODO print congrats message for making new account
            Console.WriteLine("username entered was: " + userInfo.Username); // TODO delete in final version
            Console.WriteLine("password entered was: " + userInfo.Password); // TODO delete in final version
            #if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadKey();
            #endif
            // State = List of User Accounts
            // User Account has
            // balance
            // history
            // user info
            // username
            // password


            // If account created then login automatically
            // If login -> login questions -> if accepted then login else back to login or create new account
            // Once logged in then display main menu (After each operation performed except 5 display menu again)
            // 1) Deposit -> print old balance, update balance, print new balance
            // 2) Withdrawal -> print old balance, update balance, print new balance
            // Do we want to allow negative balances? Fees? Disallow them?
            // 3) Check Balance -> print balance
            // 4) Transaction History -> print history
            // 5) Log out -> Goes back to main menu options
            // 






        }

        private static UserInfo getUserInfoForNewAccount()
        {
            UserInfo userInfo = new UserInfo();
            String username, password = String.Empty;
            Console.WriteLine("Please enter username");
            username = Console.ReadLine();
            userInfo.Username = username;
            while (password == null || password == String.Empty)
            {
                password = gatherPasswordForNewAccount();
            }
            userInfo.Password = password;
            return userInfo;
        }

        private static String gatherPasswordForNewAccount()
        {
            String password = String.Empty;
            Console.WriteLine("Please enter password");
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
                password += cki.KeyChar;
            } while (cki.Key != ConsoleKey.Enter);
            Console.WriteLine("Please enter password again to validate");
            String validatePassword = String.Empty;
            int counter = 0;
            do
            {
                if (counter > 0) // Don't prompt to re-enter password on first attempt
                {
                    Console.WriteLine("Please re-enter password");
                }

                validatePassword = String.Empty;
                do
                {
                    cki = Console.ReadKey(true);
                    validatePassword += cki.KeyChar;
                } while (cki.Key != ConsoleKey.Enter);
                if (!validatePassword.Equals(password))
                {
                    Console.WriteLine("The passwords entered don't match");
                } else
                {
                    return password;
                }
                counter++;
            } while (!validatePassword.Equals(password) && counter < 3); // 3 is number of attempts to match first pw
            return null;
        }
    }
}
