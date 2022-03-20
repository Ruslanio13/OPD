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
    

    public void SetInfo(Company comp, float amount)
    {
        company = comp;
        //button.onClick.AddListener(() => { DetailedInfoManager._instance.UpdateAllInformation(company); });
        companyName.text = company.GetNameOfCompany();
        
        price.text = company.GetSecurityPrice().ToString();
        myAmountOfSecurities.text = amount.ToString();
    }

    void Update() 
    {
       UpdatePercent(); 
       myAmountOfSecurities.text = company.GetSecurityMyAmount().ToString();
    }

    public void UpdatePercent()
    {
        if (company == null)
            return;
        deltaPrice = company.GetSecurityDelta();
        if (deltaPrice > 0)
            percentOfChange.color = green;
        else if (deltaPrice < 0)
            percentOfChange.color = red;

        percentOfChange.text =  Math.Abs(deltaPrice).ToString("0.00") + "%";
        price.text = company.GetSecurityPrice().ToString("0.00");
        //companyName.text = company.GetNameOfCompany();
    }

     
}
