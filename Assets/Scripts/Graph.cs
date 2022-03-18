using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private LineRenderer _graph;
    [SerializeField] private float _stepX;
    [SerializeField] private float _deltaX;
    [SerializeField] private float _deltaY;
    [SerializeField] private int _scale;



    private void Start()
    {

        UpdateGraph();
    }

    public void UpdateGraph()
    {
        int g = 0;
        int count = DetailedInfoManager._instance.currentCompany._priceHistory.Count;
        for (int i = count - 10; i < count; i++, g++)
        {

            _graph.SetPosition(g, new Vector2(g * _stepX + _deltaX, Mathf.Clamp((DetailedInfoManager._instance.currentCompany._priceHistory[i] - 90f) * _scale, 0, 700) + _deltaY));
        }
    }

}
