using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetailedInfo : MonoBehaviour
{
    public virtual void SetInfo(Securities sec)
    {

    }
}
public class DetailedShareInfo: DetailedInfo
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


    public override void SetInfo(Securities sec)
    {
        averagePrice2017.text = sec.AveragePrice2017.ToString("0.00") + "$";
        averagePrice2018.text = sec.AveragePrice2018.ToString("0.00") + "$";
        averagePrice2019.text = sec.AveragePrice2019.ToString("0.00") + "$";
        averagePrice2020.text = sec.AveragePrice2020.ToString("0.00") + "$";
        averagePrice2021.text = sec.AveragePrice2021.ToString("0.00") + "$";

        if (sec.ParentCompany == null)
            return;

        capitalization.text = sec.ParentCompany.Capitalization.ToString();
        amountOfSecurities.text = sec.ParentCompany.AmountOfSecurities.ToString();
        profit.text = sec.ParentCompany.Profit.ToString();
        staff.text = sec.ParentCompany.Staff.ToString();
        credit.text = sec.ParentCompany.Credit.ToString();

    }
}
