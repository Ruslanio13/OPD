using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PortfolioShortInfo : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI companyName;
    [SerializeField] private TextMeshProUGUI _spendMoney;
    [SerializeField] private TextMeshProUGUI _currentPrice;
    [SerializeField] private TextMeshProUGUI myAmountOfSecurities;
    [SerializeField] private TextMeshProUGUI _profitPercent;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Button SelectSecurityButton;

    [SerializeField] private ColorBlock _buttonColors;


    public Securities securities { get; private set; }
    Color red = new Color(0.8f, 0.09f, 0.09f, 1);
    Color green = new Color(0.09f, 0.7f, 0.1f, 1f);
    public float SpendOnSecurity { get; private set; }


    public void SetInfo(Securities sec)
    {
        securities = sec;
        SelectSecurityButton.onClick.AddListener(() =>
       {
           DetailedInfoManager._instance.SelectSecurity(sec);
           PortfolioManager._instance.UpdatePortfolio();
       });
    }
    public void UpdateInfo()
    {
        if (DetailedInfoManager._instance.currentSecurity == securities)
        {
            _buttonColors.normalColor = new Color32(0x3E, 0x5B, 0xD2, 255);
            _buttonColors.selectedColor = new Color32(0x3E, 0x5B, 0xD2, 255);
            SelectSecurityButton.colors = _buttonColors;
        }
        else
        {
            _buttonColors.selectedColor = Color.white;
            _buttonColors.normalColor = Color.white;
            SelectSecurityButton.colors = _buttonColors;
        }

        float profitPercentFloat = ((securities.AmountInPortolio * securities.Price) / ShowSpentInCurrentVal(securities) - 1) * 100;
        companyName.text = securities.ParentCompany.GetNameOfCompany();

        price.text = securities.Price.ToString("0.00");
        myAmountOfSecurities.text = securities.AmountInPortolio.ToString();
        _spendMoney.text = ShowSpentInCurrentVal(securities).ToString("0.00");

        if (profitPercentFloat > 0f)
            _profitPercent.color = green;
        else if (profitPercentFloat < 0f)
            _profitPercent.color = red;

        _profitPercent.text = Math.Abs(securities.Delta).ToString("0.00") + "%";
        _profitPercent.text = profitPercentFloat.ToString("0.00") + "%";
        _currentPrice.text = (securities.AmountInPortolio * securities.Price).ToString("0.00");

    }
    public float ShowSpentInCurrentVal(Securities sec)
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
            total += sec.TransHistory[i][0] * sec.TransHistory[i][1] * sec.TransHistory[i][valID + 2];
        return total;
    }
}