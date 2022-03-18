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
    [SerializeField] TextMeshProUGUI name;  
    [SerializeField] TextMeshProUGUI price;
    CompanySO company;
    SetCompanies companies;
    SetCompanies setCompanies;

    void Awake() 
    {
        setCompanies = FindObjectOfType<SetCompanies>();
    }

    void Start() 
    {

        company = SetCompanies._instance.companies[index];
        button.onClick.AddListener(()=>{SetCompanies._instance.UpdateTable(index);});
        name.text = company.returnNameOfCompany();
        price.text = company.returnPrice().ToString();
    }

    int GetIndex() => index;

    public void SetIndex()
    {
        setCompanies.currentIndex = index;
    }

}
