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
            Dictionary<String, UserAccount> userAccounts = new Dictionary<String, UserAccount>();
            Boolean quit = false;
            while (!quit)
            {
                UserAccount userAccount = runMainMenu(userAccounts, out quit);
                //runUserAccountMenu();
            }
            
            
            // TODO Display main menu
            // Once logged in then display main menu (After each operation performed except 5 display menu again)
            // 1) Deposit -> print old balance, update balance, print new balance
            // 2) Withdrawal -> print old balance, update balance, print new balance
            // Do we want to allow negative balances? Fees? Disallow them?
            // 3) Check Balance -> print balance
            // 4) Transaction History -> print history
            // 5) Log out -> Goes back to main menu options


            // If account created then login automatically
            // If login -> login questions -> if accepted then login else back to login or create new account

            // 
            #if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadKey();
            #endif
        }

        private static UserAccount runMainMenu(Dictionary<String, UserAccount> userAccounts, out Boolean quit)
        {
            // Main menu gives option to login or create a new account
            UserAccount userAccount = null;
            Boolean loggedIn = false;
            quit = false;
            while (!loggedIn)
            {
                ConsoleKeyInfo cki = grabUserMainMenuChoice();

                // Retrieve account information
                if(cki.Key == ConsoleKey.D1)
                {
                    userAccount = loginUser(userAccounts, out loggedIn);
                    if (!loggedIn)
                    {
                        Console.WriteLine("Incorrect username or password");
                    }
                }
                else if (cki.Key == ConsoleKey.D2)
                {
                    UserInfo userInfo = grabUserInfo(true, out loggedIn);
                    userAccount = new UserAccount(userInfo);
                    userAccounts.Add(userInfo.Username, userAccount);
                } else
                {
                    quit = true;
                    break;
                }
            }
            return userAccount;
        }

        private static UserAccount loginUser(Dictionary<string, UserAccount> userAccounts, out Boolean success)
        {
            UserInfo userInfo = grabUserInfo(false, out success);
            if (userAccounts.ContainsKey(userInfo.Username))
            {
                UserAccount account;
                success = userAccounts.TryGetValue(userInfo.Username, out account);
                return account;
            } else
            {
                success = false;
                return null;
            }
        }

        private static ConsoleKeyInfo grabUserMainMenuChoice()
        {
            Console.WriteLine(" -=Welcome to Local Bank!=- ");
            Console.WriteLine("Login              : Press 1");
            Console.WriteLine("Create New Account : Press 2");
            Console.WriteLine("Quit               : Press 3");
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
                if (cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2)
                {
                    Console.WriteLine(cki.Key.ToString() + " is an invalid selection.");
                }

            } while (cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2);
            return cki;
        }

        private static UserInfo grabUserInfo(Boolean newAccount, out Boolean success)
        {
            UserInfo userInfo = new UserInfo();
            String password = String.Empty;
            success = true;
            userInfo.Username = gatherUserName();
            password = newAccount ? gatherPasswordForNewAccount(out success) : gatherPassword();
            userInfo.Password = password;
            // TODO print congrats message for making new account
            Console.WriteLine("username entered was: " + userInfo.Username); // TODO delete in final version
            Console.WriteLine("password entered was: " + userInfo.Password); // TODO delete in final version
            return userInfo;
        }

        private static string gatherUserName()
        {
            string username;
            Console.WriteLine("Please enter username");
            username = Console.ReadLine();
            return username;
        }

        private static String gatherPasswordForNewAccount(out Boolean success)
        {
            String passwordToValidate = gatherPassword();
            String reenteredPassword = String.Empty;
            Console.WriteLine("Please enter password again to validate");
            int counter = 0;
            do
            {
                if (counter > 0) // Don't prompt to re-enter password on first attempt
                {
                    Console.WriteLine("Please re-enter password");
                }

                reenteredPassword = gatherPassword();
                if (!reenteredPassword.Equals(passwordToValidate))
                {
                    Console.WriteLine("The passwords entered don't match");
                }
                counter++;
            } while (!reenteredPassword.Equals(passwordToValidate) && counter < 3); // 3 is number of attempts to match first pw
            success = reenteredPassword.Equals(passwordToValidate);
            return passwordToValidate;
        }

        private static String gatherPassword()
        {
            String password = String.Empty;
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            while (password == null || password == String.Empty)
            {
                Console.WriteLine("Please enter password");
                do
                {
                    cki = Console.ReadKey(true);
                    password += cki.KeyChar;
                } while (cki.Key != ConsoleKey.Enter);
            }
            return password;
        }
    }
}
