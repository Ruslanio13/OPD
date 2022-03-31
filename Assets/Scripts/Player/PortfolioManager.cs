using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortfolioManager : MonoBehaviour
{
    public static PortfolioManager _instance;
    public Dictionary<Securities, int> Portfolio = new Dictionary<Securities, int>();
    private Dictionary<Securities, PortfolioShortInfo> _portfolioInUI = new Dictionary<Securities, PortfolioShortInfo>();

    [SerializeField] private GameObject _securitiesPrefab;
    [SerializeField] private Transform _portfolioParentTForm;

    private void Start()
    {
        InitializePortfolio();
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
        foreach (KeyValuePair<Securities, int> sec in Portfolio)
        {
            Debug.Log("In Portfolio " + sec.Key.ParentCompany.GetNameOfCompany() + " " + sec.Value);
            tempGO = Instantiate(_securitiesPrefab, _portfolioParentTForm);
            tempInfo = tempGO.GetComponent<PortfolioShortInfo>();
            tempInfo.SetInfo(sec.Key, sec.Value);
            
            _portfolioInUI.Add(sec.Key, tempInfo);
        }
    }

    public void BuySecurities(Securities securities, int amount)
    {
        float totalSum;
        
        totalSum = (securities.GetType() == typeof(Valute)) ? amount*securities.GetPriceInCurrentValue() : amount*securities.Price;

        if (BalanceManager._instance.BuyWith(DetailedInfoManager._instance.currentValute,totalSum))
        {
            AddSecurities(securities, amount);
        }
        else
            Debug.Log("Not Enough Money");
    }
    public void SellSecurities(Securities securities, int amount)
    {
        if(securities.GetType() == typeof(Valute))
        {
            if(BalanceManager._instance.Wallet[(Valute)securities] >= amount)
            {
                BalanceManager._instance.SellIn(DetailedInfoManager._instance.currentValute, amount*securities.GetPriceInCurrentValue());
                BalanceManager._instance.RemoveValuteFromWallet((Valute)securities, amount);
            }
            else
                Debug.Log("NotEnoughValute");
            return;
        }
        
        if (Portfolio[securities] >= amount)
        {
            RemoveSecurities(securities, amount);
            BalanceManager._instance.SellIn(DetailedInfoManager._instance.currentValute, amount*securities.Price);
        }
        else
            Debug.Log("Not Enough Securities");
    }

    private void AddSecurities(Securities securities, int amount)
    {
        if(securities.GetType() == typeof(Valute))
        {
            BalanceManager._instance.AddValuteToWallet((Valute)securities, amount);

            return;
        }
        if (Portfolio.ContainsKey(securities))
        {
            Portfolio[securities] += amount;
            _portfolioInUI[securities].SetAmount(Portfolio[securities]);
        }
        else
        {
            GameObject temp;
            PortfolioShortInfo tempInfo;

            Portfolio.Add(securities, amount);
            temp = Instantiate(_securitiesPrefab, _portfolioParentTForm);
            tempInfo = temp.GetComponent<PortfolioShortInfo>();
            tempInfo.SetInfo(securities, amount);

            _portfolioInUI.Add(securities, tempInfo);
        }
    }
    private void RemoveSecurities(Securities securities, int amount)
    {
        if (Portfolio.ContainsKey(securities))
        {
            if (amount == Portfolio[securities])
            {
                Portfolio.Remove(securities);

                Destroy(_portfolioInUI[securities].gameObject);
                _portfolioInUI.Remove(securities);
            }
            else
            {
                Portfolio[securities] -= amount;
                _portfolioInUI[securities].SetAmount(Portfolio[securities]);
            }
        }
        else
        {
            throw new System.Exception("Cannot remove sec-s due to their absense");
        }
    }


    public void UpdatePortfolio()
    {            
        foreach (KeyValuePair<Securities, PortfolioShortInfo> sec in _portfolioInUI)
        {
            sec.Value.UpdateInfo();
        }
    }
}

