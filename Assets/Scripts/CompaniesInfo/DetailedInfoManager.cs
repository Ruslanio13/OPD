using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedInfoManager : MonoBehaviour
{

    [SerializeField] public List<Company> Companies = new List<Company>();
    [SerializeField] private Transform _shortInfoListTransform;
    [SerializeField] public Graph _graph;
    [SerializeField] DetailedInfo table;
    [SerializeField] private GameObject _shortInfoPrefab;
    public int currentIndex;
    public Company currentCompany;
    public Securities currentSecurity;
    void Awake()
    {
        if (_instance == null)
            _instance = this;       
    }

    private void Start() {
        foreach (Company comp in Companies)
        {
            comp.SetCompanyToSecurities();
        }
        
        UpdateAllInformation(Companies[0]);        
        SetMarket(Companies);
        FindObjectOfType<NewsManager>().SpawnNews(Companies);
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

    public void UpdateAllInformation(Company company)
    {
        currentCompany = company;
        currentSecurity = company.CompanyShare;
        table.SetInfo(company);
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
            }
            _graph.ResetPosition();
            _graph.UpdateGraph();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _graph.EnlargeScale();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            _graph.LessenScale();

    }

    public void SetMarket(List<Company> market)
    {
        GameObject temp;
        for (int i = 0; i < market.Count; i++)
        {
            temp = Instantiate(_shortInfoPrefab, _shortInfoListTransform);
            temp.GetComponent<ShortInfo>().SetInfo(market[i]);
        }
    }




}
