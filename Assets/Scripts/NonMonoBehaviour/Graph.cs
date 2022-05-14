using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [SerializeField] private LineRenderer _graph;
    [SerializeField] private LineRenderer _maxLine;
    [SerializeField] private LineRenderer _minLine;
    [SerializeField] private Text _maxPriceTxt;
    [SerializeField] private Text _minPriceTxt;
    [SerializeField] private float _stepX;
    [SerializeField] private float _deltaX;
    [SerializeField] private float _deltaY;
    [SerializeField] private float _visualScale;
    private float _minY;
    private float _maxY;
    private int _timeScaleKoef;


    private void SetAmountOfDots(int v)
    {
        _graph.positionCount = v;
        _stepX = 800 / v;
    }

    private void Start()
    {
        SetAmountOfDots(50);
        ResetPosition();
        UpdateGraph();
    }

    public void UpdateGraph()
    {
        int g = 0;
        int count = GameManager._instance.CurrentSecurity._priceHistory.Count;
        float currentPrice;
        bool isValuteGraph = (GameManager._instance.CurrentSecurity.GetType() == typeof(Valute));

        for (int i = count - _graph.positionCount; i < count; i++, g++)
        {
            if(isValuteGraph)
                currentPrice = GameManager._instance.CurrentSecurity._priceHistory[i]/GameManager._instance.CurrentValute._priceHistory[i];
            else
                currentPrice = GameManager._instance.CurrentSecurity._priceHistory[i];
            
            _graph.SetPosition(g, new Vector2(g * _stepX, currentPrice * _visualScale));
        }

        for (int i = 0; i < 2; i++)
        {
            _minLine.SetPosition(i, new Vector2(390 + 50 * i, _minY * _visualScale));
            _maxLine.SetPosition(i, new Vector2(390 + 50 * i, _maxY * _visualScale));
        }
        _graph.transform.localPosition = new Vector2(-_graph.transform.position.x + _deltaX, -(_maxY + _minY) / 2f * _visualScale);
        _minLine.transform.localPosition = new Vector2(-_minLine.transform.position.x, -(_maxY + _minY) / 2f * _visualScale);
        _maxLine.transform.localPosition = new Vector2(-_maxLine.transform.position.x, -(_maxY + _minY) / 2f * _visualScale);
        _maxPriceTxt.text = _maxY.ToString("0.00");
        _minPriceTxt.text = _minY.ToString("0.00");
        _maxPriceTxt.transform.localPosition = new Vector2(_maxPriceTxt.transform.localPosition.x, (_maxY * _visualScale + 15f));
        _minPriceTxt.transform.localPosition = new Vector2(_minPriceTxt.transform.localPosition.x, (_minY * _visualScale + 15f));
    }

    public void ResetPosition()
    {
        _minY = 1000000f;
        _maxY = 0f;
        transform.localPosition = new Vector2(transform.localPosition.x, 0f);
        _visualScale = 3;
        float currentPrice;
        bool isValuteGraph = (GameManager._instance.CurrentSecurity.GetType() == typeof(Valute));
        int count = GameManager._instance.CurrentSecurity._priceHistory.Count;
        for (int i = count - _graph.positionCount; i < count; i++)
        {
            if(isValuteGraph)
                currentPrice = GameManager._instance.CurrentSecurity._priceHistory[i]/GameManager._instance.CurrentValute._priceHistory[i];
            else
                currentPrice = GameManager._instance.CurrentSecurity._priceHistory[i];
            if (_minY > currentPrice)

                _minY = currentPrice;

            if (_maxY < currentPrice)
                _maxY = currentPrice;
        }

        if (_maxY != _minY)
            _visualScale = 350f/(_maxY - _minY);
        else   
            _visualScale = 1;

    }
}