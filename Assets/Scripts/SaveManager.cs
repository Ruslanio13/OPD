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
        SaveData save = new SaveData(DetailedInfoManager._instance.Companies, PortfolioManager._instance.Portfolio, PlayerManager._instance.PlayerBalance);

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
            
            
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        SaveData save = (SaveData)bf.Deserialize(file);
        file.Close();

        DetailedInfoManager._instance.Companies = save.Companies;
        PortfolioManager._instance.Portfolio = save.Portfolio;
        PlayerManager._instance.PlayerBalance = save.Balance;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

[Serializable]
public class SaveData
{
    public List<Company> Companies;
    public Dictionary<Securities, int> Portfolio = new Dictionary<Securities, int>();
    public Balance Balance;
    public SaveData(List<Company> comp, Dictionary<Securities, int> port, Balance bal)
    {
        Companies = comp;
        Portfolio = port;
        Balance = bal;
    }
}