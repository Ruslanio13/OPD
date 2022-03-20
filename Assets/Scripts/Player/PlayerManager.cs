using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceInMarket;
    [SerializeField] private TextMeshProUGUI _balanceInPortfolio;

    public Balance PlayerBalance;

    public static PlayerManager _instance;

    private void Awake() {
        if(_instance == null)
            _instance = this;
    }

    private void Start() {
        if(PlayerBalance == null)
        PlayerBalance = new Balance();
        _balanceInMarket.text = GetBalance().ToString("00.00") + "$";
        _balanceInPortfolio.text = GetBalance().ToString("00.00") + "$";
    }

    public float GetBalance()
    {
        return PlayerBalance.Dol.AmountOnHands;
    }

    public void Buy(float price)
    {
        if(PlayerBalance.Dol.BuyWith(price))
            {
                _balanceInMarket.text = GetBalance().ToString("00.00") + "$";
                _balanceInPortfolio.text = GetBalance().ToString("00.00") + "$";
            }
    }



    

    

}
