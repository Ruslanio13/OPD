using System;
using UnityEngine;

[Serializable]
public class Valute
{
    public float AmountOnHands{get; private set;}
    public Valute()
    {
        AmountOnHands = 1500000000f;
    }
    public bool BuyWith(float amount)
    {
        if(AmountOnHands >= amount)
        {
            AmountOnHands -= amount;
            return true;
        }

        return false;
    }
}

[Serializable]
public class Dollars:Valute
{
    
}
[Serializable]
public class Rubles:Valute
{

}
[Serializable]
public class Euros:Valute
{

}
[Serializable]
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
