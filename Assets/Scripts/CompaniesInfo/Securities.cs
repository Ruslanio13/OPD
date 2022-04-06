using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Securities
{
    [System.NonSerialized] public Company ParentCompany;
    public List<List<float>> TransHistory = new List<List<float>>();
    public float Delta = 0;
    public int Amount{get; private set;}
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

    public List<float> _priceHistory = new List<float>();

    public Securities()
    {
        Price = 5f;
        Amount = 0;
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
    public void SetAmount(int am)
    {
        if(am >= 0)
            Amount = am;
        else
            throw new System.Exception("Wrong Amount Of Sec");
    }

    public void AddTransaction(float spend)
    {
        List<float> tempList = new List<float>();
        tempList.Add(spend);

        for (int i = 0; i < BalanceManager._instance.Valutes.Count; i++)
        {
            tempList.Add(BalanceManager._instance.Valutes[i].Price);
        }

        TransHistory.Add(tempList);
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



