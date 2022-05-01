using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DetailedValuteInfo : DetailedInfo
{
    [SerializeField] private TextMeshProUGUI _averagePrice2017;
    [SerializeField] private TextMeshProUGUI _averagePrice2018;
    [SerializeField] private TextMeshProUGUI _averagePrice2019;
    [SerializeField] private TextMeshProUGUI _averagePrice2020;
    [SerializeField] private TextMeshProUGUI _averagePrice2021;
    [SerializeField] private TextMeshProUGUI _countryName;


    public override void SetInfo(Securities sec)
    {
        _averagePrice2017.text = sec.AveragePrice2017.ToString("0.00") + "$";
        _averagePrice2018.text = sec.AveragePrice2018.ToString("0.00") + "$";
        _averagePrice2019.text = sec.AveragePrice2019.ToString("0.00") + "$";
        _averagePrice2020.text = sec.AveragePrice2020.ToString("0.00") + "$";
        _averagePrice2021.text = sec.AveragePrice2021.ToString("0.00") + "$";

        _countryName.text = (sec as Valute).ValuteCountry.Name;
    }
}
