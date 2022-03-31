using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsSetUp : MonoBehaviour
{
   
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI mainText;
    
 

    public void SetUpNews(Company comp, NewsSO newsPattern) 
    {
        titleText.text = "Компания " + comp.GetNameOfCompany() + " " + newsPattern.title;
        mainText.text = newsPattern.text;
    }

    
}
