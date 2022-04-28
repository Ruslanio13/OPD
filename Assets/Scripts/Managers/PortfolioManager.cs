using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private Button _selValutesButton;
    [SerializeField] private Button _selETFButton;
    [SerializeField] private TextMeshProUGUI _totalProfit;
    Color red = new Color(0.8f, 0.09f, 0.09f, 1);
    Color green = new Color(0.09f, 0.7f, 0.1f, 1f);

    private void Start()
    {
        GoToPortfolio.onClick.AddListener(() =>
        {
            InitializePortfolio(GameManager._instance.currentSecurity.GetType());
            UpdatePortfolio();
            if (_portfolioInUI.Count != 0)
                _portfolioInUI[0].SelectSecurity();
        });
        _selShareButton.onClick.AddListener(() =>
        {
            InitializePortfolio(typeof(Share));
            UpdatePortfolio();
            if (_portfolioInUI.Count != 0)
                _portfolioInUI[0]?.SelectSecurity();
        });
        _selObligationsButton.onClick.AddListener(() =>
        {
            InitializePortfolio(typeof(Obligation));
            UpdatePortfolio();
            if (_portfolioInUI.Count != 0)
                _portfolioInUI[0]?.SelectSecurity();
        });
        _selValutesButton.onClick.AddListener(() =>
        {
            InitializePortfolio(typeof(Valute));
            UpdatePortfolio();
            if (_portfolioInUI.Count != 0)
                _portfolioInUI[0]?.SelectSecurity();
        });
        _selETFButton.onClick.AddListener(() =>
        {
            InitializePortfolio(typeof(ETF));
            UpdatePortfolio();
            if (_portfolioInUI.Count != 0)
                _portfolioInUI[0]?.SelectSecurity();
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


        if (reqSecType == typeof(Obligation))
            infoPrefab = _obligationInfoPrefab;
        else
            infoPrefab = _shareInfoPrefab;

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

        if (BalanceManager._instance.BuyWith(GameManager._instance.currentValute, totalSum))
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
            float sum;
            if(securities.GetType() == typeof(Valute))
                sum = (securities as Valute).GetPriceInCurrentValue() * amount;
            else
                sum = amount * securities.Price;
            RemoveSecurities(securities, amount);
            BalanceManager._instance.SellIn(securities, sum);

        }
        else
            Debug.Log("Not Enough Securities");
    }

    private void AddSecurities(Securities securities, int amount)
    {
        GameObject infoPrefab;


        if (securities.GetType() == typeof(Obligation))
        {
            infoPrefab = _obligationInfoPrefab;
            Portfolio.Add(new Obligation(securities as Obligation, securities.ParentCompany.GetNameOfCompany(), amount));
        }
        else
        {
            infoPrefab = _shareInfoPrefab;
            if (securities.AmountInPortolio > 0)
            {
                securities.SetAmount(securities.AmountInPortolio + amount);
                securities.AddTransaction(amount, securities.Price / GameManager._instance.currentValute.Price);
            }
            else
            {
                Portfolio.Add(securities);
                securities.SetAmount(securities.AmountInPortolio + amount);
                securities.AddTransaction(amount, securities.Price / GameManager._instance.currentValute.Price);
            }
        }
        UpdatePortfolio();
    }

    public void RemoveSecurities(Securities securities, int amount)
    {
        if (securities.GetType() == typeof(Obligation))
        {
            Portfolio.Remove(securities);
            Destroy(FindInUIList(securities).gameObject);
            _portfolioInUI.Remove(FindInUIList(securities));
        }
        else
        {
            if (securities.AmountInPortolio > 0)
            {
                if (amount == securities.AmountInPortolio)
                {
                    Portfolio.Remove(securities);
                    Destroy(FindInUIList(securities).gameObject);
                    _portfolioInUI.Remove(FindInUIList(securities));
                    securities.SetAmount(0);
                }
                else
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
        UpdatePortfolio();
    }


    public void UpdatePortfolio()
    {
        float sellNowTotal = 0f;
        float spendTotal = 0f;

        foreach (PortfolioShortInfo sec in _portfolioInUI)
        {
            sec.UpdateInfo();

            spendTotal += sec.GetSpendMoney();
            sellNowTotal += (sec.securities.AmountInPortolio * sec.securities.Price);
        }
        Debug.Log(spendTotal);
        Debug.Log(sellNowTotal);
        Debug.Log(BalanceManager._instance.GetWalletInCurrentValute());
        float _total = ((sellNowTotal - spendTotal) / (BalanceManager._instance.GetWalletInCurrentValute() + spendTotal) * 100f);
        _totalProfit.text = _total.ToString("0.00") + "%";
        if (_total > 0f)
            _totalProfit.color = green;
        else if (_total < 0f)
            _totalProfit.color = red;
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
