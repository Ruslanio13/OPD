using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private LineRenderer _graph;
    [SerializeField] private float _stepX;
    [SerializeField] private float _deltaX;
    [SerializeField] private float _deltaY;
    private float _minY;
    private float _maxY;
    private float _visualScale;
    public enum Scale
    {
        YEAR = -1,
        QUARTER = 0,
        MONTH = 1
    }
    private Scale _currentTimeScale;
    private int _timeScaleKoef;

    public void LessenScale()
    {
        if (_currentTimeScale == 0)
        {
            _currentTimeScale = (Scale)1;
            _timeScaleKoef = 1;
            SetAmountOfDots(50);
        }
        else if ((int)_currentTimeScale == -1)
        {
            _currentTimeScale = 0;
            _timeScaleKoef = 90;
            SetAmountOfDots(20);
        }
        UpdateGraph();
    }
    public void EnlargeScale()
    {
        if (_currentTimeScale == 0)
        {
            _currentTimeScale = (Scale)(-1);
            _timeScaleKoef = 365;
            _graph.positionCount = 5;
            SetAmountOfDots(5);
        }
        else if ((int)_currentTimeScale == 1)
        {
            _currentTimeScale = 0;
            _timeScaleKoef = 90;
            SetAmountOfDots(20);
        }
        UpdateGraph();
    }

    private void SetAmountOfDots(int v)
    {
        _graph.positionCount = v;
        _stepX = 800 / v;
    }

    private void Start()
    {
        _currentTimeScale = Scale.MONTH;
        _timeScaleKoef = 1;

        SetAmountOfDots(50);
        ResetPosition();
        UpdateGraph();
    }

    public void UpdateGraph()
    {
        int g = 0;
        int count = DetailedInfoManager._instance.currentSecurity._priceHistory.Count;
        float currentPrice;
        bool isValuteGraph = (DetailedInfoManager._instance.currentSecurity.GetType() == typeof(Valute));

        for (int i = count - _graph.positionCount * _timeScaleKoef; i < count; i = i + _timeScaleKoef, g++)
        {
            if(isValuteGraph)
                currentPrice = DetailedInfoManager._instance.currentSecurity._priceHistory[i]/DetailedInfoManager._instance.currentValute._priceHistory[i];
            else
                currentPrice = DetailedInfoManager._instance.currentSecurity._priceHistory[i];
            
            _graph.SetPosition(g, new Vector2(g * _stepX, currentPrice * _visualScale));
        }

        _graph.transform.localPosition = new Vector2(-_graph.transform.position.x + _deltaX, -(_maxY + _minY) / 2f * _visualScale);
    }

    private void OnGUI() {
        if(Input.mouseScrollDelta.y != 0)
        {
            if(_visualScale>=1)
            {
                _visualScale += Input.mouseScrollDelta.y;    
                UpdateGraph();
            }
            else
            {
                _visualScale = 1;
            }
        }
    }
    private void OnMouseDrag() {
        Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
 
        Vector2 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = new Vector2(transform.position.x, curPosition.y);
    }

    public void ResetPosition()
    {
        _minY = 10000f;
        _maxY = 0f;
        transform.localPosition = new Vector2(transform.localPosition.x, 0f);
        _visualScale = 3;
        float currentPrice;
        bool isValuteGraph = (DetailedInfoManager._instance.currentSecurity.GetType() == typeof(Valute));
        int count = DetailedInfoManager._instance.currentSecurity._priceHistory.Count;
        for (int i = count - _graph.positionCount * _timeScaleKoef; i < count; i = i + _timeScaleKoef)
        {
            if(isValuteGraph)
                currentPrice = DetailedInfoManager._instance.currentSecurity._priceHistory[i]/DetailedInfoManager._instance.currentValute._priceHistory[i];
            else
                currentPrice = DetailedInfoManager._instance.currentSecurity._priceHistory[i];
            if (_minY > currentPrice)

                _minY = currentPrice;

            if (_maxY < currentPrice)
                _maxY = currentPrice;
        }
        if(_maxY != _minY)
            _visualScale = 350f/(_maxY - _minY);
        else   
            _visualScale = 1;
    }
}