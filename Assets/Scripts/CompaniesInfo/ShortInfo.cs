using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.Events;
using System;

public class ShortInfo : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] int index;
    [SerializeField] TextMeshProUGUI companyName;
    [SerializeField] TextMeshProUGUI percentOfChange;  
    [SerializeField] TextMeshProUGUI price;
    CompanySO company;
    DetailedInfoManager companies;
    DetailedInfoManager setCompanies;
    Color red = new Color(0.8f, 0.09f, 0.09f, 1);
    Color green = new Color(0.09f, 0.7f, 0.1f, 1f);
    float previousPrice;
    double deltaPrice;

    void Awake() 
    {
        setCompanies = FindObjectOfType<DetailedInfoManager>();
    }

    void Start() 
    {

        company = DetailedInfoManager._instance.companies[index];
        button.onClick.AddListener(()=>{DetailedInfoManager._instance.UpdateAllInformation(index);});
        companyName.text = company.returnNameOfCompany();
        price.text = company.GetPrice().ToString();
        previousPrice = company.GetPrice();
    }
    private void Update()
    {
        
        if(previousPrice > company.GetPrice())
        {
            percentOfChange.color = red;
            
            deltaPrice = Math.Round((previousPrice/company.GetPrice()-1)*100,2);
            percentOfChange.text = deltaPrice.ToString()+"%";
        }
        else if(previousPrice < company.GetPrice())
        {
            percentOfChange.color = green;
            
            deltaPrice = Math.Round((company.GetPrice()/previousPrice-1)*100, 2);
            percentOfChange.text = deltaPrice.ToString()+"%";
        }
        price.text = company.GetPrice().ToString();

        previousPrice = company.GetPrice();
       
    }

    int GetIndex() => index;

    public void SetIndex()
    {
        setCompanies.currentIndex = index;
    }

}
