using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Valute : Securities
{
    public string Name { get; protected set; }
    public char Symbol { get; protected set; }
    private bool isUpdatable;
    public Valute(string name, char sign, bool isUpdatable = true)
    {
        Price = UnityEngine.Random.Range(0.8f, 100f);
        this.isUpdatable = isUpdatable;
        Symbol = sign;
        Name = name;
        ParentCompany = null;
    }

    public override void UpdatePrice()
    {

        if (!isUpdatable)
        {
            Delta = 0;
            Price = 1;
            return;
        }

        Delta = UnityEngine.Random.Range(-2f, 2f);
        Price += Price * Delta / 100;
        _priceHistory.Add(Price);
    }
    public override string GetName()
    {
        return Name;
    }


}