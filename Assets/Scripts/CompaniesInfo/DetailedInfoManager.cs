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
    [SerializeField] private GameObject _shortInfoPrefab;
    [SerializeField] private Button _selRubButton;
    [SerializeField] private Button _selEurButton;
    [SerializeField] private Button _selDolButton;
    [SerializeField] private TextMeshProUGUI _calendarTxt;

    [SerializeField] private Button _selSharesMarket;
    [SerializeField] private Button _selValuteMarket;
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

    }

    public static DetailedInfoManager _instance;

    public void UpdateAllInformation(Securities sec)
    {
        _calendarTxt.text = Calendar.GetStrDate();
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
            PortfolioManager._instance.UpdatePortfolio();
            BalanceManager._instance.UpdateBalance();

            
            foreach (Company comp in Companies)          
                comp.UpdatePrice();       
            
            foreach (ShortInfo info in _displayedSecurities)          
                info.UpdateInfo();           
            Calendar.UpdateDate();
            if (Calendar.IsTimeToDividends())
                Instantiate(_notification);

            UpdateAllInformation(currentSecurity);


        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _graph.EnlargeScale();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            _graph.LessenScale();

    }

    public void SetSecuritiesMarket()
    {
        GameObject temp;
        ShortInfo temp2;
        foreach (ShortInfo info in _displayedSecurities)
        {
            Destroy(info.gameObject);
        }
        _displayedSecurities.Clear();

        SelectSecurity(SecMarket[0]);
        _shortInfoListTransform.sizeDelta = Vector2.zero;
        Debug.Log(SecMarket.Count);
        for (int i = 0; i < SecMarket.Count; i++)
        {
            if (SecMarket[i].GetName() == currentValute.GetName())
            {
                if (i == 0)
                    SelectSecurity(SecMarket[1]);
                continue;
            }

            temp = Instantiate(_shortInfoPrefab, _shortInfoListTransform);
            temp2 = temp.GetComponent<ShortInfo>();
            temp2.SetInfo(SecMarket[i]);
            _displayedSecurities.Add(temp.GetComponent<ShortInfo>());
            _shortInfoListTransform.sizeDelta += new Vector2(0, 65f);
        }
        UpdateAllInformation(currentSecurity);
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

        SelectSecurity(market[0]);
        _shortInfoListTransform.sizeDelta = Vector2.zero;
        Debug.Log(market.Count);
        for (int i = 0; i < market.Count; i++)
        {
            if (market[i].GetName() == currentValute.GetName())
            {
                if(i == 0)
                    SelectSecurity(market[1]);
                continue;
            }

            temp = Instantiate(_shortInfoPrefab, _shortInfoListTransform);
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
            if (reqType == typeof(Future))
            {
                SecMarket.Add(comp.CompanyFuture);
                continue;
            }
        }
    }


}
