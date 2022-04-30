using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PreGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _difficultyPrefab;
    [SerializeField] private RectTransform _difficultiesRT;
    [SerializeField] private RectTransform _brokersRT;
    [SerializeField] private GameObject _brokerPrefab;
    [SerializeField] private TextMeshProUGUI _selectedDiffName;
    [SerializeField] private TextMeshProUGUI _selectedBrokerName;
    [SerializeField] private TextMeshProUGUI _selectedComission;

    public Difficulty CurrentDifficulty{get; private set;}
    public Broker CurrentBroker{get; private set;}
    public static PreGameManager _instance;
    private List<Difficulty> _difficulties = new List<Difficulty>();
    private List<Broker> _brokers = new List<Broker>();
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        InitializeDifficulties();
        InitializeBrokers();
    }

    private void InitializeDifficulties()
    {
        _difficulties.Add(new Difficulty("Easy", 1f));
        _difficulties.Add(new Difficulty("Medium", 1.5f));
        _difficulties.Add(new Difficulty("Hard", 2f));

        foreach (var diff in _difficulties)
        {
            var temp = Instantiate(_difficultyPrefab, _difficultiesRT);
            temp.GetComponent<DifficultyShortInfo>().SetDifficulty(diff);
            _difficultiesRT.sizeDelta += new Vector2(0, 65f);
        }
    }
    private void InitializeBrokers()
    {
        _brokers.Add(new Broker("Freedom", 0.12f, 0f));
        _brokers.Add(new Broker("Alpha", 0.014f, 199f));
        _brokers.Add(new Broker("VTB", 0.06f, 210f));
        _brokers.Add(new Broker("Tinkoff", 0.04f, 290f));
        _brokers.Add(new Broker("Sberbank", 0.055f, 220f));
        foreach (var diff in _brokers)
        {
            var temp = Instantiate(_brokerPrefab, _brokersRT);
            temp.GetComponent<BrokerShortInfo>().SetBroker(diff);
        }
    }
    public void SetDifficulty(Difficulty diff)
    {
        CurrentDifficulty = diff;
        _selectedDiffName.text = diff.Name;
    }
    public void SetBroker(Broker broker)
    {
        CurrentBroker = broker;
        _selectedBrokerName.text = broker.Name;
    }
    public void SetCommision(float comm)
    {
        if(comm > 0 && CurrentBroker.Maintenance >= 0)
            _selectedComission.text = "Комиссия брокера: " + comm +"%\n" + 
            "Ежемесечная плата: " + CurrentBroker.Maintenance +"P";
        else
            throw new Exception("Wrong commision passed");
    }
}
