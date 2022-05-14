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
    [SerializeField] TextMeshProUGUI percentOfPayback;
    [SerializeField] TextMeshProUGUI ETFAmount;
    Securities sec;
    Color red = new Color(0.8f, 0.09f, 0.09f, 1);
    Color green = new Color(0.09f, 0.7f, 0.1f, 1f);
    double deltaPrice;
    [SerializeField] private ColorBlock _buttonColors;


    public void SetInfo(Securities securities, int etfAmount = 0)
    {
        sec = securities;
        _securityName.text = securities.GetName();
        
        button.onClick.AddListener(() => { GameManager._instance.UpdateAllInformation(securities); NewsManager._instance.ShowCompanyNews(securities.ParentCompany); });

        if(ETFAmount != null)
            ETFAmount.text = etfAmount.ToString();

        UpdateInfo();
    }


    public void UpdateInfo()
    {
        if (GameManager._instance.CurrentSecurity == sec)
        {
            _buttonColors.normalColor = new Color32(0x3E, 0x5B, 0xD2, 255);
            _buttonColors.selectedColor = new Color32(0x3E, 0x5B, 0xD2, 255);
            button.colors = _buttonColors;
        }
        else
        {
            _buttonColors.selectedColor = Color.white;
            _buttonColors.normalColor = Color.white;
            button.colors = _buttonColors;
        }

        Valute currentVal = GameManager._instance.CurrentValute;

        if(GameManager._instance.CurrentSecurity.GetType() == typeof(Valute))
            deltaPrice = ((sec as Valute).GetPriceInCurrentValue() - (sec as Valute).GetPreviousPriceInCurrentValue()) / (sec as Valute).GetPreviousPriceInCurrentValue() * 100f;
        else
            deltaPrice = sec.DeltaPrice;

        if(sec.GetType() == typeof(Valute))
            price.text = (sec as Valute).GetPriceInCurrentValue().ToString("0.000");
        else
            price.text = sec.Price.ToString("0.00");


        if (deltaPrice > 0)
            percentOfChange.color = green;
        else if (deltaPrice < 0)
            percentOfChange.color = red;



        percentOfChange.text = Math.Abs(deltaPrice).ToString("0.00") + "%";
    }
}

