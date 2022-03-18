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
        int count = DetailedInfoManager._instance.currentCompany._priceHistory.Count;
<<<<<<< HEAD
        for (int i = count - 10; i < count; i++)
=======
        for (int i = 0; i < 10; i++)
>>>>>>> a7efe385531b1654268cf8594cfc4504c27e3ace
        {
            _graph.SetPosition(i, new Vector2(i * _stepX + _deltaX, DetailedInfoManager._instance.currentCompany._priceHistory[i] + _deltaY));
        }
    }

}
