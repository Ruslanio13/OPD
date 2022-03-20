using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Purchasing : MonoBehaviour
{
    [SerializeField] TMP_InputField amountText;
    [SerializeField] int amount;
    [SerializeField] GameObject confirmationTable;
    [SerializeField] GameObject prePurchaseTable;
    [SerializeField] TextMeshProUGUI question;
    [SerializeField] GameObject portfolioContent;
 
    DetailedInfoManager detailedInfoManager;
    Company company;
    bool toSell;
    [SerializeField] GameObject portfolioShortInfoPrefab;


    void Awake() 
    {
        detailedInfoManager = FindObjectOfType<DetailedInfoManager>();
    }
    public void Buy()
    {
        if(amountText.text != "")
        {
            confirmationTable.SetActive(true);
            prePurchaseTable.SetActive(false);
            question.text = "Are you want to buy " + Convert.ToInt32(amountText.text) + " ?" ;
            toSell=false;
        }
    }
     public void Sell()
    {
        if(amountText.text != "")
        {
            confirmationTable.SetActive(true);
            prePurchaseTable.SetActive(false);
            question.text = "Are you want to sell " + Convert.ToInt32(amountText.text) + " ?" ;
            toSell=true;
        }
    }

    public void ConfirmAmount()
    {
        amount = Convert.ToInt32(amountText.text);
        if(toSell)
        PortfolioManager._instance.SellSecurities(DetailedInfoManager._instance.currentSecurity, amount);
            else
        PortfolioManager._instance.BuySecurities(DetailedInfoManager._instance.currentSecurity, amount);

        Cancel();
    }

    // private void AddInPortfolio()
    // {
        
    //     GameObject temp;
        
    //     company = detailedInfoManager.currentCompany;
        
    //     if(company.GetSecurityMyAmount() == 0 )
    //     {
    //         temp = Instantiate(portfolioShortInfoPrefab, portfolioContent.transform);
    //         temp.GetComponent<PortfolioShortInfo>().SetInfo(company, amount);  
    //     }

    //     company.UpdateMyAmount(amount);
    // }

    public void Cancel()
    {
        amountText.text = "";
        confirmationTable.SetActive(false);
        prePurchaseTable.SetActive(true); 
    }
}
