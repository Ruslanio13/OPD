using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Securities
{
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

    private System.Random random;

    public Securities()
    {
        price =  500f;
    }

    
    public void UpdatePrice()
    {
        _priceHistory.Add(price);
        float delta = UnityEngine.Random.Range(-2f, 2f); //Такое себе согласен
        price += price * delta / 100;
    }

}

[System.Serializable]
public class Share:Securities
{
}
[System.Serializable]
public class Obligation:Securities
{
}
[System.Serializable]
public class Future:Securities
{
}
