using System;

public class BankAccount
{
    private int _id;
    private decimal _balance;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public decimal Balance
    {
        get { return _balance; }
        set { _balance = value; }
    }

    public void Deposit(decimal amount)
    {
        _balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            _balance -= amount;
        }
        else
        {
            Console.WriteLine("Insufficient balance");
        }
    }

    public override string ToString()
    {
        return $"Account ID{Id}, balance {Balance:F2}";
    }

    public override bool Equals(object obj)
    {
        if (!(obj is BankAccount item))
        {
            return false;
        }

        return this.Id.Equals(item.Id);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}