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
        SaveData save = new SaveData(DetailedInfoManager._instance.Companies, PortfolioManager._instance.Portfolio, BalanceManager._instance.RublesWallet, BalanceManager._instance.Valutes, NewsManager._instance.AllNews);

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
        NewsManager._instance.AllNews = save.News;

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
    public List<Securities> Portfolio = new List<Securities>();
    public float Wallet;
    public List<Valute> Valutes = new List<Valute>();
    public List<News> News = new List<News>();
    public SaveData(List<Company> comp, List<Securities> port, float wal, List<Valute> val, List<News> news)
    {
        Companies = comp;
        Portfolio = port;
        Wallet = wal;
        Valutes = val;
        News = news;
    }
}