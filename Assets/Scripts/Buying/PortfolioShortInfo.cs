using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PortfolioShortInfo : MonoBehaviour
{
    private List<List<float>> TransHistory = new List<List<float>>();

    [SerializeField] public TextMeshProUGUI companyName;
    [SerializeField] TextMeshProUGUI additionalInfo;
    [SerializeField] TextMeshProUGUI myAmountOfSecurities;
    [SerializeField] TextMeshProUGUI percentOfChange;
    [SerializeField] TextMeshProUGUI price;


    Securities securities;
    Color red = new Color(0.8f, 0.09f, 0.09f, 1);
    Color green = new Color(0.09f, 0.7f, 0.1f, 1f);
    public float SpendOnSecurity { get; private set; }


    public void SetInfo(Securities sec, int amount, float spend)
    {
        securities = sec;
        AddTransaction(spend);
        ShowSpentInCurrentVal();
        SetAmount(amount);
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
        companyName.text = securities.ParentCompany.GetNameOfCompany();
        
        price.text = securities.Price.ToString("0.00");
        ShowSpentInCurrentVal();

        if (securities.Delta > 0f)
            percentOfChange.color = green;
        else if (securities.Delta<0f)
            percentOfChange.color = red;
        
        percentOfChange.text =  Math.Abs(securities.Delta).ToString("0.00") + "%";
        
    }
    public void ShowSpentInCurrentVal()
    {
        float total = 0;
        int valID = 0;
        for (int i = 0; i < BalanceManager._instance.Valutes.Count; i++)
            if (DetailedInfoManager._instance.currentValute == BalanceManager._instance.Valutes[i])
            {
                valID = i;
                break;
            }


        for (int i = 0; i < TransHistory.Count; i++)
            total += TransHistory[i][0] * TransHistory[i][valID + 1];
        additionalInfo.text = total.ToString("0.00");
    }


    public void AddTransaction(float spend)
    {
        List<float> tempList = new List<float>();
        tempList.Add(spend);

        for (int i = 0; i < BalanceManager._instance.Valutes.Count; i++)
        {
            tempList.Add(BalanceManager._instance.Valutes[i].Price);
        }

        TransHistory.Add(tempList);
        ShowSpentInCurrentVal();
    }
    
}
