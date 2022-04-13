using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortfolioManager : MonoBehaviour
{
    public static PortfolioManager _instance;
    public List<Securities> Portfolio = new List<Securities>();
    private List<PortfolioShortInfo> _portfolioInUI = new List<PortfolioShortInfo>();

    [SerializeField] private GameObject _shareInfoPrefab;
    [SerializeField] private GameObject _obligationInfoPrefab;
    [SerializeField] private Transform _portfolioParentTForm;
    [SerializeField] private Button GoToPortfolio;
    [SerializeField] private Button _selShareButton;
    [SerializeField] private Button _selObligationsButton;

    private void Start()
    {
        GoToPortfolio.onClick.AddListener(() =>
        {
            InitializePortfolio(DetailedInfoManager._instance.currentSecurity.GetType());
            if (Portfolio.Count != 0)
                DetailedInfoManager._instance.SelectSecurity(Portfolio[0]);

            UpdatePortfolio();
        });
        _selShareButton.onClick.AddListener(() =>
        {
            InitializePortfolio(typeof(Share));
            UpdatePortfolio();
        });
        _selObligationsButton.onClick.AddListener(() =>
        {
            InitializePortfolio(typeof(Obligation));
            UpdatePortfolio();
        });
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    public void InitializePortfolio(System.Type reqSecType)
    {
        GameObject tempGO;
        PortfolioShortInfo tempInfo;
        GameObject infoPrefab;

        if (reqSecType == typeof(Share))
            infoPrefab = _shareInfoPrefab;
        else if (reqSecType == typeof(Obligation))
            infoPrefab = _obligationInfoPrefab;
        else
            throw new System.Exception("Wrong sec type in portfolio");

        for (int i = 0; i < _portfolioInUI.Count; i++)
        {
            Destroy(_portfolioInUI[i].gameObject);
        }
        _portfolioInUI.Clear();
        foreach (Securities sec in Portfolio)
        {
            if (sec.GetType() == reqSecType)
            {
                tempGO = Instantiate(infoPrefab, _portfolioParentTForm);
                tempInfo = tempGO.GetComponent<PortfolioShortInfo>();
                tempInfo.SetInfo(sec);

                _portfolioInUI.Add(tempInfo);
            }
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
        if (securities.AmountInPortolio >= amount)
        {
            RemoveSecurities(securities, amount);
            BalanceManager._instance.SellIn(DetailedInfoManager._instance.currentValute, amount * securities.Price);
        }
        else
            Debug.Log("Not Enough Securities");
    }

    private void AddSecurities(Securities securities, int amount)
    {
        GameObject infoPrefab;

        if (securities.GetType() == typeof(Share))
            infoPrefab = _shareInfoPrefab;
        else if (securities.GetType() == typeof(Obligation))
            infoPrefab = _obligationInfoPrefab;
        else
            throw new System.Exception("Wrong sec type in portfolio");
        if (securities.GetType() == typeof(Share))
        {
            if (securities.AmountInPortolio > 0)
            {
                securities.SetAmount(securities.AmountInPortolio + amount);
                securities.AddTransaction(amount, securities.Price / DetailedInfoManager._instance.currentValute.Price);
            }
            else
            {
                GameObject temp;
                PortfolioShortInfo tempInfo;

                Portfolio.Add(securities);

                temp = Instantiate(infoPrefab, _portfolioParentTForm);
                tempInfo = temp.GetComponent<PortfolioShortInfo>();
                tempInfo.SetInfo(securities);

                securities.SetAmount(securities.AmountInPortolio + amount);
                securities.AddTransaction(amount, securities.Price / DetailedInfoManager._instance.currentValute.Price);

                _portfolioInUI.Add(tempInfo);
            }
        }
        else if (securities.GetType() == typeof(Obligation))
        {
            Portfolio.Add(new Obligation(securities as Obligation, securities.ParentCompany.GetNameOfCompany(), amount));
        }


        UpdatePortfolio();
    }

    public void RemoveSecurities(Securities securities, int amount)
    {
        if (securities.GetType() == typeof(Share))
        {
            if (securities.AmountInPortolio > 0)
            {
                if (amount == securities.AmountInPortolio)
                {
                    Portfolio.Remove(securities);
                    Destroy(FindInUIList(securities).gameObject);
                    _portfolioInUI.Remove(FindInUIList(securities));
                }
                securities.SetAmount(securities.AmountInPortolio - amount);
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
        }
        else if (securities.GetType() == typeof(Obligation))
        {
            Portfolio.Remove(securities);
            Destroy(FindInUIList(securities).gameObject);
            _portfolioInUI.Remove(FindInUIList(securities));
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
        for (int i = 0; i < _portfolioInUI.Count; i++)
            if (_portfolioInUI[i].securities == sec)
                return _portfolioInUI[i];

        throw new System.Exception("Sec is not presented in UIList");
    }

    public void UpdateObligations()
    {
        for (int i = Portfolio.Count - 1; i >= 0; i--)
        {
            if (Portfolio[i].GetType() == typeof(Obligation))
                (Portfolio[i] as Obligation).DecreaseDueTo();
        }
    }
}
