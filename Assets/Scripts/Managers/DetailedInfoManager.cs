using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class DetailedInfoManager : MonoBehaviour
{
    [SerializeField] private DetailedShareInfo _shareTable;
    [SerializeField] private DetailedETFInfo _ETFTable;
    public static DetailedInfoManager _instance;
    private DetailedInfo _currentInfo;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    public enum States
    {
        SHARE,
        OBLIGATION,
        ETF
    }
    public States CurrentState{get; private set;}

    public void Start()
    {
        _shareTable.gameObject.SetActive(true);
        _shareTable.SetInfo(GameManager._instance.currentSecurity);
        _currentInfo = _shareTable;
    }

    public void SetState(Securities sec, States newState)
    {
        _currentInfo?.gameObject.SetActive(false);
        CurrentState = newState;
        switch (newState)
        {
            case States.SHARE:
            case States.OBLIGATION:
                _shareTable.gameObject.SetActive(true);
                _shareTable.SetInfo(sec);
                _currentInfo = _shareTable;
                break;

            case States.ETF:
                _ETFTable.gameObject.SetActive(true);
                _ETFTable.SetInfo(sec);
                _currentInfo = _ETFTable;
                break;
        }
    }
    public void UpdateDetailedInfo()
    {
        _currentInfo?.SetInfo(GameManager._instance.currentSecurity);
    }
}
