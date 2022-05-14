using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class DetailedInfoManager : MonoBehaviour
{
    [SerializeField] private DetailedShareInfo _shareTable;
    [SerializeField] private DetailedETFInfo _ETFTable;
    [SerializeField] private DetailedValuteInfo _valuteTable;
    public static DetailedInfoManager _instance;
    private DetailedInfo _currentInfo;
    private Securities _currentSecInMarket;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void Start()
    {
        _shareTable.gameObject.SetActive(true);
        _shareTable.SetInfo(GameManager._instance.CurrentSecurity);
        _currentInfo = _shareTable;
    }

    public void SetState(Securities sec)
    {
        var temp = sec.GetType();
        _currentSecInMarket = sec;

        _currentInfo?.gameObject.SetActive(false);

        if (temp == typeof(Share) || temp == typeof(Obligation))
            _currentInfo = _shareTable;
        else if (temp == typeof(Valute))
            _currentInfo = _valuteTable;
        else if (temp == typeof(ETF))
            _currentInfo = _ETFTable;

        _currentInfo.gameObject.SetActive(true);
        _currentInfo.SetInfo(sec);
    }
    public void UpdateDetailedInfo()
    {
        if(_currentSecInMarket is ETF && GameManager._instance.CurrentSecurity is Share)
            _currentInfo?.SetInfo(_currentSecInMarket);
        else
        {
            _currentSecInMarket = GameManager._instance.CurrentSecurity;
        }
        _currentInfo?.SetInfo(_currentSecInMarket);
    }
}
