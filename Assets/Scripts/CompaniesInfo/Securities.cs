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

    public void UpdatePrice()
    {
        System.Random rand = new System.Random();
        _priceHistory.Add(price);
        float delta = -2f + 4 * System.Convert.ToSingle(rand.NextDouble()); //Такое себе согласен
        price += price * delta / 100;
    }
    public List<float> _priceHistory = new List<float>();

    public Securities()
    {
        System.Random rand = new System.Random();
        price =  100f + 9000f*System.Convert.ToSingle(rand.NextDouble());
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
