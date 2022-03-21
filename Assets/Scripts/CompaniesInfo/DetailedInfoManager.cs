using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailedInfoManager : MonoBehaviour
{

    [SerializeField] public List<Company> Companies = new List<Company>();
    [SerializeField] private List<Securities> SecMarket = new List<Securities>();
    [SerializeField] private Transform _shortInfoListTransform;
    [SerializeField] public Graph _graph;
    [SerializeField] DetailedInfo table;
    [SerializeField] private GameObject _shortInfoPrefab;
    [SerializeField] private Button _selRubButton;
    [SerializeField] private Button _selEurButton;
    [SerializeField] private Button _selDolButton;

    [SerializeField] private Button _selSharesMarket;
    [SerializeField] private Button _selValuteMarket;
    private List<ShortInfo> _displayedSecurities = new List<ShortInfo>();
    public int currentIndex;
    public Company currentCompany;
    public Securities currentSecurity;
    public Valute currentValute;

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
        });
        _selValuteMarket.onClick.AddListener(() =>
        {
            SetValute(BalanceManager._instance.Valutes[0]);
            SetSecuritiesMarket(BalanceManager._instance.Valutes);
        });
        
        currentValute = BalanceManager._instance.Valutes[0];

        CreateSecuritiesMarket(new Share());
        SetSecuritiesMarket(SecMarket);

        UpdateAllInformation(SecMarket[0]);
    }



    public void InitializeCompanies()
    {
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

    }

    public static DetailedInfoManager _instance;

    public void UpdateAllInformation(Securities sec)
    {
        currentCompany = sec.ParentCompany;
        currentSecurity = sec;

        foreach (ShortInfo info in _displayedSecurities)
        {
            info.UpdateInfo();
        }

        table.SetInfo(sec);
        _graph.ResetPosition();
        _graph.UpdateGraph();
    }



    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            foreach (Company comp in Companies)
            {
                comp.UpdatePrice();
                PortfolioManager._instance.UpdatePortfolio();
                BalanceManager._instance.UpdateBalance();

                foreach (ShortInfo info in _displayedSecurities)
                {
                    info.UpdateInfo();
                }
            }
            _graph.ResetPosition();
            _graph.UpdateGraph();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _graph.EnlargeScale();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            _graph.LessenScale();

    }

    public void SetSecuritiesMarket<T>(List<T> market) where T : Securities
    {
        GameObject temp;
        ShortInfo temp2;
        foreach (ShortInfo info in _displayedSecurities)
        {
            Destroy(info.gameObject);
        }
        _displayedSecurities.Clear();

        table.SetTableActive(true);

        Debug.Log(market.Count);
        for (int i = 0; i < market.Count; i++)
        {
            if (market[i].GetName() == currentValute.GetName())
                continue;

            temp = Instantiate(_shortInfoPrefab, _shortInfoListTransform);
            temp2 = temp.GetComponent<ShortInfo>();
            temp2.SetInfo(market[i]);
            _displayedSecurities.Add(temp.GetComponent<ShortInfo>());
        }
        foreach (ShortInfo info in _displayedSecurities)
        {
            info.UpdateInfo();
        }
    }

    public void SetValute(Valute val)
    {
        foreach (Securities sec in SecMarket)
        {
            sec.RecalculateHistoryForValute(val, currentValute);
        }

        currentValute = val;

        BalanceManager._instance.UpdateAmountOfValuteOnGUI();

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
            if (reqType == typeof(Future))
            {
                SecMarket.Add(comp.CompanyFuture);
                continue;
            }
        }
    }

}
