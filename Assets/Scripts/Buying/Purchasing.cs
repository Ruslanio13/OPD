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
    int sign;
    [SerializeField] GameObject portfolioShortInfoPrefab;


    void Awake() 
    {
        detailedInfoManager = FindObjectOfType<DetailedInfoManager>();
    }
    public void Buy()
    {
        company = detailedInfoManager.currentCompany;
        Debug.Log(company.GetSecurityMyAmount());
        if(amountText.text != "")
        {
            confirmationTable.SetActive(true);
            prePurchaseTable.SetActive(false);
            question.text = "Are you want to buy " + Convert.ToInt32(amountText.text) + " ?" ;
            sign=1;
        }
    }
     public void Sell()
    {
        company = detailedInfoManager.currentCompany;
        if(company.GetSecurityMyAmount() < amount)
        {
            question.text = "Not enough securities on your balance!";
        }
        else if(amountText.text != "")
        {
            confirmationTable.SetActive(true);
            prePurchaseTable.SetActive(false);
            question.text = "Are you want to sell " + Convert.ToInt32(amountText.text) + " ?" ;
            sign=-1;
        }
    }

    public void ConfirmAmount()
    {
        amount = sign * Convert.ToInt32(amountText.text);
        Cancel();

        AddInPortfolio();
    }

    private void AddInPortfolio()
    {
        
        GameObject temp;
        
        company = detailedInfoManager.currentCompany;
        
        if(company.GetSecurityMyAmount() == 0 )
        {
            temp = Instantiate(portfolioShortInfoPrefab, portfolioContent.transform);
            temp.GetComponent<PortfolioShortInfo>().SetInfo(company, amount);  
        }

        company.UpdateMyAmount(amount);
    }

    public void Cancel()
    {
        amountText.text = "";
        confirmationTable.SetActive(false);
        prePurchaseTable.SetActive(true); 
    }
}
