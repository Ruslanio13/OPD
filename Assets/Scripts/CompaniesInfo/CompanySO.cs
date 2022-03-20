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
    }

    public void UpdatePrice()
    {
        CompanyShare.UpdatePrice();
        CompanyObligation.UpdatePrice();
        CompanyFuture.UpdatePrice();
    }

    public void UpdateMyAmount(float amountDelta)
    {
        CompanyShare.UpdateMyAmountOfSecurities(amountDelta);
        CompanyObligation.UpdateMyAmountOfSecurities(amountDelta);
        CompanyFuture.UpdateMyAmountOfSecurities(amountDelta);
    }

    public float GetSecurityPrice()
    {
        if(DetailedInfoManager._instance.currentCompany.GetType() == typeof(Share))
            return CompanyShare.GetPrice();
        if(DetailedInfoManager._instance.currentCompany.GetType() == typeof(Obligation))
            return CompanyObligation.GetPrice();
        if(DetailedInfoManager._instance.currentCompany.GetType() == typeof(Future))
            return CompanyFuture.GetPrice();        
        return CompanyShare.GetPrice();
    }

    public float GetSecurityMyAmount()
    {
        if(DetailedInfoManager._instance.currentCompany.GetType() == typeof(Share))
            return CompanyShare.GetMyAmountOfSecurities();
        if(DetailedInfoManager._instance.currentCompany.GetType() == typeof(Obligation))
            return CompanyObligation.GetMyAmountOfSecurities();
        if(DetailedInfoManager._instance.currentCompany.GetType() == typeof(Future))
            return CompanyFuture.GetMyAmountOfSecurities();        
        return CompanyShare.GetMyAmountOfSecurities();
    }

    public float GetSecurityDelta()
    {
        if (DetailedInfoManager._instance.currentCompany.GetType() == typeof(Share))
            return CompanyShare.delta;
        if (DetailedInfoManager._instance.currentCompany.GetType() == typeof(Obligation))
            return CompanyObligation.delta;
        if (DetailedInfoManager._instance.currentCompany.GetType() == typeof(Future))
            return CompanyFuture.delta;
        return CompanyShare.delta;
    }

    public void GeneratePreGameHistory()
    {
        for (int i = 0; i < 1500; i++)
        {
            UpdatePrice();
        }
    }



}
