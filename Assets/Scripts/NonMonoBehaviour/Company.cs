using System.Collections;
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
    [JsonIgnore]
    public Share CompanyShare;
    [JsonIgnore]
    public Obligation CompanyObligation;
    [JsonIgnore]
    public ETF CompanyETF;




    public string CompanyName{get; private set;}
    public float Profit { get; private set; }
    public int Staff { get; private set; }
    public float Credit { get; private set; }
    public float Capitalization { get; private set; }
    public int AmountOfSecurities { get; private set; }

    [SerializeField] private float _minPriceChange;
    [SerializeField] private float _maxPriceChange;

    public void ChangePriceVolatility(float max, float min)
    {
        if (_minPriceChange + min > -5f * PreGameManager._instance.CurrentDifficulty.Coefficient  && _minPriceChange + min < 0f)
            _minPriceChange += min * PreGameManager._instance.CurrentDifficulty.Coefficient;
        if (_maxPriceChange + max < 5f && _maxPriceChange + max > 0f)
            _maxPriceChange += max;
    
        CompanyShare.OnCompanyVolatilityChange(_maxPriceChange, _minPriceChange);
        CompanyObligation.OnCompanyVolatilityChange(_maxPriceChange, _minPriceChange);
    }
    
    public float GetMinPriceChange() => _minPriceChange;
    public float GetMaxPriceChange() => _maxPriceChange;

    public string GetNameOfCompany() => CompanyName;

    [JsonConstructor]
    public Company(string CompanyName, Country country)
    {
        _minPriceChange = -2f;
        _maxPriceChange = 2f;
        Country = country;
        this.CompanyName = CompanyName;
    }
    public Company(Company companyFromJSON)
    {
        var country = GameManager._instance.Countries[companyFromJSON.Country.ID];
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

    public void UpdatePrice()
    {
        CompanyShare.UpdatePrice();
        CompanyObligation.UpdatePrice();
        CompanyETF.UpdatePrice();
    }

    public float GetSecurityDelta()
    {
        if (GameManager._instance.currentCompany.GetType() == typeof(Share))
            return CompanyShare.DeltaPrice;
        if (GameManager._instance.currentCompany.GetType() == typeof(Obligation))
            return CompanyObligation.DeltaPrice;
        if (GameManager._instance.currentCompany.GetType() == typeof(ETF))
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
            var randAmount = UnityEngine.Random.Range(0, 20);
            CompanyETF.AddShareToFond(GameManager._instance.Companies[allowedNumbers[randShare]].CompanyShare, randAmount);

            allowedNumbers.RemoveAt(randShare);
        }
        CompanyETF.GeneratePriceHistory();
    }
}
