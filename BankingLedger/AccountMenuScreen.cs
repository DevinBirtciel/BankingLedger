using System;
using System.Globalization;

namespace BankingLedger
{
    class AccountMenuScreen
    {
        public static void runAccountMenu(UserAccount userAccount)
        {
            String username = userAccount.UserInfo.Username;
            userAccount.History += username + " logged in\n";
            Console.WriteLine("You are logged in as " + username);
            while (true)
            {
                // Once logged in then display main menu (After each operation performed except 5 display menu again)
                ConsoleKeyInfo cki = grabUserAccountChoice(userAccount);
                // 1) Deposit -> print old balance, update balance, print new balance
                if(cki.Key == ConsoleKey.D1)
                {
                    Console.WriteLine("Depositing for: " + username);
                    Console.WriteLine("Please enter amount to deposit: ");
                    String amountStr = Console.ReadLine();
                    // TODO handle non double input
                    double amount = double.Parse(amountStr, CultureInfo.InvariantCulture);
                    userAccount.Balance += amount;
                    String transaction = buildTransactionStatement("Deposited ", amount, userAccount.Balance, username);
                    Console.WriteLine(transaction);
                    userAccount.History += transaction + "\n";
#if DEBUG
                    Console.WriteLine("Press enter to return to account menu...");
                    Console.ReadKey();
#endif
                }
                // 2) Withdrawal -> print old balance, update balance, print new balance
                else if (cki.Key == ConsoleKey.D2)
                {
                    Console.WriteLine("Withdrawing for: " + username);
                    Console.WriteLine("Please enter amount to withdraw: ");
                    String amountStr = Console.ReadLine();
                    double amount = double.Parse(amountStr, CultureInfo.InvariantCulture);
                    if(amount > userAccount.Balance)
                    {
                        userAccount.History += username + " was unable to withdraw " 
                            + amount.ToString("C", CultureInfo.CurrentCulture) 
                            + " due to insufficient funds of " 
                            + userAccount.Balance.ToString("C", CultureInfo.CurrentCulture);
                        Console.WriteLine("Please add more funds or withdraw a smaller amount to complete transaction.");
#if DEBUG
                        Console.WriteLine("Press enter to return to account menu...");
                        Console.ReadKey();
#endif
                        continue;
                    }
                    userAccount.Balance -= amount;
                    String transaction = buildTransactionStatement("Withdrew ", amount, userAccount.Balance, username);
                    Console.WriteLine(transaction);
                    userAccount.History += transaction + "\n";
#if DEBUG
                    Console.WriteLine("Press enter to return to account menu...");
                    Console.ReadKey();
#endif
                }
                // 3) Check Balance -> print balance
                else if (cki.Key == ConsoleKey.D3)
                {
                    Console.WriteLine("Printing Balance for: " + username);
                    Console.WriteLine(userAccount.Balance.ToString("C", CultureInfo.CurrentCulture));
#if DEBUG
                    Console.WriteLine("Press enter to return to account menu...");
                    Console.ReadKey();
#endif
                }
                // 4) Transaction History -> print history
                else if (cki.Key == ConsoleKey.D4)
                {
                    Console.WriteLine("Printing Transaction History for: " + username);
                    Console.WriteLine(userAccount.History);
#if DEBUG
                    Console.WriteLine("Press enter to return to account menu...");
                    Console.ReadKey();
#endif
                }
                // 5) Log out -> Goes back to main menu options
                else if (cki.Key == ConsoleKey.D5)
                {
                    userAccount.History += username + " logged out\n";
                    Console.WriteLine("Have a fantastic day " + username + "!");
#if DEBUG
                    Console.WriteLine("Press enter to return to main menu...");
                    Console.ReadKey();
#endif
                    break;
                }
            }
        }

        private static String buildTransactionStatement(String transactionType, double amount, double balance, String username)
        {
            String amountStr = amount.ToString("C", CultureInfo.CurrentCulture);
            String balanceStr = balance.ToString("C", CultureInfo.CurrentCulture);
            return transactionType + amountStr + " for " + username + " who now has a balance of " + balanceStr + ".";
        }

        private static ConsoleKeyInfo grabUserAccountChoice(UserAccount account)
        {
            ConsoleKeyInfo cki;
            do
            {
                Console.Clear();
                Console.WriteLine(" -= Welcome " + account.UserInfo.Username + "! =- ");
                Console.WriteLine("Deposit             : Press 1");
                Console.WriteLine("Withdrawl           : Press 2");
                Console.WriteLine("Check Balance       : Press 3");
                Console.WriteLine("Transaction History : Press 4");
                Console.WriteLine("Log Out             : Press 5");
                cki = Console.ReadKey(true);
                if (aValidKey(ref cki))
                {
                    Console.WriteLine(cki.Key.ToString() + " is an invalid selection.");
#if DEBUG
                    Console.WriteLine("Press enter to return to account menu...");
                    Console.ReadKey();
                    continue;
#endif
                }

            } while (aValidKey(ref cki));
            return cki;
        }

        private static bool aValidKey(ref ConsoleKeyInfo cki)
        {
            return cki.Key != ConsoleKey.D1 && cki.Key != ConsoleKey.D2 && cki.Key != ConsoleKey.D3 && cki.Key != ConsoleKey.D4 && cki.Key != ConsoleKey.D5;
        }
    }
}