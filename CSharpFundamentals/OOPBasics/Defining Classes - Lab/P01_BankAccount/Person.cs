using System.Collections.Generic;
using System.Linq;

public class Person
{
    private string _name;
    private int _age;
    private List<BankAccount> _accounts;

    public Person(string name, int age) : this(name, age, new List<BankAccount>())
    {
    }

    public Person(string name, int age, List<BankAccount> accounts) 
    {
        _name = name;
        _age = age;
        _accounts = accounts;
    }

    public decimal GetBalance()
    {
        return _accounts.Select(a => a.Balance).Sum();
    }
}