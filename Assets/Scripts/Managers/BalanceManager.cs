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

    public float GetWalletInCurrentValute() => Valutes[1].GetPriceInCurrentValue() * RublesWallet;

    public void GenerateValutesList()
    {
        Valutes.Add(new Valute("доллар", '$', false));
        Valutes.Add(new Valute("рубль", 'P'));
        Valutes.Add(new Valute("евро", '€'));
        Valutes.Add(new Valute("фунт", '€'));
        Valutes.Add(new Valute("юань", '€'));
        Valutes.Add(new Valute("йен", '€'));
        Valutes.Add(new Valute("шведская крона", '€'));
        Valutes.Add(new Valute("чешская крона", '€'));
        Valutes.Add(new Valute("швейцарский франк", '€'));
        Valutes.Add(new Valute("вон", '€'));
        Valutes.Add(new Valute("гривна", '€'));
        Valutes.Add(new Valute("тенге", '€'));
        Valutes.Add(new Valute("песо", '€'));

        for (int i = 0; i < 1500; i++)
        {
            UpdateBalance();
        }
    }

    public bool BuyWith(Valute val, float price)
    {
        if (RublesWallet >= price * val.GetPriceInCurrentValue())
        {
            RublesWallet -= val.GetPriceInCurrentValue() / Valutes[1].GetPriceInCurrentValue() * price;
            return true;
        }

        return false;
    }
    public bool SellIn(Securities sec, float sum)
    {
        if (sum > 0)
        {
            RublesWallet += DetailedInfoManager._instance.currentValute.GetPriceInCurrentValue() / Valutes[1].GetPriceInCurrentValue() * sum;
            return true;
        }

        return false;
    }

    public void UpdateAmountOfValuteOnGUI()
    {
        Valute val = DetailedInfoManager._instance.currentValute;


        float onHands = RublesWallet * Valutes[1].GetPriceInCurrentValue() / val.GetPriceInCurrentValue();
        _balanceInMarket.text = onHands.ToString("00.00") + val.Symbol;
        _balanceInPortfolio.text = onHands.ToString("00.00") + val.Symbol;
    }

    public void AddMoney(float amount)
    {
        if (amount > 0f)
            RublesWallet += amount;
    }
}

