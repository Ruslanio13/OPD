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
        if (int.TryParse(amountText.text, out amount))
        {
            confirmationTable.SetActive(true);
            prePurchaseTable.SetActive(false);
            question.text = "Вы уверены что хотите купить " + Convert.ToInt32(amountText.text) + " ?";
            toSell = false;
        }
        else
            amountText.text = "";
    }
    public void Sell()
    {
        if (int.TryParse(amountText.text, out amount) && !(GameManager._instance.CurrentSecurity is Obligation))
        {
            confirmationTable.SetActive(true);
            prePurchaseTable.SetActive(false);
            question.text = "Вы уверены что хотите продать " + Convert.ToInt32(amountText.text) + " ?";
            toSell = true;
        }
        else
            amountText.text = "";

    }

    public void ConfirmAmount()
    {
        amount = Convert.ToInt32(amountText.text);
        
        if(toSell)
            PortfolioManager._instance.SellSecurities(GameManager._instance.CurrentSecurity, amount);
        else
            PortfolioManager._instance.BuySecurities(GameManager._instance.CurrentSecurity, amount);

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
