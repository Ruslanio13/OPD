using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class News : MonoBehaviour
{
   
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI mainText;
    public Company Company { get; private set; }
 

    public void SetUpNews(Company comp, NewsSO newsPattern) 
    {
        Company = comp;
        titleText.text = "Компания " + comp.GetNameOfCompany() + " " + newsPattern.title;
        mainText.text = newsPattern.text;
        comp.SetMaxPriceChange(newsPattern.maxChange);
        comp.SetMinPriceChange(newsPattern.minChange);
    }

    
}
