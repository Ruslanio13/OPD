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



    public override void SetInfo(Securities sec)
    {
        averagePrice2017.text = sec.AveragePrice2017.ToString("0.00") + "$";
        averagePrice2018.text = sec.AveragePrice2018.ToString("0.00") + "$";
        averagePrice2019.text = sec.AveragePrice2019.ToString("0.00") + "$";
        averagePrice2020.text = sec.AveragePrice2020.ToString("0.00") + "$";
        averagePrice2021.text = sec.AveragePrice2021.ToString("0.00") + "$";
    }
}
