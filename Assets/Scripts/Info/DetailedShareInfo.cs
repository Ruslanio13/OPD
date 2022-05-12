using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class DetailedInfo : MonoBehaviour
{
    public abstract void SetInfo(Securities sec);
}
public class DetailedShareInfo: DetailedInfo
{
    [SerializeField] TextMeshProUGUI averagePrice2017;
    [SerializeField] TextMeshProUGUI averagePrice2018;
    [SerializeField] TextMeshProUGUI averagePrice2019;
    [SerializeField] TextMeshProUGUI averagePrice2020;
    [SerializeField] TextMeshProUGUI averagePrice2021;
    [SerializeField] TextMeshProUGUI profit;
    [SerializeField] TextMeshProUGUI EBITDA;
    [SerializeField] TextMeshProUGUI clearProfit;
    [SerializeField] TextMeshProUGUI actives;
    [SerializeField] TextMeshProUGUI debt;
    [SerializeField] TextMeshProUGUI divProfit;
    [SerializeField] TextMeshProUGUI PE;
    [SerializeField] TextMeshProUGUI PS;
    [SerializeField] TextMeshProUGUI PBV;
    [SerializeField] TextMeshProUGUI EXEBITDA;



    public override void SetInfo(Securities sec)
    {
        averagePrice2017.text = sec.AveragePrice2017.ToString("0.00") + "$";
        averagePrice2018.text = sec.AveragePrice2018.ToString("0.00") + "$";
        averagePrice2019.text = sec.AveragePrice2019.ToString("0.00") + "$";
        averagePrice2020.text = sec.AveragePrice2020.ToString("0.00") + "$";
        averagePrice2021.text = sec.AveragePrice2021.ToString("0.00") + "$";
        if (!(sec is Share))
            return;
        profit.text = sec.ParentCompany.Profit.ToString("0.00");
        EBITDA.text = sec.ParentCompany.EBITDA.ToString("0.00");
        clearProfit.text = sec.ParentCompany.ClearProfit.ToString("0.00");
        actives.text = sec.ParentCompany.Actives.ToString("0.00");
        debt.text = sec.ParentCompany.Debt.ToString("0.00");
        divProfit.text = sec.ParentCompany.DivProfit.ToString("0.00");
        PE.text = sec.ParentCompany.PE.ToString("0.00");
        PS.text = sec.ParentCompany.PS.ToString("0.00");
        PBV.text = sec.ParentCompany.PBV.ToString("0.00");
        EXEBITDA.text = sec.ParentCompany.EVEBITDA.ToString("0.00");
    

    }
}
