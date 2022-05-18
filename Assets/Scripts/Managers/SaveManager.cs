using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    public static SaveManager _instance;

    private List<Company> _companyJSONList;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        
    }

    private void Start() {
        try
        {
            _companyJSONList = JsonConvert.DeserializeObject<List<Company>>
            (File.ReadAllText(Application.streamingAssetsPath + "/Companies.json"));
        }
        catch(Exception)
        {
            throw new Exception("Wrong JSON Save File Format");
        }
        Load();    
    }

    public void Save()
    {
        Debug.Log("Game Saved!");
        SaveData save = new SaveData(GameManager._instance.Companies,GameManager._instance.Countries, PortfolioManager._instance.Portfolio, 
        BalanceManager._instance.RublesWallet, BalanceManager._instance.Valutes, 
        NewsManager._instance.AllLocalNews, NewsManager._instance.AllLocalNews,
        GameManager._instance.Calendar,
        PreGameManager._instance.CurrentBroker, PreGameManager._instance.CurrentDifficulty
        );

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }
    public void Load()
    {
        if (!File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            Debug.Log("No save file. Creating New!");
            GameManager._instance.InitializeCountries();
            BalanceManager._instance.GenerateValutesList();
            GameManager._instance.InitializeCompanies(_companyJSONList);
            BalanceManager._instance.CreateNewWallet();

            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        SaveData save = (SaveData)bf.Deserialize(file);
        file.Close();

        GameManager._instance.Countries = save.Countries;
        GameManager._instance.Companies = save.Companies;
        PortfolioManager._instance.Portfolio = save.Portfolio;
        BalanceManager._instance.RublesWallet = save.Wallet;
        BalanceManager._instance.Valutes = save.Valutes;
        NewsManager._instance.AllLocalNews = save.LocalNews;
        NewsManager._instance.AllGlobalNews = save.LocalNews;
        GameManager._instance.Calendar = save.Calendar;
        PreGameManager._instance.SetBroker(save.Broker);
        PreGameManager._instance.SetDifficulty(save.Difficulty);
    }

    private void OnApplicationQuit()
    {
        GameManager._instance.SetValute(BalanceManager._instance.Valutes[0]);
        Save();
    }
}

[Serializable]
public class SaveData
{
    public List<Company> Companies;
    public List<Country> Countries;
    public List<Securities> Portfolio = new List<Securities>();
    public float Wallet;
    public List<Valute> Valutes = new List<Valute>();
    public List<News> LocalNews = new List<News>();
    public List<News> GlobalNews = new List<News>();
    public Calendar Calendar;
    public Broker Broker;
    public Difficulty Difficulty;

    public SaveData(List<Company> comp,List<Country> countries, List<Securities> port, float wal, List<Valute> val, List<News> localNews,List<News> globalNews, Calendar cal, Broker broker, Difficulty diff)
    {       
        Companies = comp;
        Countries = countries;
        Portfolio = port;
        Wallet = wal;
        Valutes = val;
        LocalNews = localNews;
        GlobalNews = globalNews;
        Calendar = cal;
        Broker = broker;
        Difficulty = diff;
    }
}