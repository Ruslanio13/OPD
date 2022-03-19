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
    Company company;
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


    public void SetInfo(Company comp)
    {
        company = comp;
        button.onClick.AddListener(() => { DetailedInfoManager._instance.UpdateAllInformation(company); });
        companyName.text = company.GetNameOfCompany();
        price.text = company.GetSecurityPrice().ToString();
        previousPrice = company.GetSecurityPrice();
    }

    private void Update()
    {
        
        if(previousPrice > company.GetSecurityPrice())
        {
            percentOfChange.color = red;
            
            deltaPrice = Math.Round((previousPrice/company.GetSecurityPrice()-1)*100,2);
            percentOfChange.text = deltaPrice.ToString()+"%";
        }
        else if(previousPrice < company.GetSecurityPrice())
        {
            percentOfChange.color = green;
            
            deltaPrice = Math.Round((company.GetSecurityPrice()/previousPrice-1)*100, 2);
            percentOfChange.text = deltaPrice.ToString()+"%";
        }
        price.text = company.GetSecurityPrice().ToString();

        previousPrice = company.GetSecurityPrice();
       
        companyName.text = company.GetNameOfCompany();
        price.text = DetailedInfoManager._instance.currentSecurity.GetPrice().ToString();
    }

}

