using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedInfoManager : MonoBehaviour
{
    
    [SerializeField] public List<CompanySO> companies= new List<CompanySO>();
    [SerializeField] public Graph _graph;
    [SerializeField] DetailedInfo table;
    public int currentIndex;
    public CompanySO currentCompany;
    void Awake() 
    {
        if(_instance == null){ _instance = this;}
        currentCompany = companies[0];
        currentIndex = 0;
    }

    public static DetailedInfoManager _instance;

    public void UpdateAllInformation(int index)
    {
        currentIndex = index;
        currentCompany = companies[currentIndex];
        table.SetInfo(companies[currentIndex]);
        _graph.UpdateGraph();

    }



    private void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.A))
        {
            foreach(CompanySO comp in companies)
            {
                comp.UpdatePrice();
            }
            _graph.UpdateGraph();
        }
    }

}
