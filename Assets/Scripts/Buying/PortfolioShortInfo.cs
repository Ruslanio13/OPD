using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PortfolioShortInfo : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI companyName;
    [SerializeField] TextMeshProUGUI additionalInfo;
    [SerializeField] TextMeshProUGUI myAmountOfSecurities;
    [SerializeField] TextMeshProUGUI percentOfChange;
    [SerializeField] TextMeshProUGUI price;


    public Securities securities{get; private set;}
    Color red = new Color(0.8f, 0.09f, 0.09f, 1);
    Color green = new Color(0.09f, 0.7f, 0.1f, 1f);
    public float SpendOnSecurity { get; private set; }


    public void SetInfo(Securities sec)
    {
        securities = sec;
    }
    public void UpdateInfo()
    {
        companyName.text = securities.ParentCompany.GetNameOfCompany();
        
        price.text = securities.Price.ToString("0.00");
        myAmountOfSecurities.text = securities.Amount.ToString();
        ShowSpentInCurrentVal(securities);

        if (securities.Delta > 0f)
            percentOfChange.color = green;
        else if (securities.Delta<0f)
            percentOfChange.color = red;
        
        percentOfChange.text =  Math.Abs(securities.Delta).ToString("0.00") + "%";
        
    }
    public void ShowSpentInCurrentVal(Securities sec)
    {
        float total = 0;
        int valID = 0;
        for (int i = 0; i < BalanceManager._instance.Valutes.Count; i++)
            if (DetailedInfoManager._instance.currentValute == BalanceManager._instance.Valutes[i])
            {
                valID = i;
                break;
            }


        for (int i = 0; i < sec.TransHistory.Count; i++)
            total += sec.TransHistory[i][0] * sec.TransHistory[i][valID + 1];
        additionalInfo.text = total.ToString("0.00");
    }


    
    
}
