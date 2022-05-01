using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PortfolioShortInfo : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _spendMoney;
    [SerializeField] private TextMeshProUGUI _currentSellPrice;
    [SerializeField] private TextMeshProUGUI myAmountOfSecurities;
    [SerializeField] private TextMeshProUGUI _profitPercent;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI dueTo;
    [SerializeField] private TextMeshProUGUI PaybackCost;
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
           GameManager._instance.SelectSecurity(sec);
           PortfolioManager._instance.UpdatePortfolio();
       });
    }
    public void UpdateInfo()
    {
        if (GameManager._instance.currentSecurity == securities)
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

        if (securities.GetType() == typeof(Share) |securities.GetType() == typeof(ETF))
        {
            _name.text = securities.GetName();

            price.text = securities.Price.ToString("0.00");
            myAmountOfSecurities.text = securities.AmountInPortolio.ToString();
            _spendMoney.text = ShowSpentInCurrentVal(securities).ToString("0.00");


            float profitPercentFloat = ((securities.AmountInPortolio * securities.SellPrice) / ShowSpentInCurrentVal(securities) - 1) * 100;
            if (profitPercentFloat > 0f)
                _profitPercent.color = green;
            else if (profitPercentFloat < 0f)
                _profitPercent.color = red;

            _profitPercent.text = profitPercentFloat.ToString("0.00") + "%";
            _currentSellPrice.text = (securities.AmountInPortolio * securities.SellPrice).ToString("0.00");

        }
        else if (securities.GetType() == typeof(Obligation))
        {
            _name.text = (securities as Obligation).ParentCompanyName;
            PaybackCost.text = "Будет выплачено: "+(securities as Obligation).PaybackCost.ToString("0.00") + "P";
            dueTo.text = "через: " + (securities as Obligation).DueTo.ToString() + " дней ";
        }
        else if(securities.GetType() == typeof(Valute))
        {

            _name.text = (securities as Valute).GetName();
            price.text = (securities as Valute).GetPriceInCurrentValue().ToString("0.00");
            myAmountOfSecurities.text = securities.AmountInPortolio.ToString();
            _spendMoney.text = ShowSpentInCurrentVal(securities).ToString("0.00");




            float profitPercentFloat = ((securities.AmountInPortolio * (securities as Valute).GetSellPriceInCurrentValue()) / ShowSpentInCurrentVal(securities) - 1) * 100;
            if (profitPercentFloat > 0f)
                _profitPercent.color = green;
            else if (profitPercentFloat < 0f)
                _profitPercent.color = red;

            _profitPercent.text = profitPercentFloat.ToString("0.00") + "%";
            _currentSellPrice.text = (securities.AmountInPortolio * (securities as Valute).GetPriceInCurrentValue()).ToString("0.00");
        }
    }
    public float GetSpendMoney()
    {
       return ShowSpentInCurrentVal(securities);
    }
    public float ShowSpentInCurrentVal(Securities sec)
    {
        float total = 0;
        int valID = 0;
        for (int i = 0; i < BalanceManager._instance.Valutes.Count; i++)
            if (GameManager._instance.currentValute == BalanceManager._instance.Valutes[i])
            {
                valID = i;
                break;
            }

        if(sec.GetType() == typeof(Share) || sec.GetType() == typeof(ETF))
            for (int i = 0; i < sec.TransHistory.Count; i++)
                total += sec.TransHistory[i][0] * sec.TransHistory[i][1] * sec.TransHistory[i][valID + 2];
        
        else if(sec.GetType() == typeof(Valute))
            for (int i = 0; i < sec.TransHistory.Count; i++)
                total += sec.TransHistory[i][0] / sec.TransHistory[i][1] * sec.TransHistory[i][valID + 2];
            
        return total;
    }
    public void SelectSecurity()
    {
        if(securities.GetType() != typeof(Obligation))
            GameManager._instance.SelectSecurity(securities);
    }
}