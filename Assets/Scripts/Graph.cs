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
        int j =0;
        int count = DetailedInfoManager._instance.currentCompany._priceHistory.Count;
        for (int i = count - 10; i < count; i++, j++)
        {
            _graph.SetPosition(j, new Vector2(j * _stepX + _deltaX, DetailedInfoManager._instance.currentCompany._priceHistory[i]));
        }
        _graph.transform.localPosition = new Vector2(_graph.transform.position.x, _graph.GetPosition(9).y);
    }

}
