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
    [SerializeField] TextMeshProUGUI companyName;
    [SerializeField] TextMeshProUGUI additionalInfo;
    [SerializeField] TextMeshProUGUI myAmountOfSecurities;
    [SerializeField] TextMeshProUGUI percentOfChange;  
    [SerializeField] TextMeshProUGUI price;

    DetailedInfoManager companies;
    Company company;
    float deltaPrice;
    Color red = new Color(0.8f, 0.09f, 0.09f, 1);
    Color green = new Color(0.09f, 0.7f, 0.1f, 1f); 
    

    public void SetInfo(Securities sec, float amount)
    {
        company = sec.ParentCompany;
        //button.onClick.AddListener(() => { DetailedInfoManager._instance.UpdateAllInformation(company); });
        companyName.text = company.GetNameOfCompany();
        
        price.text = company.GetSecurityPrice().ToString();
        myAmountOfSecurities.text = amount.ToString();
        percentOfChange.text =  Math.Abs(company.GetSecurityDelta()).ToString("0.00") + "%";
        price.text = company.GetSecurityPrice().ToString("0.00");
    }


    public void SetAmount(int amount)
    {
        myAmountOfSecurities.text = amount.ToString();
        if(amount <= 0)
            Destroy(gameObject);
    }     
}
