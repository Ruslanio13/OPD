using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PortfolioShortInfo : MonoBehaviour
{

    [SerializeField] Button button;
    [SerializeField] int index;
    [SerializeField] public TextMeshProUGUI companyName;
    [SerializeField] TextMeshProUGUI additionalInfo;
    [SerializeField] TextMeshProUGUI myAmountOfSecurities;
    [SerializeField] TextMeshProUGUI percentOfChange;  
    [SerializeField] TextMeshProUGUI price;

    //DetailedInfoManager companies;
    Company company;
    float deltaPrice;
    Color red = new Color(0.8f, 0.09f, 0.09f, 1);
    Color green = new Color(0.09f, 0.7f, 0.1f, 1f); 
    

    public void SetInfo(Securities sec, float amount)
    {
        company = sec.ParentCompany;
        myAmountOfSecurities.text = amount.ToString();
        UpdateInfo();        
    }


    public void SetAmount(int amount)
    {
        myAmountOfSecurities.text = amount.ToString();
        if(amount <= 0)
            Destroy(gameObject);
    }     


    public void UpdateInfo()
    {
        companyName.text = company.GetNameOfCompany();
        
        price.text = company.GetSecurityPrice().ToString();
        
        if(company.GetSecurityDelta() > 0f)
            percentOfChange.color = green;
        else if (company.GetSecurityDelta()<0f)
            percentOfChange.color = red;
        
        percentOfChange.text =  Math.Abs(company.GetSecurityDelta()).ToString("0.00") + "%";
        
        price.text = company.GetSecurityPrice().ToString("0.00");
    }
}
