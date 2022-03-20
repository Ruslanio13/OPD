using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text _balance;

    private Portfolio _playerPortfolio;
    private Balance _playerBalance;

    public static PlayerManager _instance;

    private void Awake() {
        if(_instance == null)
            _instance = this;
    }

    private void Start() {
        _playerPortfolio = new Portfolio();
        _playerBalance = new Balance();
        _balance.text = GetBalance().ToString() + "$";
    }

    public float GetBalance()
    {
        return _playerBalance.Dol.AmountOnHands;
    }

}
