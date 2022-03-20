using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Securities
{
    public Company ParentCompany{get; private set;} 
    public float delta = 0;
    float price;
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

    public float GetPrice() => price;
    public List<float> _priceHistory = new List<float>();

    public Securities(Company parentComp)
    {
        price =  500f;
        ParentCompany = parentComp;
    }

    
    public void UpdatePrice()
    {
        _priceHistory.Add(price);
        delta = UnityEngine.Random.Range(-2f, 2f); 
        price += price * delta / 100;
    }
}

[System.Serializable]
public class Share : Securities
{
    public Share(Company parentComp) : base(parentComp)
    {
    }
}
[System.Serializable]
public class Obligation : Securities
{
    public Obligation(Company parentComp) : base(parentComp)
    {
    }
}
[System.Serializable]
public class Future : Securities
{
    public Future(Company parentComp) : base(parentComp)
    {
    }
}
