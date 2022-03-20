using System;
using UnityEngine;

public class Valute
{
    public float AmountOnHands{get; private set;}
    public Valute()
    {
        AmountOnHands = 1500000000f;
    }
    public void BuyWith(int amount)
    {
        if(AmountOnHands - amount >= 0f)
        AmountOnHands = AmountOnHands - amount;
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



}
