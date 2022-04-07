using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalanceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceInMarket;
    [SerializeField] private TextMeshProUGUI _balanceInPortfolio;
    public List<Valute> Valutes = new List<Valute>();
    public float RublesWallet;
    public static BalanceManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        UpdateAmountOfValuteOnGUI();
    }

    public void CreateNewWallet()
    {
        RublesWallet = 20000f;
    }

    public void UpdateBalance()
    {
        foreach (Valute val in Valutes)
        {
            val.UpdatePrice();
        }
    }

    public void GenerateValutesList()
    {
        Valutes.Add(new Valute("Dollars", '$', false));
        Valutes.Add(new Valute("Rubles", 'P'));
        Valutes.Add(new Valute("Euros", '€'));

        for (int i = 0; i < 1500; i++)
        {
            UpdateBalance();
        }
    }

    public bool BuyWith(Valute val, float amount)
    {
        if (RublesWallet>= amount  * val.GetPriceInCurrentValue() )
        {
            RublesWallet -= val.GetPriceInCurrentValue()/ Valutes[1].GetPriceInCurrentValue() * amount;
            return true;
        }

        return false;
    }
    public bool SellIn(Valute val, float sum)
    {
        if (sum > 0)
        {
            RublesWallet += val.GetPriceInCurrentValue()/ Valutes[1].GetPriceInCurrentValue() * sum;
            return true;
        }

        return false;
    }

    public void UpdateAmountOfValuteOnGUI()
    {
        Valute val = DetailedInfoManager._instance.currentValute;


        float onHands = RublesWallet *Valutes[1].GetPriceInCurrentValue()/val.GetPriceInCurrentValue() ;
        _balanceInMarket.text = onHands.ToString("00.00") + val.Symbol;
        _balanceInPortfolio.text = onHands.ToString("00.00") + val.Symbol;
    }
}

