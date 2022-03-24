using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.Events;
using System;

public class ShortInfo : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] int index;
    [SerializeField] TextMeshProUGUI _securityName;
    [SerializeField] TextMeshProUGUI percentOfChange;
    [SerializeField] TextMeshProUGUI price;
    Securities sec;
    Company company;
    Valute valute;
    DetailedInfoManager companies;
    Color red = new Color(0.8f, 0.09f, 0.09f, 1);
    Color green = new Color(0.09f, 0.7f, 0.1f, 1f);
    double deltaPrice;


    public void SetInfo(Securities securities)
    {
        sec = securities;
        _securityName.text = securities.GetName();

        button.onClick.AddListener(() => { DetailedInfoManager._instance.UpdateAllInformation(securities); });

        UpdateInfo();
    }


    public void UpdateInfo()
    {
        Valute currentVal = DetailedInfoManager._instance.currentValute;
        if(DetailedInfoManager._instance.currentSecurity.GetType() == typeof(Valute))
        {
            deltaPrice = (sec.GetPriceInCurrentValue() - sec.GetPreviousPriceInCurrentValue()) / sec.GetPreviousPriceInCurrentValue() * 100f;
        }
        else
            deltaPrice = sec.Delta;

        if(sec.GetType() == typeof(Valute))
            price.text = sec.GetPriceInCurrentValue().ToString();
        else
            price.text = sec.Price.ToString();

        if (deltaPrice > 0)
            percentOfChange.color = green;
        else if (deltaPrice < 0)
            percentOfChange.color = red;

        percentOfChange.text = Math.Abs(deltaPrice).ToString("0.00") + "%";
    }
}

