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
            UserAccount userAccount;
            while (!quit)
            {
                userAccount = MainMenuScreen.runMainMenu(userAccounts, out quit);
                Console.WriteLine("Thanks for using our bank ledger software!");
                //runUserAccountMenu();
            }
            
            // Once logged in then display main menu (After each operation performed except 5 display menu again)
            // 1) Deposit -> print old balance, update balance, print new balance
            // 2) Withdrawal -> print old balance, update balance, print new balance
            // Do we want to allow negative balances? Fees? Disallow them?
            // 3) Check Balance -> print balance
            // 4) Transaction History -> print history
            // 5) Log out -> Goes back to main menu options

            #if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadKey();
            #endif
        }

        
    }
}
