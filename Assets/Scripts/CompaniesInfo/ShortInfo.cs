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
    Color red = new Color(0.8f, 0.09f, 0.09f, 1);
    Color green = new Color(0.09f, 0.7f, 0.1f, 1f);
    double deltaPrice;
    [SerializeField] private ColorBlock _buttonColors;


    public void SetInfo(Securities securities)
    {
        sec = securities;
        _securityName.text = securities.GetName();

        button.onClick.AddListener(() => { DetailedInfoManager._instance.UpdateAllInformation(securities); NewsManager._instance.ShowCompanyNews(securities.ParentCompany); });

        UpdateInfo();
    }


    public void UpdateInfo()
    {
        if (DetailedInfoManager._instance.currentSecurity == sec)
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

        Valute currentVal = DetailedInfoManager._instance.currentValute;
        if(DetailedInfoManager._instance.currentSecurity.GetType() == typeof(Valute))
        {
            deltaPrice = (sec.GetPriceInCurrentValue() - (sec as Valute).GetPreviousPriceInCurrentValue()) / (sec as Valute).GetPreviousPriceInCurrentValue() * 100f;
        }
        else
            deltaPrice = sec.Delta;

        if(sec.GetType() == typeof(Valute))
            price.text = sec.GetPriceInCurrentValue().ToString("0.00");
        else
            price.text = sec.Price.ToString("0.00");

        if (deltaPrice > 0)
            percentOfChange.color = green;
        else if (deltaPrice < 0)
            percentOfChange.color = red;

        percentOfChange.text = Math.Abs(deltaPrice).ToString("0.00") + "%";
    }
}

