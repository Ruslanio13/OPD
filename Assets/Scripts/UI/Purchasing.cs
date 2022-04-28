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
 
    GameManager detailedInfoManager;
    Company company;
    bool toSell;
    [SerializeField] GameObject portfolioShortInfoPrefab;
    GameObject target;


    void Awake() 
    {
        detailedInfoManager = FindObjectOfType<GameManager>();
    }

    public void Buy()
    {
        if(amountText.text != "")
        {
            confirmationTable.SetActive(true);
            prePurchaseTable.SetActive(false);
            question.text = "Do you want to buy " + Convert.ToInt32(amountText.text) + " ?" ;
            toSell=false;
        }
    }
    public void Sell()
    {
        if(amountText.text != "")
        {
            confirmationTable.SetActive(true);
            prePurchaseTable.SetActive(false);
            question.text = "Do you want to sell " + Convert.ToInt32(amountText.text) + " ?" ;
            toSell=true;
        }
    }

    public void ConfirmAmount()
    {
        amount = Convert.ToInt32(amountText.text);
        
        if(toSell)
            PortfolioManager._instance.SellSecurities(GameManager._instance.currentSecurity, amount);
        else
            PortfolioManager._instance.BuySecurities(GameManager._instance.currentSecurity, amount);

        BalanceManager._instance.UpdateAmountOfValuteOnGUI();

        Cancel();
    }

    public void Cancel()
    {
        amountText.text = "";
        confirmationTable.SetActive(false);
        prePurchaseTable.SetActive(true); 
    }

    /*void SearchForCompanyInPortfolio(Company company)
    {
        foreach(PortfolioShortInfo portfolioShortInfo in portfolioContent.GetComponentsInChildren<PortfolioShortInfo>())
        {
            if(portfolioShortInfo.companyName.text == company.GetNameOfCompany())
            {
               target =  portfolioShortInfo.gameObject;
            }
        }
    }*/
}
