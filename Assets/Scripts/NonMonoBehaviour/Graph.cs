using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Graph : MonoBehaviour
{
    [SerializeField] private LineRenderer _graph;
    [SerializeField] private TextMeshProUGUI _maxPriceTxt;
    [SerializeField] private TextMeshProUGUI _minPriceTxt;
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
        _stepX = 800 / v * Screen.width / 1920f;
    }

    private void Start()
    {
        _deltaX *= Screen.width / 1920f;
        SetAmountOfDots(50);
        ResetPosition();
        
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

      
        _graph.transform.localPosition = new Vector2(-_graph.transform.position.x + _deltaX, -(_maxY + _minY) / 2f * _visualScale);
        _maxPriceTxt.text = _maxY.ToString("0.00");
        _minPriceTxt.text = _minY.ToString("0.00");
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
            _visualScale = 350f/(_maxY - _minY) * Screen.width / 1920f;
        else   
            _visualScale = 1 * Screen.width / 1920f;
        UpdateGraph();

    }
}