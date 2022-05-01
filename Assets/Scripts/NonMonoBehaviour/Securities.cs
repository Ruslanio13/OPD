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
    public float SellPrice => Price * (100f - PreGameManager._instance.CurrentBroker.Commision) / 100f;

    public float AveragePrice2017 { get; protected set; }
    public float AveragePrice2018 { get; protected set; }
    public float AveragePrice2019 { get; protected set; }
    public float AveragePrice2020 { get; protected set; }
    public float AveragePrice2021 { get; protected set; }

    public List<float> _priceHistory = new List<float>();

    protected float maxPrice;
    protected float minPrice;

    public float GetPriceInRubles()
    {
        return Price / BalanceManager._instance.Valutes[1].GetPriceInCurrentValue();
    }
    public Securities()
    {
        maxPrice = 2f;
        minPrice = -2f;
        Price = 5f;
        AmountInPortolio = 0;
    }
    public virtual void UpdatePrice(bool reducePrice = true)
    {
    }

    public virtual string GetName() => ParentCompany.GetNameOfCompany();

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
    public virtual void RecalculateHistoryForValute(Valute val, Valute prevVal)
    {
        for (int i = _priceHistory.Count - 1500, j = val._priceHistory.Count - 1500; i < _priceHistory.Count; i++, j++)
        {
            _priceHistory[i] *= val._priceHistory[j] / prevVal._priceHistory[j];
        }

        Price *= val.Price / prevVal.Price;
    }
    public void SetAmount(int am)
    {
        if (am >= 0)
            AmountInPortolio = am;
        else
            throw new System.Exception("Wrong Amount Of Sec");
    }

    public virtual void AddTransaction(int amount, float pricePerOne)
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
    public void OnCompanyVolatilityChange(float max, float min)
    {
        minPrice = min;
        maxPrice = max;
    }
}

[System.Serializable]
public class Share : Securities
{
    private int countOfChanges = 0;
    private float _dividendsPercent;

    public Share()
    {
        _dividendsPercent = 4f;
    }
    public override void UpdatePrice(bool reducePrice = true)
    {
        if (reducePrice)
        {
            maxPrice *= 0.9f;
            minPrice *= 0.9f;
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

    public void PayDividends()
    {

        BalanceManager._instance.AddMoney(GetSumOfDividends());
    }
    public float GetSumOfDividends()
    {
        return GetPriceInRubles() * AmountInPortolio * _dividendsPercent / 100f;
    }
}
[System.Serializable]
public class Obligation : Securities
{
    public string ParentCompanyName { get; private set; }
    public float DeltaPaybackPercent { get; private set; }
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

    public override void UpdatePrice(bool reducePrice = true)
    {
        if (reducePrice)
        {
            maxPrice *= 0.9f;
            minPrice *= 0.9f;
        }

        DeltaPrice = Random.Range(minPrice, maxPrice);
        Price += Price * DeltaPrice / 100;

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
public class ETF : Securities
{
    public List<(Share, int)> Fond = new List<(Share, int)>();
    private float percentageOfFondPrice = 5f;

    public ETF()
    {
        Price = 0;
    }

    public void AddShareToFond(Share share, int amount)
    {
        Fond.Add((share, amount));
        Price += share.Price * amount * percentageOfFondPrice / 100f;
    }
    public override void UpdatePrice(bool reducePrice = true)
    {
        Price = 0;
        foreach (var ShareCortage in Fond)
        {
            Price += ShareCortage.Item1.Price * ShareCortage.Item2 * percentageOfFondPrice / 100f;
        }
        _priceHistory.Add(Price);
        DeltaPrice = (Price - _priceHistory[_priceHistory.Count - 2]) / Price * 100f;
    }

    public void GeneratePriceHistory()
    {
        for (int i = 0; i < 1500; i++)
        {
            _priceHistory.Add(0);
            foreach (var Share in Fond)
                _priceHistory[i] += Share.Item1._priceHistory[i] * Share.Item2 * percentageOfFondPrice / 100f;
        }
        DeltaPrice = (_priceHistory[1499] - _priceHistory[1498]) / _priceHistory[1499] * 100f;
    }
}

[System.Serializable]
public class Valute : Securities
{
    public Country ValuteCountry { get; private set; }
    public string Name { get; protected set; }
    public char Symbol { get; protected set; }
    private bool isUpdatable;



    public Valute(string name, char sign, Country country, bool isUpdatable = true)
    {
        ValuteCountry = country;
        Price = UnityEngine.Random.Range(0.8f, 100f);
        this.isUpdatable = isUpdatable;
        Price = 1f;
        Symbol = sign;
        Name = name;
        ParentCompany = null;
    }
    public float GetPriceInCurrentValue() => GameManager._instance.currentValute.Price / Price;
    public float GetSellPriceInCurrentValue() => GameManager._instance.currentValute.Price / Price * (100f - PreGameManager._instance.CurrentBroker.Commision) / 100f;
    public float GetPreviousPriceInCurrentValue() => GameManager._instance.currentValute._priceHistory[_priceHistory.Count - 2] / _priceHistory[_priceHistory.Count - 2];
    public override void UpdatePrice(bool reducePrice = true)
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
        _priceHistory.Add(1/Price);
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

    public override string GetName()
    {
        return Name;
    }


}


