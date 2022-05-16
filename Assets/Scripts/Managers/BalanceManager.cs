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
        Valutes.Add(new Valute("доллар", '$', GameManager._instance.Countries[1],false));
        Valutes.Add(new Valute("рубль", 'P', GameManager._instance.Countries[0]));
        Valutes.Add(new Valute("евро", '€', GameManager._instance.Countries[5]));
        Valutes.Add(new Valute("фунт", '€', GameManager._instance.Countries[11]));
        Valutes.Add(new Valute("юань", '€', GameManager._instance.Countries[3]));
        Valutes.Add(new Valute("йен", '€', GameManager._instance.Countries[10]));
        Valutes.Add(new Valute("швед. крона", '€', GameManager._instance.Countries[9]));
        Valutes.Add(new Valute("чешская крона", '€', GameManager._instance.Countries[6]));
        Valutes.Add(new Valute("швейц. франк", '€', GameManager._instance.Countries[7]));
        Valutes.Add(new Valute("вон", '€', GameManager._instance.Countries[12]));
        Valutes.Add(new Valute("гривна", '€', GameManager._instance.Countries[2]));
        Valutes.Add(new Valute("тенге", '€', GameManager._instance.Countries[8]));
        Valutes.Add(new Valute("песо", '€', GameManager._instance.Countries[0]));

        for (int i = 0; i < 1500; i++)
        {
            UpdateBalance();
        }
        foreach(var item in Valutes)
        {
            item.CalculateAveragePrice();
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
            RublesWallet += GameManager._instance.CurrentValute.GetPriceInCurrentValue() / Valutes[1].GetPriceInCurrentValue() * sum;
            return true;
        }

        return false;
    }

    public void UpdateAmountOfValuteOnGUI()
    {
        Valute val = GameManager._instance.CurrentValute;


        float onHands = RublesWallet * Valutes[1].GetPriceInCurrentValue() / val.GetPriceInCurrentValue();
        _balanceInMarket.text = onHands.ToString("00.00") + val.Symbol;
        _balanceInPortfolio.text = onHands.ToString("00.00") + val.Symbol;
    }

    public void AddMoney(float amount)
    {
        if (amount > 0f)
            RublesWallet += amount;
    }

    public void PayForMaintenance()
    {
        RublesWallet -= PreGameManager._instance.CurrentBroker.Maintenance;
    }
}

