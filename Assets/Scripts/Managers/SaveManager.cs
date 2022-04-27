using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        
    }

    private void Start() {
        Load();    
    }

    public void Save()
    {
        Debug.Log("Game Saved!");
        SaveData save = new SaveData(DetailedInfoManager._instance.Companies,DetailedInfoManager._instance.Countries, PortfolioManager._instance.Portfolio, 
        BalanceManager._instance.RublesWallet, BalanceManager._instance.Valutes, 
        NewsManager._instance.AllLocalNews, NewsManager._instance.AllLocalNews,
        DetailedInfoManager._instance.Calendar);

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
            DetailedInfoManager._instance.InitializeCountries();
            DetailedInfoManager._instance.InitializeCompanies();
            BalanceManager._instance.GenerateValutesList();
            BalanceManager._instance.CreateNewWallet();
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        SaveData save = (SaveData)bf.Deserialize(file);
        file.Close();

        DetailedInfoManager._instance.Companies = save.Companies;
        PortfolioManager._instance.Portfolio = save.Portfolio;
        BalanceManager._instance.RublesWallet = save.Wallet;
        BalanceManager._instance.Valutes = save.Valutes;
        NewsManager._instance.AllLocalNews = save.LocalNews;
        NewsManager._instance.AllGlobalNews = save.LocalNews;
        DetailedInfoManager._instance.Calendar = save.Calendar;

    }

    private void OnApplicationQuit()
    {
        DetailedInfoManager._instance.SetValute(BalanceManager._instance.Valutes[0]);
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
    public SaveData(List<Company> comp,List<Country> countries, List<Securities> port, float wal, List<Valute> val, List<News> localNews,List<News> globalNews, Calendar cal)
    {
        Companies = comp;
        Countries = countries;
        Portfolio = port;
        Wallet = wal;
        Valutes = val;
        LocalNews = localNews;
        GlobalNews = globalNews;
        Calendar = cal;
    }
}