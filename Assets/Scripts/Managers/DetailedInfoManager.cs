using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailedInfoManager : MonoBehaviour
{

    [SerializeField] public List<Company> Companies = new List<Company>();
    [SerializeField] private List<Securities> SecMarket = new List<Securities>();
    [SerializeField] private RectTransform _shortInfoListTransform;
    [SerializeField] public Graph _graph;
    [SerializeField] DetailedInfo table;
    [SerializeField] GameObject _tableGameObject;
    [SerializeField] private GameObject _shareInfoPrefab;
    [SerializeField] private GameObject _obligationInfoPrefab;
    [SerializeField] private GameObject _notification;
    [SerializeField] private GameObject _notificationPanel;

    [SerializeField] private Button _selRubButton;
    [SerializeField] private Button _selEurButton;
    [SerializeField] private Button _selDolButton;
    [SerializeField] private TextMeshProUGUI _marketCalendarTxt;
    [SerializeField] private TextMeshProUGUI _portfolioCalendarTxt;

    [SerializeField] private Button _selSharesMarket;
    [SerializeField] private Button _selObligationMarket;
    [SerializeField] private Button _selValuteMarket;
    [SerializeField] private Button _selETFMarket;
    private List<ShortInfo> _displayedSecurities = new List<ShortInfo>();
    public int currentIndex;
    public Company currentCompany;
    public Securities currentSecurity;
    public Valute currentValute;
    public Calendar Calendar;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        foreach (Company comp in Companies)
        {
            comp.SetCompanyToSecurities();
        }


        _selDolButton.onClick.AddListener(() => SetValute(BalanceManager._instance.Valutes[0]));
        _selRubButton.onClick.AddListener(() => SetValute(BalanceManager._instance.Valutes[1]));
        _selEurButton.onClick.AddListener(() => SetValute(BalanceManager._instance.Valutes[2]));



        _selSharesMarket.onClick.AddListener(() =>
        {
            SetValute(BalanceManager._instance.Valutes[0]);
            CreateSecuritiesMarket(new Share());
            SetSecuritiesMarket(SecMarket);
            _tableGameObject.SetActive(true);
        });
        _selObligationMarket.onClick.AddListener(() =>
        {
            SetValute(BalanceManager._instance.Valutes[0]);
            CreateSecuritiesMarket(new Obligation());
            SetSecuritiesMarket(SecMarket);
            _tableGameObject.SetActive(false);
        });
        _selValuteMarket.onClick.AddListener(() =>
        {
            SetValute(BalanceManager._instance.Valutes[0]);
            SetSecuritiesMarket(BalanceManager._instance.Valutes);
            _tableGameObject.SetActive(false);
        });
        _selETFMarket.onClick.AddListener(() =>
        {
            SetValute(BalanceManager._instance.Valutes[0]);
            CreateSecuritiesMarket(new ETF());
            SetSecuritiesMarket(SecMarket);
        });


        currentValute = BalanceManager._instance.Valutes[0];

        CreateSecuritiesMarket(new Share());
        SetSecuritiesMarket(SecMarket);



        UpdateAllInformation(SecMarket[0]);

    }



    public void InitializeCompanies()
    {
        Calendar = new Calendar(1, 1, 2022);

        Companies.Add(new Company("Sberbank"));
        Companies.Add(new Company("VTB"));
        Companies.Add(new Company("Tinkoff"));
        Companies.Add(new Company("Raiffaizen"));
        Companies.Add(new Company("Amazon"));
        Companies.Add(new Company("Asos"));
        Companies.Add(new Company("DNS"));
        Companies.Add(new Company("Samsung"));
        Companies.Add(new Company("Apple"));
        Companies.Add(new Company("Xiaomi"));
        Companies.Add(new Company("Meizu"));
        Companies.Add(new Company("LG"));
        Companies.Add(new Company("Lenovo"));
        Companies.Add(new Company("JBL"));
        Companies.Add(new Company("Oppo"));
        Companies.Add(new Company("Phillips"));
        Companies.Add(new Company("Sony"));

        foreach (var comp in Companies)
        {
            comp.InitializeETF();
        }

    }

    public static DetailedInfoManager _instance;

    public void UpdateAllInformation(Securities sec)
    {
        _marketCalendarTxt.text = Calendar.GetStrDate();
        _portfolioCalendarTxt.text = Calendar.GetStrDate();
        SelectSecurity(sec);

        foreach (ShortInfo info in _displayedSecurities)
        {
            info.UpdateInfo();
        }

        table.SetInfo(sec);
        _graph.ResetPosition();
        _graph.UpdateGraph();
        BalanceManager._instance.UpdateAmountOfValuteOnGUI();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Calendar.AllDays % 10 == 0)
                NewsManager._instance.SpawnNews(Companies);

            PortfolioManager._instance.UpdateObligations();
            PortfolioManager._instance.UpdatePortfolio();
            BalanceManager._instance.UpdateBalance();
            Calendar.UpdateDate();


            foreach (Company comp in Companies)
                comp.UpdatePrice();

            foreach (ShortInfo info in _displayedSecurities)
                info.UpdateInfo();



            Debug.Log(currentSecurity.GetName());
            if (Calendar.IsTimeToDividends())
            {
                GameObject notification;
                GameObject notificationPanel;
                List<Securities> portfolio = PortfolioManager._instance.Portfolio;
                for (int i = 0; i < PortfolioManager._instance.Portfolio.Count; i++)
                {
                    if (portfolio[i].GetType() == typeof(Share))
                    {
                        notification = Instantiate(_notification);
                        notification.GetComponent<OkButton>().SetInfo(portfolio[i].ParentCompany.GetNameOfCompany(), (portfolio[i] as Share).GetSumOfDividends());
                        (portfolio[i] as Share).PayDividends();
                        if (i == 0)
                            notificationPanel = Instantiate(_notificationPanel, notification.transform);
                    }
                }
            }
            UpdateAllInformation(currentSecurity);


        }
    }
    public void SetSecuritiesMarket<T>(List<T> market) where T : Securities
    {
        GameObject temp;
        GameObject infoPrefab;

        ShortInfo temp2;

        if (market[0].GetType() == typeof(Obligation))
            infoPrefab = _obligationInfoPrefab;
        else
            infoPrefab = _shareInfoPrefab;


        foreach (ShortInfo info in _displayedSecurities)
        {
            Destroy(info.gameObject);
        }
        _displayedSecurities.Clear();

        SelectSecurity(market[0]);

        _shortInfoListTransform.sizeDelta = Vector2.zero;

        Debug.Log(market.Count);
        for (int i = 0; i < market.Count; i++)
        {
            if (market[i].GetName() == currentValute.GetName())
            {
                if (i == 0)
                    SelectSecurity(market[1]);
                continue;
            }

            temp = Instantiate(infoPrefab, _shortInfoListTransform);
            temp2 = temp.GetComponent<ShortInfo>();
            temp2.SetInfo(market[i]);
            _displayedSecurities.Add(temp.GetComponent<ShortInfo>());
            _shortInfoListTransform.sizeDelta += new Vector2(0, 65f);
        }
        UpdateAllInformation(currentSecurity);
    }
    public void SelectSecurity(Securities sec)
    {
        currentSecurity = sec;
        currentCompany = sec.ParentCompany;
        NewsManager._instance.ShowCompanyNews(sec.ParentCompany);
    }
    public void SetValute(Valute val)
    {

        if (currentSecurity.GetType() == typeof(Valute))
        {
            currentValute = val;
            Debug.Log(currentValute.GetName());
            SetSecuritiesMarket(BalanceManager._instance.Valutes);
        }
        else
        {
            foreach (Securities sec in SecMarket)
            {
                sec.RecalculateHistoryForValute(val, currentValute);
            }
            currentValute = val;
        }

        BalanceManager._instance.UpdateAmountOfValuteOnGUI();

        PortfolioManager._instance.UpdatePortfolio();

        UpdateAllInformation(currentSecurity);
    }



    public void CreateSecuritiesMarket<T>(T sec) where T : Securities
    {
        System.Type reqType = typeof(T);

        SecMarket.Clear();
        if (reqType == typeof(Valute))
        {
            foreach (Valute val in BalanceManager._instance.Valutes)
                SecMarket.Add(val);
            return;
        }



        foreach (Company comp in Companies)
        {
            if (reqType == typeof(Share))
            {
                SecMarket.Add(comp.CompanyShare);
                continue;
            }
            if (reqType == typeof(Obligation))
            {
                SecMarket.Add(comp.CompanyObligation);
                continue;
            }
            if (reqType == typeof(ETF))
            {
                SecMarket.Add(comp.CompanyETF);
                continue;
            }
        }
    }


}
