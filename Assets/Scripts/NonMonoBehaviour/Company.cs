using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class Company
{
    private List<float> _shareHistory;
    public Securities DisplayedSec;
    public Country Country{get; private set;}
    public string ETFName{get; private set;}

    [JsonIgnore]
    public Share CompanyShare;
    [JsonIgnore]
    public Obligation CompanyObligation;
    [JsonIgnore]
    public ETF CompanyETF;

    public string CompanyName{get; private set;}
    public float Profit { get; private set; }
    public float EBITDA { get; private set; }
    public float ClearProfit { get; private set; }
    public float Actives { get; private set; }
    public float Debt { get; private set; }
    public float DivProfit { get; private set; }
    public float PE { get; private set; }
    public float PS { get; private set; }
    public float PBV { get; private set; }
    public float EVEBITDA { get; private set; }
    

    [SerializeField] private float _minPriceChange;
    [SerializeField] private float _maxPriceChange;

    public void ChangePriceVolatility(float max, float min)
    {
        if (_minPriceChange + min * PreGameManager._instance.CurrentDifficulty.Coefficient > -2f - PreGameManager._instance.CurrentDifficulty.Coefficient && _minPriceChange + min * PreGameManager._instance.CurrentDifficulty.Coefficient < -0.25f)
            _minPriceChange += min * PreGameManager._instance.CurrentDifficulty.Coefficient;
        if (_maxPriceChange + max < 2f + PreGameManager._instance.CurrentDifficulty.Coefficient && _maxPriceChange + max > 0.5f)
            _maxPriceChange += max;
        if (CompanyName == "Sberbank")
            Debug.Log(_minPriceChange + " " + _maxPriceChange);
    
        CompanyShare.OnCompanyVolatilityChange(_maxPriceChange, _minPriceChange);
        CompanyObligation.OnCompanyVolatilityChange(_maxPriceChange, _minPriceChange);
    }
    
    public float GetMinPriceChange() => _minPriceChange;
    public float GetMaxPriceChange() => _maxPriceChange;

    public string GetNameOfCompany() => CompanyName;

    [JsonConstructor]
    public Company(string CompanyName, Country country, float Profit, float EBITDA, float ClearProfit, float Actives, float Debt, float DivProfit, float PE, float PS, float PBV, float EVEBITDA, string ETFName)
    {
        this.Profit = Profit;
        this.EBITDA = EBITDA;
        this.ClearProfit = ClearProfit;
        this.Actives = Actives;
        this.Debt = Debt;
        this.DivProfit = DivProfit;
        this.PE = PE;
        this.PS = PS;
        this.PBV = PBV;
        this.EVEBITDA = EVEBITDA;
        this.ETFName = ETFName;

        _minPriceChange = -2f;
        _maxPriceChange = 2f;
        Country = country;
        this.CompanyName = CompanyName;
    }
    public Company(Company companyFromJSON)
    {
        var country = GameManager._instance.Countries[companyFromJSON.Country.ID];
        if(country.ID != 0)
            SetAdditionalInfo(companyFromJSON, BalanceManager._instance.Valutes[1].Price);
        else
            SetAdditionalInfo(companyFromJSON);

        this.ETFName = companyFromJSON.ETFName;

        Country = country;
        this.CompanyName = companyFromJSON.CompanyName;

        CompanyObligation = new Obligation();
        CompanyShare = new Share();
        CompanyETF = new ETF();
        GeneratePreGameHistory();
        CompanyShare.CalculateAveragePrice();
        CompanyObligation.CalculateAveragePrice();

        country.HandlePriceVolatility += ChangePriceVolatility;
    }

    private void SetAdditionalInfo(Company companyFromJSON, float valutePrice = 1)
    {

        this.Profit = companyFromJSON.Profit * valutePrice;
        this.EBITDA = companyFromJSON.EBITDA * valutePrice;
        this.ClearProfit = companyFromJSON.ClearProfit * valutePrice;
        this.Actives = companyFromJSON.Actives * valutePrice;
        this.Debt = companyFromJSON.Debt * valutePrice;
        this.DivProfit = companyFromJSON.DivProfit * valutePrice;
        this.PE = companyFromJSON.PE * valutePrice;
        this.PS = companyFromJSON.PS * valutePrice;
        this.PBV = companyFromJSON.PBV * valutePrice;
        this.EVEBITDA = companyFromJSON.EVEBITDA * valutePrice;
    }

    public void UpdatePrice()
    {
        CompanyShare.UpdatePrice();
        CompanyObligation.UpdatePrice();
        CompanyETF.UpdatePrice();
    }

    public float GetSecurityDelta()
    {
        if (GameManager._instance.CurrentCompany.GetType() == typeof(Share))
            return CompanyShare.DeltaPrice;
        if (GameManager._instance.CurrentCompany.GetType() == typeof(Obligation))
            return CompanyObligation.DeltaPrice;
        if (GameManager._instance.CurrentCompany.GetType() == typeof(ETF))
            return CompanyETF.DeltaPrice;
        return CompanyShare.DeltaPrice;
    }

    public void GeneratePreGameHistory()
    {
        for (int i = 0; i < 1500; i++)
        {
            CompanyShare.UpdatePrice(false);
            CompanyObligation.UpdatePrice(false);
        }
    }

    public void SetCompanyToSecurities()
    {
        CompanyShare.ParentCompany = this;
        CompanyObligation.ParentCompany = this;
        CompanyETF.ParentCompany = this;
    }

    public void InitializeETF()
    {
        List<int> allowedNumbers = new List<int>();
        for(int i = 0; i< GameManager._instance.Companies.Count; i++)
        {
            allowedNumbers.Add(i);
        }
        
        for(int i = 0; i < UnityEngine.Random.Range(10, 16); i++)
        {
            var randShare = UnityEngine.Random.Range(0, allowedNumbers.Count);
            var randAmount = UnityEngine.Random.Range(1, 20);
            CompanyETF.AddShareToFond(GameManager._instance.Companies[allowedNumbers[randShare]].CompanyShare, randAmount);

            allowedNumbers.RemoveAt(randShare);
        }
        CompanyETF.GeneratePriceHistory();
    }
}
