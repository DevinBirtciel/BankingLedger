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
                if(!quit && userAccount != null)
                {
                    AccountMenuScreen.runAccountMenu(userAccount);
                }
                Console.WriteLine("Thanks for using our bank ledger software!");
            }

            #if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadKey();
            #endif
        }

        
    }
}
