using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Securities
{
    [System.NonSerialized] public Company ParentCompany;
    public List<List<float>> TransHistory = new List<List<float>>();
    public float Delta = 0;
    public int AmountInPortolio{get; protected set;}
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


    
    
    protected float maxPrice = 2f;
    protected float minPrice = -2f;

    public Securities()
    {
        Price = 5f;
        AmountInPortolio = 0;
    }
    public virtual void UpdatePrice()
    {
    }

    public virtual string GetName() => ParentCompany.GetNameOfCompany();

    public virtual void RecalculateHistoryForValute(Valute val, Valute prevVal)
    {
    }
    public virtual void SetAmount(int am)
    {
    }

    public virtual void AddTransaction(int amount, float pricePerOne)
    {
    
    }
}

[System.Serializable]
public class Share : Securities
{
    private int countOfChanges = 0;
    private bool needToSet = true;
    public override void UpdatePrice()
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

        Price = Mathf.Round(Price * 10000f)/10000f;
        
        _priceHistory.Add(Price);
    }

    public override string GetName()
    {
        return ParentCompany.GetNameOfCompany();
    }

    public override void RecalculateHistoryForValute(Valute val, Valute prevVal)
    {
        for(int i = _priceHistory.Count - 1500, j = val._priceHistory.Count - 1500; i < _priceHistory.Count; i++, j++)
        {
            _priceHistory[i] *= val._priceHistory[j] / prevVal._priceHistory[j];
        }
        
        Price *= val.Price/prevVal.Price;
    }
    public override void SetAmount(int am)
    {
        if(am >= 0)
            AmountInPortolio += am;
        else
            throw new System.Exception("Wrong Amount Of Sec");
    }

    public override void AddTransaction(int amount, float pricePerOne)
    {
        List<float> tempList = new List<float>();
        tempList.Add(amount);
        tempList.Add(pricePerOne);

        for (int i = 0; i < BalanceManager._instance.Valutes.Count; i++)
        {
            tempList.Add(BalanceManager._instance.Valutes[i].Price);
        }

        TransHistory.Add(tempList);
    }

}
[System.Serializable]
public class Obligation : Securities
{

}
[System.Serializable]
public class Future : Securities
{

}

[System.Serializable]
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
    public float GetPriceInCurrentValue()
    {
        return DetailedInfoManager._instance.currentValute.Price/Price;
    }
    public float GetPreviousPriceInCurrentValue() => DetailedInfoManager._instance.currentValute._priceHistory[_priceHistory.Count-2] /_priceHistory[_priceHistory.Count-2];
    public override void UpdatePrice()
    {

        if (!isUpdatable)
        {
            Delta = 0;
            Price = 1;
            _priceHistory.Add(Price);
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


