using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Securities
{
    [System.NonSerialized] public Company ParentCompany;
    public float Delta = 0;
    
    public float Price { get; protected set; }
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

    public virtual float GetPriceInCurrentValue(){
       return Price / DetailedInfoManager._instance.currentValute.Price;
    }
    
    public List<float> _priceHistory = new List<float>();

    public Securities()
    {
        Price = 5f;
    }

    bool needToSet = true;
    int countOfChanges = 0;
    float maxPrice = 2f;
    float minPrice = -2f;

    public virtual void UpdatePrice()
    {
        

        if (ParentCompany != null && needToSet == true)
        {
            maxPrice = ParentCompany.GetMaxPriceChange();
            minPrice = ParentCompany.GetMinPriceChange();
            needToSet = false;
        }
        else if (ParentCompany == null)
        {
            maxPrice = 2f;
            minPrice = -2f;
        }

        if (maxPrice != 2f && ParentCompany != null)
        {
            if (countOfChanges < 10)
            {
                maxPrice *= 0.9f;
                minPrice *= 0.9f;
                countOfChanges += 1;
            }
            else
            {
                needToSet = true;
                countOfChanges = 0;
            }
        }

        if (maxPrice == 2f)
        {
            needToSet = true;
            Debug.Log("я хуесос");
        }

        if (ParentCompany != null)
            Debug.Log(ParentCompany.GetNameOfCompany() + " " + maxPrice + " " + minPrice);
      
        Delta = UnityEngine.Random.Range(minPrice, maxPrice);
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
