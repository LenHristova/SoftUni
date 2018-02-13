using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        var accounts = new HashSet<BankAccount>();

        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            var commandArgs = input.Split();
            var command = commandArgs[0];
            var currentAccount = new BankAccount{Id = int.Parse(commandArgs[1]) };
            switch (command)
            {
                case "Create":
                    CreateAccount(accounts, currentAccount);
                    break;
                case "Deposit":
                    EnterDeposit(accounts, currentAccount, commandArgs);
                    break;
                case "Withdraw":
                    WithdrawMoney(accounts, currentAccount, commandArgs);
                    break;
                case "Print":
                    DisplayAccount(accounts, currentAccount);
                    break;
                    default:
                        continue;
            }
        }
    }

    private static void DisplayAccount(HashSet<BankAccount> accounts, BankAccount currentAccount)
    {
        if (accounts.Contains(currentAccount))
        {
            Console.WriteLine(accounts.First(acc => acc.Equals(currentAccount)));
        }
        else
        {
            Console.WriteLine("Account does not exist");
        }
    }

    private static void WithdrawMoney(HashSet<BankAccount> accounts, BankAccount currentAccount, string[] commandArgs)
    {
        var amount = int.Parse(commandArgs[2]);
        if (accounts.Contains(currentAccount))
        {
            accounts.First(acc => Equals(acc, currentAccount)).Withdraw(amount);
        }
        else
        {
            Console.WriteLine("Account does not exist");
        }
    }

    private static void EnterDeposit(HashSet<BankAccount> accounts, BankAccount currentAccount, string[] commandArgs)
    {
        var amount = int.Parse(commandArgs[2]);
        if (accounts.Contains(currentAccount))
        {
            accounts.First(acc => Equals(acc, currentAccount)).Deposit(amount);
        }
        else
        {
            Console.WriteLine("Account does not exist");
        }
    }

    private static void CreateAccount(HashSet<BankAccount> accounts, BankAccount currentAccount)
    {
        if (!accounts.Contains(currentAccount))
        {
            accounts.Add(currentAccount);
        }
        else
        {
            Console.WriteLine("Account already exists");
        }
    }
}