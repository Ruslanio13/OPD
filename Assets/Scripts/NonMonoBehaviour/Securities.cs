using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Securities
{
    [System.NonSerialized] public Company ParentCompany;
    public List<List<float>> TransHistory = new List<List<float>>();
    public float DeltaPrice = 0;
    public int AmountInPortolio { get; protected set; }
    public float Price { get; protected set; }

    public float AveragePrice2017 { get; protected set; }
    public float AveragePrice2018 { get; protected set; }
    public float AveragePrice2019 { get; protected set; }
    public float AveragePrice2020 { get; protected set; }
    public float AveragePrice2021 { get; protected set; }

    public List<float> _priceHistory = new List<float>();

    protected float maxPrice = 2f;
    protected float minPrice = -2f;

    public float GetPriceInRubles()
    {
        return Price / BalanceManager._instance.Valutes[1].GetPriceInCurrentValue();
    }
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
        for (int i = _priceHistory.Count - 1500, j = val._priceHistory.Count - 1500; i < _priceHistory.Count; i++, j++)
        {
            _priceHistory[i] *= val._priceHistory[j] / prevVal._priceHistory[j];
        }

        Price *= val.Price / prevVal.Price;
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
    private float _dividendsPercent;

    public Share()
    {
        _dividendsPercent = 4f;
    }
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

        DeltaPrice = UnityEngine.Random.Range(minPrice, maxPrice);
        Price += Price * DeltaPrice / 100;

        Price = Mathf.Round(Price * 10000f) / 10000f;

        _priceHistory.Add(Price);
    }

    public override string GetName()
    {
        return ParentCompany.GetNameOfCompany();
    }
    public override void SetAmount(int am)
    {
        if (am >= 0)
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

    public void CalculateAveragePrice()
    {
        for (int i = 0; i < 300; i++)
        {
            AveragePrice2017 += _priceHistory[i];
            AveragePrice2018 += _priceHistory[i + 300];
            AveragePrice2019 += _priceHistory[i + 600];
            AveragePrice2020 += _priceHistory[i + 900];
            AveragePrice2021 += _priceHistory[i + 1200];
        }
        AveragePrice2017 /= 300f;
        AveragePrice2018 /= 300f;
        AveragePrice2019 /= 300f;
        AveragePrice2020 /= 300f;
        AveragePrice2021 /= 300f;
    }

    public void PayDividends()
    {
        
        BalanceManager._instance.AddMoney(GetSumOfDividends());
    }
    public float GetSumOfDividends()
    {
        return GetPriceInRubles() * AmountInPortolio * _dividendsPercent /100f;
    }
}
[System.Serializable]
public class Obligation : Securities
{
    public string ParentCompanyName{get; private set;}
    public float DeltaPaybackPercent{get; private set;}
    public int DueTo { get; private set; }
    private int _paybackTime;
    public float PercentOfPayback { get; private set; }

    public float PaybackCost { get; private set; }
    public Obligation()
    {
        _paybackTime = 100;
        DueTo = -1;
        PercentOfPayback = 5f;
        Price = 50f;
    }
    public Obligation(Obligation copied, string compName, int amount)
    {
        this.ParentCompanyName = compName;

        this._paybackTime = copied._paybackTime;

        this.DueTo = copied._paybackTime;
        
        this.PercentOfPayback = copied.PercentOfPayback;
        
        this._priceHistory = copied._priceHistory;

        this.PaybackCost = copied.Price * (1 + copied.PercentOfPayback / 100f) / BalanceManager._instance.Valutes[1].GetPriceInCurrentValue() * amount;

        this.AmountInPortolio = amount;

    }

    public override void UpdatePrice()
    {
        Price += Price * DeltaPrice / 100;
        DeltaPrice = Random.Range(-0.1f, 0.1f);

        DeltaPaybackPercent = Random.Range(-2f, 2f);
        PercentOfPayback += PercentOfPayback * DeltaPaybackPercent / 100f;


        _priceHistory.Add(Price);
    }

    public void DecreaseDueTo()
    {
        DueTo--;
        if (DueTo == (_paybackTime / 2))
        {
            BalanceManager._instance.AddMoney(PaybackCost / 2);
        }
        else if (DueTo == 0)
        {
            BalanceManager._instance.AddMoney(PaybackCost / 2);
            PortfolioManager._instance.RemoveSecurities(this, AmountInPortolio);
        }
    }
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
        Price = 1f;
        Symbol = sign;
        Name = name;
        ParentCompany = null;
    }
    public float GetPriceInCurrentValue()
    {
        return DetailedInfoManager._instance.currentValute.Price / Price;
    }
    public float GetPreviousPriceInCurrentValue() => DetailedInfoManager._instance.currentValute._priceHistory[_priceHistory.Count - 2] / _priceHistory[_priceHistory.Count - 2];
    public override void UpdatePrice()
    {

        if (!isUpdatable)
        {
            DeltaPrice = 0;
            Price = 1;
            _priceHistory.Add(Price);
            return;
        }

        DeltaPrice = UnityEngine.Random.Range(-2f, 2f);
        Price += Price * DeltaPrice / 100;
        _priceHistory.Add(Price);
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
    public override void SetAmount(int am)
    {
        if (am >= 0)
            AmountInPortolio += am;
        else
            throw new System.Exception("Wrong Amount Of Sec");
    }
    public override string GetName()
    {
        return Name;
    }


}


