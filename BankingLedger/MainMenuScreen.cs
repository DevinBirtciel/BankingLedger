using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger
{
    class MainMenuScreen
    {
        public static UserAccount runMainMenu(Dictionary<String, UserAccount> userAccounts, out Boolean quit)
        {
            // Main menu gives option to login or create a new account
            UserAccount userAccount = null;
            Boolean loggedIn = false;
            quit = false;
            while (!loggedIn)
            {
                ConsoleKeyInfo cki = grabUserMainMenuChoice();

                // Retrieve account information
                if (cki.Key == ConsoleKey.D1)
                {
                    userAccount = loginUser(userAccounts, out loggedIn);
                    if (!loggedIn)
                    {
                        Console.WriteLine("Incorrect username or password");
                        #if DEBUG
                        Console.WriteLine("Press enter to return to main menu...");
                        Console.ReadKey();
                        #endif
                    }
                }
                else if (cki.Key == ConsoleKey.D2)
                {
                    UserInfo userInfo = grabUserInfo(true, out loggedIn);
                    if (loggedIn && !userAccounts.ContainsKey(userInfo.Username))
                    {
                        Console.WriteLine("Congratulations on creating your new account.");
#if DEBUG
                        Console.WriteLine("Press enter to finish logging in...");
                        Console.ReadKey();
#endif
                        userAccount = new UserAccount(userInfo);
                        userAccounts.Add(userInfo.Username, userAccount);
                    }
                    else if (userAccounts.ContainsKey(userInfo.Username))
                    {
                        Console.WriteLine("Username already taken.");
#if DEBUG
                        Console.WriteLine("Press enter to return to main menu");
                        Console.ReadKey();
#endif
                    }
                    else if (!loggedIn)
                    {
                        Console.WriteLine("Failed to create new account.");
#if DEBUG
                        Console.WriteLine("Press enter to return to main menu");
                        Console.ReadKey();
#endif
                    }
                    
                }
                else if(cki.Key == ConsoleKey.D3)
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
                if (!account.UserInfo.Password.Equals(userInfo.Password))
                {
                    success = false;
                    account = null;
                }
                return account;
            }
            else
            {
                success = false;
                return null;
            }
        }

        private static ConsoleKeyInfo grabUserMainMenuChoice()
        {
            ConsoleKeyInfo cki;
            do
            {
                Console.Clear();
                Console.WriteLine(" -= Welcome to Bank Ledger! =- ");
                Console.WriteLine("Login              : Press 1");
                Console.WriteLine("Create New Account : Press 2");
                Console.WriteLine("Quit               : Press 3");
                cki = Console.ReadKey(true);
                if (aValidKey(ref cki))
                {
                    Console.WriteLine(cki.Key.ToString() + " is an invalid selection.");
#if DEBUG
                    Console.WriteLine("Press enter to return to main menu...");
                    Console.ReadKey();
                    continue;
#endif
                }

            } while (aValidKey(ref cki));
            return cki;
        }

        private static bool aValidKey(ref ConsoleKeyInfo cki)
        {
            return cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2 && cki.Key != ConsoleKey.D3;
        }

        private static UserInfo grabUserInfo(Boolean newAccount, out Boolean success)
        {
            UserInfo userInfo = new UserInfo();
            String password = String.Empty;
            success = true;
            userInfo.Username = gatherUserName();
            password = newAccount ? gatherPasswordForNewAccount(out success) : gatherPassword(true);
            userInfo.Password = password;
            return userInfo;
        }

        private static string gatherUserName()
        {
            string username;
            Console.Clear();
            Console.WriteLine("Please enter username");
            username = Console.ReadLine();
            return username;
        }

        private static String gatherPasswordForNewAccount(out Boolean success)
        {
            String passwordToValidate = gatherPassword(true);
            String reenteredPassword = String.Empty;
            Console.WriteLine("Please enter password again to validate");
            int counter = 0;
            do
            {
                if (counter > 0) // Don't prompt to re-enter password on first attempt
                {
                    Console.WriteLine("Please re-enter password");
                }

                reenteredPassword = gatherPassword(false);
                if (!reenteredPassword.Equals(passwordToValidate))
                {
                    Console.WriteLine("The passwords entered don't match");
                }
                counter++;
            } while (!reenteredPassword.Equals(passwordToValidate) && counter < 3); // 3 is number of attempts to match first pw
            success = reenteredPassword.Equals(passwordToValidate);
            return passwordToValidate;
        }

        private static String gatherPassword(Boolean displayPrompt)
        {
            String password = String.Empty;
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            if (displayPrompt)
            {
                Console.Clear();
                Console.WriteLine("Please enter password");
            }
            while (password == null || password == String.Empty)
            {
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
