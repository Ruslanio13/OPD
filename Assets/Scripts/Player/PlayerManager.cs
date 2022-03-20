using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceInMarket;
    [SerializeField] private TextMeshProUGUI _balanceInPortfolio;

    private Balance _playerBalance;

    public static PlayerManager _instance;

    private void Awake() {
        if(_instance == null)
            _instance = this;
    }

    private void Start() {
        _playerBalance = new Balance();
        _balanceInMarket.text = GetBalance().ToString("00.00") + "$";
        _balanceInPortfolio.text = GetBalance().ToString("00.00") + "$";
    }

    public float GetBalance()
    {
        return _playerBalance.Dol.AmountOnHands;
    }



    

    

}
