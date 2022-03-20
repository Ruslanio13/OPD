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
    GameObject target;


    void Awake() 
    {
        detailedInfoManager = FindObjectOfType<DetailedInfoManager>();
    }

    public void DisplayMyCompanies(List<Company> companies) 
    {
        foreach(Company tempCompany in companies)
        {
         
            if(tempCompany.GetSecurityMyAmount() != 0 )
            {
                amount = tempCompany.GetSecurityMyAmount();
                AddInPortfolio(tempCompany,0);
            }
        }
    }
    public void Buy()
    {
        company = detailedInfoManager.currentCompany;
        
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
        if(amountText.text != "")
        {
            confirmationTable.SetActive(true);
            prePurchaseTable.SetActive(false);
            question.text = "Are you want to sell " + Convert.ToInt32(amountText.text) + " ?" ;
            sign=-1;
        }
    }

    public void ConfirmAmount()
    {
        if(company.GetSecurityMyAmount() < -sign *Convert.ToInt32(amountText.text)  )
        {
            question.text = "Not enough securities on your balance!";
            Invoke("Cancel",2f);
        }
        if(Convert.ToInt32(amountText.text) <= 0)
        {
            question.text = "Enter number more than 0";
            Invoke("Cancel",2f);
        }
        else
        {
            amount = sign * Convert.ToInt32(amountText.text);
            Cancel();

            AddInPortfolio(detailedInfoManager.currentCompany, amount);
        }
        
    }

    private void AddInPortfolio(Company company, int amount)
    {
        
        GameObject temp;
        
        if(company.GetSecurityMyAmount() == 0 )
        {
            temp = Instantiate(portfolioShortInfoPrefab, portfolioContent.transform);
            temp.GetComponent<PortfolioShortInfo>().SetInfo(company, amount);  
        }
        else if(company.GetSecurityMyAmount() == -amount)
        {
            SearchForCompanyInPortfolio(company);
            Destroy(target);
        }
        else if(amount == 0)
        {
            temp = Instantiate(portfolioShortInfoPrefab, portfolioContent.transform);
            temp.GetComponent<PortfolioShortInfo>().SetInfo(company, company.GetSecurityMyAmount());
        }

        company.UpdateMyAmount(amount);
    }

    public void Cancel()
    {
        amountText.text = "";
        confirmationTable.SetActive(false);
        prePurchaseTable.SetActive(true); 
    }

    void SearchForCompanyInPortfolio(Company company)
    {
        foreach(PortfolioShortInfo portfolioShortInfo in portfolioContent.GetComponentsInChildren<PortfolioShortInfo>())
        {
            if(portfolioShortInfo.companyName.text == company.GetNameOfCompany())
            {
               target =  portfolioShortInfo.gameObject;
            }
        }
    }
}
