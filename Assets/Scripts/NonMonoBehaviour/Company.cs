using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Company
{
    [SerializeField] string companyName = "Enter new company name here";
    [SerializeField] private List<float> _shareHistory;
    public Securities DisplayedSec;
    public Country Country{get; private set;}
    public Share CompanyShare;
    public Obligation CompanyObligation;
    public ETF CompanyETF;

    [SerializeField] float capitalization;
    [SerializeField] int amountOfSecurities;
    [SerializeField] float profit;
    [SerializeField] int staff;
    [SerializeField] float credit;



    public float Profit { get => profit; }
    public int Staff { get => staff; }
    public float Credit { get => credit; }
    public float Capitalization { get => capitalization; }
    public int AmountOfSecurities { get => amountOfSecurities; }

    [SerializeField] private float _minPriceChange;
    [SerializeField] private float _maxPriceChange;

    public void ChangeMinPriceChange(float price)
    {
        if (_minPriceChange + price > -5f && _minPriceChange + price < 0f)
            _minPriceChange += price;
    }
    public void ChangeMaxPriceChange(float price)
    {
        if (_maxPriceChange + price < 5f && _maxPriceChange + price > 0f)
            _maxPriceChange += price;
    }
    

    public float GetMinPriceChange() => _minPriceChange;
    public float GetMaxPriceChange() => _maxPriceChange;

    public string GetNameOfCompany() => companyName;

    public Company(string name, Country country)
    {
        Country = country;
        companyName = name;
        capitalization = UnityEngine.Random.Range(150f, 1000f);
        amountOfSecurities = UnityEngine.Random.Range(150, 1000);
        profit = UnityEngine.Random.Range(150f, 1000f);
        staff = UnityEngine.Random.Range(150, 1000);
        credit = UnityEngine.Random.Range(150f, 1000f);

        CompanyObligation = new Obligation();
        CompanyShare = new Share();
        CompanyETF = new ETF();
        GeneratePreGameHistory();
        CompanyShare.CalculateAveragePrice();
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
            CompanyShare.UpdatePrice();
            CompanyObligation.UpdatePrice();
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
