using System;
using UnityEngine;

public class Wallet
{
    public int Balance { get; private set; }
    
    public event Action<int> OnBalanceChanged;

    public Wallet(int value)
    {
        this.Balance = value;
        Debug.Log("Баланс на старте " + this.Balance);
    }

    public void Add(int amount)
    {
        Balance += amount;
        OnBalanceChanged?.Invoke(Balance);
        Debug.Log("Баланс ваш изменился " + this.Balance);
    }

    public bool TrySpend(int amount)
    {
        if (Balance < amount)
            return false;

        Balance -= amount;
        return true;
    }
}