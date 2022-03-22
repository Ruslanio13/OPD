using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Securities
{
    [System.NonSerialized] public Company ParentCompany;
    public float Delta = 0;
    
    public float Price { get; protected set; }
    public float PriceInSelValue{get; protected set;}
    float averagePrice2017;
    float averagePrice2018;
    float averagePrice2019;
    float averagePrice2020;
    float averagePrice2021;
    public float AveragePrice2017 { get => averagePrice2017; }
    public float AveragePrice2018 { get => averagePrice2018; }
    public float AveragePrice2019 { get => averagePrice2019; }
    public float AveragePrice2020 { get => averagePrice2020; }
    public float AveragePrice2021 { get => averagePrice2021; }

    public virtual float GetPriceInDollars(){
       return Price / DetailedInfoManager._instance.currentValute.Price;
    }
    public List<float> _priceHistory = new List<float>();

    public Securities()
    {
        Price = 5f;
    }


    public virtual void UpdatePrice()
    {
        Delta = UnityEngine.Random.Range(-2f, 2f);
        Price += Price * Delta / 100;
        _priceHistory.Add(Price);
    }

    public virtual string GetName()
    {
        return ParentCompany.GetNameOfCompany();
    }

    public void RecalculateHistoryForValute(Valute val, Valute prevVal)
    {
        for(int i = _priceHistory.Count - 1500, j = val._priceHistory.Count - 1500; i < _priceHistory.Count; i++, j++)
        {
            _priceHistory[i] *= val._priceHistory[j] / prevVal._priceHistory[j];
        }
            Price *= val.Price/prevVal.Price;
    }
}

[System.Serializable]
public class Share : Securities
{
}
[System.Serializable]
public class Obligation : Securities
{

}
[System.Serializable]
public class Future : Securities
{

}
