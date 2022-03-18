using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedInfoManager : MonoBehaviour
{
    
    [SerializeField] public List<CompanySO> companies= new List<CompanySO>();
    public int currentIndex;
    public CompanySO currentCompany;
    [SerializeField] DetailedInfo table;
    void Awake() 
    {
        if(_instance == null){ _instance = this;}
        currentCompany = companies[0];
        currentIndex = 0;
    }

    public static DetailedInfoManager _instance;

    public void UpdateTable(int index)
    {
        currentIndex = index;
        currentCompany = companies[currentIndex];
        table.SetInfo(companies[currentIndex]);
    }
}
