using System;
using UnityEngine;

public class Valute
{
    public float AmountOnHands{get; private set;}
    public Valute()
    {
        AmountOnHands = 0f;
    }
}


public class Dollars:Valute
{
    
}
public class Rubles:Valute
{

}
public class Euros:Valute
{

}

public class Balance
{
    public Dollars Dol;
    public Euros Eur;
    public Rubles Rub;

    public Balance()
    {
        Dol = new Dollars();
        Eur = new Euros();
        Rub = new Rubles();
    }

    public void BuyWith(Valute _valute)
    {
        throw new NotImplementedException();
    }

}
