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
    public Dictionary<Valute, float> Wallet = new Dictionary<Valute, float>();
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
        Wallet.Add(Valutes[0], 0);
        Wallet.Add(Valutes[1], 20000);
        Wallet.Add(Valutes[2], 0);
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
        if (Wallet[val] >= amount)
        {
            Wallet[val] -= amount;
            return true;
        }

        return false;
    }

    public void UpdateAmountOfValuteOnGUI()
    {
        Valute val = DetailedInfoManager._instance.currentValute;
        float onHands = Wallet[val];
        _balanceInMarket.text = onHands.ToString("00.00") + val.Symbol;
        _balanceInPortfolio.text = onHands.ToString("00.00") + val.Symbol;
    }
}

