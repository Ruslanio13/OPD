using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedInfoManager : MonoBehaviour
{
    
    [SerializeField] public List<CompanySO> companies= new List<CompanySO>();
    public int currentIndex;
    [SerializeField] DetailedInfo table;
    void Awake() 
    {
        if(_instance == null){ _instance = this;}
    }

    public static DetailedInfoManager _instance;

    public void UpdateTable(int index)
    {
        currentIndex = index;
        table.SetInfo(companies[currentIndex]);
    }
}
