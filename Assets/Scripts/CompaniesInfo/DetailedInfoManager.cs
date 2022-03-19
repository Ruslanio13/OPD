using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedInfoManager : MonoBehaviour
{

    [SerializeField] public List<Company> companies = new List<Company>();
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
        SaveManager._instance.Load();
        currentCompany = companies[0];
        
        currentSecurity = companies[0].CompanyShare;
        Debug.Log(currentSecurity.GetType());
        Debug.Log(typeof(Share));

        currentIndex = 0;

        SetMarket(companies);
    }



    public void InitializeCompanies()
    {
        companies.Add(new Company("Sberbank"));
        companies.Add(new Company("VTB"));
        companies.Add(new Company("Tinkoff"));
        companies.Add(new Company("Raiffaizen"));
        companies.Add(new Company("Amazon"));
        companies.Add(new Company("Asos"));
        companies.Add(new Company("DNS"));
        companies.Add(new Company("Samsung"));
        companies.Add(new Company("Apple"));
        companies.Add(new Company("Xiaomi"));
        companies.Add(new Company("Meizu"));
        companies.Add(new Company("LG"));
        companies.Add(new Company("Lenovo"));
        companies.Add(new Company("JBL"));
        companies.Add(new Company("Oppo"));
        companies.Add(new Company("Phillips"));
        companies.Add(new Company("Sony"));

    }

    public static DetailedInfoManager _instance;

    public void UpdateAllInformation(Company company)
    {
        currentCompany = company;
        currentSecurity = company.CompanyShare;
        table.SetInfo(company);

        _graph.UpdateGraph();

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (Company comp in companies)
            {
                comp.GeneratePreGameHistory();
            }
            //_graph.UpdateGraph();
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
            temp = Instantiate(_shortInfoPrefab, transform);
            temp.GetComponent<ShortInfo>().SetInfo(market[i]);
            Debug.Log(market[i].GetNameOfCompany());
        }
    }




}
