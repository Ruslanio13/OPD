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
    int sign;
    public void Buy()
    {
        confirmationTable.SetActive(true);
        prePurchaseTable.SetActive(false);
        question.text = "Are you want to buy " + Convert.ToInt32(amountText.text) + " ?" ;
        sign=1;
    }
     public void Sell()
    {
        confirmationTable.SetActive(true);
        prePurchaseTable.SetActive(false);
        question.text = "Are you want to sell " + Convert.ToInt32(amountText.text) + " ?" ;
        sign=-1;
    }

    public void ConfirmAmount()
    {
        amount = sign*Convert.ToInt32(amountText.text);
        Cancel(); 
    }
    public void Cancel()
    {
        amountText.text = "Enter amount";
        confirmationTable.SetActive(false);
        prePurchaseTable.SetActive(true); 
    }
}
