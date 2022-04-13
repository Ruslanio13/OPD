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

    public Share CompanyShare;
    public Obligation CompanyObligation;
    public Future CompanyFuture;

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

    public void SetMinPriceChange(float price) => _minPriceChange = price;
    public void SetMaxPriceChange(float price) => _maxPriceChange = price;

    public float GetMinPriceChange() => _minPriceChange;
    public float GetMaxPriceChange() => _maxPriceChange;

    public string GetNameOfCompany() => companyName;

    public Company(string name)
    {
        companyName = name;
        capitalization = UnityEngine.Random.Range(150f, 1000f);
        amountOfSecurities = UnityEngine.Random.Range(150, 1000);
        profit = UnityEngine.Random.Range(150f, 1000f);
        staff = UnityEngine.Random.Range(150, 1000);
        credit = UnityEngine.Random.Range(150f, 1000f);

        CompanyObligation = new Obligation();
        CompanyShare = new Share();
        CompanyFuture = new Future();
        
        GeneratePreGameHistory();
        CompanyShare.CalculateAveragePrice();
    }

    public void UpdatePrice()
    {
        CompanyShare.UpdatePrice();
        CompanyObligation.UpdatePrice();
        CompanyFuture.UpdatePrice();
    }
    

    public float GetSecurityDelta()
    {
        if (DetailedInfoManager._instance.currentCompany.GetType() == typeof(Share))
            return CompanyShare.DeltaPrice;
        if (DetailedInfoManager._instance.currentCompany.GetType() == typeof(Obligation))
            return CompanyObligation.DeltaPrice;
        if (DetailedInfoManager._instance.currentCompany.GetType() == typeof(Future))
            return CompanyFuture.DeltaPrice;
        return CompanyShare.DeltaPrice;
    }

    public void GeneratePreGameHistory()
    {
        for (int i = 0; i < 1500; i++)
        {
            UpdatePrice();
        }
    }

    public void SetCompanyToSecurities()
    {
        CompanyShare.ParentCompany = this;
        CompanyObligation.ParentCompany = this;
        CompanyFuture.ParentCompany = this;
    }

}
