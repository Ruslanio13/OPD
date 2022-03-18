using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.Events;

public class ShortInfo : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] int index;
    [SerializeField] TextMeshProUGUI companyName;  
    [SerializeField] TextMeshProUGUI price;
    CompanySO company;
    DetailedInfoManager companies;
    DetailedInfoManager setCompanies;

    void Awake() 
    {
        setCompanies = FindObjectOfType<DetailedInfoManager>();
    }

    void Start() 
    {

        company = DetailedInfoManager._instance.companies[index];
        button.onClick.AddListener(()=>{DetailedInfoManager._instance.UpdateTable(index);});
        companyName.text = company.returnNameOfCompany();
        price.text = company.GetPrice().ToString();
    }

    int GetIndex() => index;

    public void SetIndex()
    {
        setCompanies.currentIndex = index;
    }

}
