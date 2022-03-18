using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private LineRenderer _graph;
    [SerializeField] private float _stepX;
    [SerializeField] private float _deltaX;
    [SerializeField] private float _deltaY;



    private void Start()
    {

        UpdateGraph();
    }

    public void UpdateGraph()
    {
        for (int i = 0; i < 10; i++)
        {
            _graph.SetPosition(i, new Vector2(i * _stepX + _deltaX, DetailedInfoManager._instance.currentCompany._priceHistory[i] + _deltaY));
        }
    }

}
