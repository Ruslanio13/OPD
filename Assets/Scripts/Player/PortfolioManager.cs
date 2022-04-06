using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortfolioManager : MonoBehaviour
{
    public static PortfolioManager _instance;
    public List<Securities> Portfolio = new List<Securities>();
    private List<PortfolioShortInfo> _portfolioInUI = new List<PortfolioShortInfo>();

    [SerializeField] private GameObject _securitiesPrefab;
    [SerializeField] private Transform _portfolioParentTForm;
    [SerializeField] private Button GoToPortfolio;

    private void Start()
    {
        InitializePortfolio();
        GoToPortfolio.onClick.AddListener(() => {
            UpdatePortfolio();
            DetailedInfoManager._instance.CreateSecuritiesMarket(new Share());
            DetailedInfoManager._instance.SetSecuritiesMarket();
        });
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    public void InitializePortfolio()
    {
        GameObject tempGO;
        PortfolioShortInfo tempInfo;
        foreach (Securities sec in Portfolio)
        {
            Debug.Log("In Portfolio " + sec.ParentCompany.GetNameOfCompany() + " " + sec.Amount);
            tempGO = Instantiate(_securitiesPrefab, _portfolioParentTForm);
            tempInfo = tempGO.GetComponent<PortfolioShortInfo>();
            tempInfo.SetInfo(sec);

            _portfolioInUI.Add(tempInfo);
        }
    }

    public void BuySecurities(Securities securities, int amount)
    {
        float totalSum;

        totalSum = (securities.GetType() == typeof(Valute)) ? amount * (securities as Valute).GetPriceInCurrentValue() : amount * securities.Price;

        if (BalanceManager._instance.BuyWith(DetailedInfoManager._instance.currentValute, totalSum))
        {
            AddSecurities(securities, amount);
        }
        else
            Debug.Log("Not Enough Money");
    }
    public void SellSecurities(Securities securities, int amount)
    {
        if (securities.GetType() == typeof(Valute))
        {
            if (BalanceManager._instance.Wallet[(Valute)securities] >= amount)
            {
                BalanceManager._instance.SellIn(DetailedInfoManager._instance.currentValute, amount * (securities as Valute).GetPriceInCurrentValue());
                BalanceManager._instance.RemoveValuteFromWallet((Valute)securities, amount);
            }
            else
                Debug.Log("NotEnoughValute");
            return;
        }

        if (securities.Amount >= amount)
        {
            RemoveSecurities(securities, amount);
            BalanceManager._instance.SellIn(DetailedInfoManager._instance.currentValute, amount * securities.Price);
        }
        else
            Debug.Log("Not Enough Securities");
    }

    private void AddSecurities(Securities securities, int amount)
    {
        if (securities.GetType() == typeof(Valute))
        {
            BalanceManager._instance.AddValuteToWallet((Valute)securities, amount);
            return;
        }
        if (securities.Amount > 0)
        {
            securities.SetAmount(securities.Amount + amount);
            securities.AddTransaction(amount, securities.Price / DetailedInfoManager._instance.currentValute.Price);
        }
        else
        {
            GameObject temp;
            PortfolioShortInfo tempInfo;

            Portfolio.Add(securities);

            temp = Instantiate(_securitiesPrefab, _portfolioParentTForm);
            tempInfo = temp.GetComponent<PortfolioShortInfo>();
            tempInfo.SetInfo(securities);

            securities.SetAmount(securities.Amount + amount);
            securities.AddTransaction(amount, securities.Price / DetailedInfoManager._instance.currentValute.Price);

            _portfolioInUI.Add(tempInfo);
        }
        UpdatePortfolio();
    }

    private void RemoveSecurities(Securities securities, int amount)
    {
        if (securities.Amount > 0)
        {
            if (amount == securities.Amount)
            {
                Portfolio.Remove(securities);
                Destroy(FindInUIList(securities).gameObject);
                _portfolioInUI.Remove(FindInUIList(securities));
            }
            securities.SetAmount(securities.Amount - amount);
            int currentAmount = amount;
            int j = securities.TransHistory.Count - 1;
            while (currentAmount != 0)
            {
                if (securities.TransHistory[j][0] >= currentAmount)
                {
                    securities.TransHistory[j][0] -= currentAmount;
                    currentAmount = 0;
                }
                else
                {
                    currentAmount -= System.Convert.ToInt32(securities.TransHistory[j][0]);
                    securities.TransHistory[j][0] = 0;
                }
                if (securities.TransHistory[j][0] == 0)
                    securities.TransHistory.RemoveAt(j);
                j--;
            }
        }
        else
        {
            throw new System.Exception("Cannot remove sec-s due to their absense");
        }
        UpdatePortfolio();
    }


    public void UpdatePortfolio()
    {
        foreach (PortfolioShortInfo sec in _portfolioInUI)
        {
            sec.UpdateInfo();
        }
    }

    public PortfolioShortInfo FindInUIList(Securities sec)
    {
        foreach (PortfolioShortInfo info in _portfolioInUI)
            if (info.securities == sec)
                return info;

        throw new System.Exception("Sec is not presented in UIList");
    }
}