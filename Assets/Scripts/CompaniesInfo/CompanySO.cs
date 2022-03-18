using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "  CompanyInfo", fileName = "New Company")]
public class CompanySO : ScriptableObject
{   
    [TextArea(2, 6)]
    [SerializeField] string companyName = "Enter new company name here";
    [SerializeField] float price = 0f;
    [SerializeField] float averagePrice2017;
    [SerializeField] float averagePrice2018;
    [SerializeField] float averagePrice2019;
    [SerializeField] float averagePrice2020;
    [SerializeField] float averagePrice2021;
    [SerializeField] float capitalization;
    [SerializeField] int amountOfSecurities;
    [SerializeField] float profit;
    [SerializeField] int staff;
    [SerializeField] float credit;
    [SerializeField] List<Vector2> _vertices = new List<Vector2>();

    
    public float AveragePrice2017 { get => averagePrice2017;  }
    public float AveragePrice2018 { get => averagePrice2018;  }
    public float AveragePrice2019 { get => averagePrice2019;  }
    public float AveragePrice2020 { get => averagePrice2020; }
    public float AveragePrice2021 { get => averagePrice2021;  }
    public float Capitalization { get => capitalization;  }
    public int AmountOfSecurities { get => amountOfSecurities;  }
    public float Profit { get => profit; }
    public int Staff { get => staff; }
    public float Credit { get => credit;  }

    public string returnNameOfCompany() => companyName;

    public float GetPrice()=> price;
    

    public List<float> _priceHistory = new List<float>();
    

    public void UpdatePrice()
    {
        _priceHistory.Add(price);
    
        float delta =  Random.Range(-2f,2f);
        price += price * delta/100;
    }
    

    private void OnApplicationQuit() 
    {
        if(_priceHistory.Count>10)
        _priceHistory.RemoveRange(0,_priceHistory.Count-10);    
    }
    
}
