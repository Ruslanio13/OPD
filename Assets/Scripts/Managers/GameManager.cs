using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] public List<Company> Companies = new List<Company>();
    [SerializeField] public List<Country> Countries = new List<Country>();
    [SerializeField] private List<Securities> SecMarket = new List<Securities>();
    [SerializeField] private RectTransform _shortInfoListTransform;
    [SerializeField] public Graph _graph;
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
    [SerializeField] private Button _selRussianMarket;
    [SerializeField] private Button _selWorldMarket;
    private List<ShortInfo> _displayedSecurities = new List<ShortInfo>();
    public int CurrentIndex {get; private set;}
    public Company CurrentCompany {get; private set;}
    public Securities CurrentSecurity {get; private set;}
    public Valute CurrentValute {get; private set;}
    public Country CurrentCountry {get; private set;}
    public Calendar Calendar;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        foreach (Company comp in Companies)
            comp.SetCompanyToSecurities();


        _selDolButton.onClick.AddListener(() => SetValute(BalanceManager._instance.Valutes[0]));
        _selRubButton.onClick.AddListener(() => SetValute(BalanceManager._instance.Valutes[1]));
        _selEurButton.onClick.AddListener(() => SetValute(BalanceManager._instance.Valutes[2]));

        _selRussianMarket.onClick.AddListener(() => 
        {
            CurrentCountry = Countries[0];
            CreateSecuritiesMarket(CurrentSecurity);
        });
        _selWorldMarket.onClick.AddListener(() => 
        {
            CurrentCountry = Countries[1];
            CreateSecuritiesMarket(CurrentSecurity);
        });

        _selSharesMarket.onClick.AddListener(() =>
        {
            SetValute(BalanceManager._instance.Valutes[0]);
            CreateSecuritiesMarket(new Share());
            DetailedInfoManager._instance.SetState(CurrentSecurity);
        });
        _selObligationMarket.onClick.AddListener(() =>
        {
            SetValute(BalanceManager._instance.Valutes[0]);
            CreateSecuritiesMarket(new Obligation());
            DetailedInfoManager._instance.SetState(CurrentSecurity);
        });
        _selValuteMarket.onClick.AddListener(() =>
        {
            SetValute(BalanceManager._instance.Valutes[0]);
            SetSecuritiesMarket(BalanceManager._instance.Valutes);
            DetailedInfoManager._instance.SetState(CurrentSecurity);
        });
        _selETFMarket.onClick.AddListener(() =>
        {
            SetValute(BalanceManager._instance.Valutes[0]);
            CreateSecuritiesMarket(new ETF());
            DetailedInfoManager._instance.SetState(CurrentSecurity);
        });



        CurrentValute = BalanceManager._instance.Valutes[0];
        CurrentCountry = Countries[0];

        CreateSecuritiesMarket(new Share());

        UpdateAllInformation(SecMarket[0]);

        StartCoroutine(ChangeDays());
    }




    public void InitializeCountries()
    {
        Countries.Add(new Country("Россия", Countries.Count));                           //0
        Countries.Add(new Country("США", Countries.Count));              //1
        Countries.Add(new Country("Украина", Countries.Count));              //2
        Countries.Add(new Country("Китай", Countries.Count));                //3
        Countries.Add(new Country("Таджикистан", Countries.Count));               //4
        Countries.Add(new Country("Германия", Countries.Count));              //5
        Countries.Add(new Country("Чешская республика", Countries.Count));               //6
        Countries.Add(new Country("Швейцария", Countries.Count));              //7
        Countries.Add(new Country("Казахстан", Countries.Count));               //8
        Countries.Add(new Country("Швеция", Countries.Count));               //9
        Countries.Add(new Country("Япония", Countries.Count));                //10
        Countries.Add(new Country("Великобритания", Countries.Count));               //11
        Countries.Add(new Country("Южная Корея", Countries.Count));              //12
    }
    public void InitializeCompanies(List<Company> companies)
    {
        Calendar = new Calendar(1, 1, 2022);
        for(int i = 0; i < companies.Count; i++)
            Companies.Add(new Company(companies[i]));
/*     TODO : Удалить коммент когда задание начальных условий будет стабильно работать

        Companies.Add(new Company("Sberbank", Countries[4]));
        Companies.Add(new Company("VTB", Countries[0]));
        Companies.Add(new Company("Tinkoff", Countries[0]));
        Companies.Add(new Company("Raiffaizen", Countries[5]));
        Companies.Add(new Company("Amazon", Countries[1]));
        Companies.Add(new Company("Asos", Countries[1]));
        Companies.Add(new Company("DNS", Countries[0]));
        Companies.Add(new Company("Samsung", Countries[12]));
        Companies.Add(new Company("Apple", Countries[1]));
        Companies.Add(new Company("Xiaomi", Countries[3]));
        Companies.Add(new Company("Meizu", Countries[3]));
        Companies.Add(new Company("LG", Countries[12]));
        Companies.Add(new Company("Lenovo", Countries[3]));
        Companies.Add(new Company("JBL", Countries[1]));
        Companies.Add(new Company("Oppo", Countries[3]));
        Companies.Add(new Company("Phillips", Countries[5]));
        Companies.Add(new Company("Sony", Countries[10]));
*/
        foreach (var comp in Companies)
            comp.InitializeETF();
    }

    public static GameManager _instance;

    public void UpdateAllInformation(Securities sec)
    {
        _marketCalendarTxt.text = Calendar.GetStrDate();
        _portfolioCalendarTxt.text = Calendar.GetStrDate();
        SelectSecurity(sec);

        foreach (ShortInfo info in _displayedSecurities)
        {
            info.UpdateInfo();
        }

        _graph.ResetPosition();
        BalanceManager._instance.UpdateAmountOfValuteOnGUI();
    }

    private IEnumerator ChangeDays()
    {
        while (true)
        {
            Calendar.UpdateDate();

            if (Calendar.AllDays % 10 == 0)
                NewsManager._instance.SpawnCompanyNews(Companies);
            if (Calendar.AllDays % 21 == 0)
                NewsManager._instance.SpawnGlobalNews(Countries);
            if (Calendar.Day == 10)
                BalanceManager._instance.PayForMaintenance();

            PortfolioManager._instance.UpdateObligations();
            BalanceManager._instance.UpdateBalance();


            foreach (Company comp in Companies)
                comp.UpdatePrice();

            foreach (ShortInfo info in _displayedSecurities)
                info.UpdateInfo();

            PortfolioManager._instance.UpdatePortfolio();


            if (Calendar.IsTimeToDividends())
            {
                GameObject notification;
                GameObject notificationPanel;
                List<Securities> portfolio = PortfolioManager._instance.Portfolio;
                for (int i = 0; i < PortfolioManager._instance.Portfolio.Count; i++)
                {
                    if (portfolio[i].GetType() == typeof(Share))
                    {
                        notification = CreateNotification(portfolio[i]);
                        if (i == 0)
                            notificationPanel = Instantiate(_notificationPanel, notification.transform);
                    }
                }
            }
            UpdateAllInformation(CurrentSecurity);

            if (Calendar.Month % 3 == 1 && Calendar.Day == 1 && (Calendar.Month != 1 || Calendar.Year != 2022))
            {
                GameObject notification;
                GameObject notificationPanel;
                notification = CreateNotification(null);
                notificationPanel = Instantiate(_notificationPanel, notification.transform);
                notification.GetComponent<NotificationButton>().SetInfo();
                break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public GameObject CreateNotification(Securities sec)
    {
        GameObject notification = Instantiate(_notification);
        if (sec?.GetType() == typeof(Share))
        {
            notification.GetComponent<NotificationButton>().SetInfo(sec, (sec as Share).GetSumOfDividends());
            (sec as Share).PayDividends();

        }
        else if (sec?.GetType() == typeof(Obligation))
            notification.GetComponent<NotificationButton>().SetInfo(sec, (sec as Obligation).PaybackCost / 2);
        else
            notification.GetComponent<NotificationButton>().SetInfo();
        return notification;
    }

    public void SetSecuritiesMarket<T>(List<T> market) where T : Securities
    {
        GameObject temp;
        GameObject infoPrefab;
        ShortInfo temp2;

        if(market.Count == 0)
            throw new System.Exception("No companies on the selected market");

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

        for (int i = 0; i < market.Count; i++)
        {
            if (market[i].GetName() == CurrentValute.GetName())
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
        UpdateAllInformation(CurrentSecurity);
    }
    public void SelectSecurity(Securities sec)
    {
        CurrentSecurity = sec;
        CurrentCompany = sec.ParentCompany;
        DetailedInfoManager._instance.UpdateDetailedInfo();
        NewsManager._instance.ShowCompanyNews(sec.ParentCompany);
    }
    public void SetValute(Valute val)
    {

        if (CurrentSecurity.GetType() == typeof(Valute))
        {
            CurrentValute = val;
            SetSecuritiesMarket(BalanceManager._instance.Valutes);
        }
        else
        {
            foreach(Company comp in Companies)
            {
                comp.CompanyShare.RecalculateHistoryForValute(val, CurrentValute);
                comp.CompanyETF.RecalculateHistoryForValute(val, CurrentValute);

            }

            
            CurrentValute = val;

        }

        BalanceManager._instance.UpdateAmountOfValuteOnGUI();

        PortfolioManager._instance.UpdatePortfolio();

        UpdateAllInformation(CurrentSecurity);
    }



    public void CreateSecuritiesMarket<T>(T sec) where T : Securities
    {

        SecMarket.Clear();
        if (sec is Valute)
        {
            foreach (Valute val in BalanceManager._instance.Valutes)
                SecMarket.Add(val);
            
            SetSecuritiesMarket(SecMarket);
            return;
        }



        foreach (Company comp in Companies)
        {
            if(CurrentCountry.ID == 0 && comp.Country.ID != 0)
                continue;
            if(CurrentCountry.ID != 0 && comp.Country.ID == 0)
                continue;

            if (sec is Share)
            {
                SecMarket.Add(comp.CompanyShare);
                continue;
            }
            if (sec is Obligation)
            {
                SecMarket.Add(comp.CompanyObligation);
                continue;
            }
            if (sec is ETF)
            {
                SecMarket.Add(comp.CompanyETF);
                continue;
            }
        }
        SetSecuritiesMarket(SecMarket);

    }
}
