using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class DetailedInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI averagePrice2017;
    [SerializeField] TextMeshProUGUI averagePrice2018;
    [SerializeField] TextMeshProUGUI averagePrice2019;
    [SerializeField] TextMeshProUGUI averagePrice2020;
    [SerializeField] TextMeshProUGUI averagePrice2021;
    [SerializeField] TextMeshProUGUI capitalization;
    [SerializeField] TextMeshProUGUI amountOfSecurities;
    [SerializeField] TextMeshProUGUI profit;
    [SerializeField] TextMeshProUGUI staff;
    [SerializeField] TextMeshProUGUI credit;

    

    public void SetInfo(CompanySO company)
    {
        averagePrice2017.text = company.AveragePrice2017.ToString();
        averagePrice2018.text = company.AveragePrice2018.ToString();
        averagePrice2019.text = company.AveragePrice2019.ToString();
        averagePrice2020.text = company.AveragePrice2020.ToString();
        averagePrice2021.text = company.AveragePrice2021.ToString();

        capitalization.text = company.Capitalization.ToString();
        amountOfSecurities.text = company.AmountOfSecurities.ToString();
        profit.text = company.Profit.ToString();
        staff.text = company.Staff.ToString();
        credit.text = company.Credit.ToString();
    }
}
