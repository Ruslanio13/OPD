using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Balance _playerBalance;

    public static PlayerManager _instance;



    private void Awake() {
        if(_instance == null)
            _instance = this;
    }

    private void Start() {
        _playerBalance = new Balance();
    }

    public float GetBalance()
    {
        return _playerBalance.Dol.AmountOnHands;
    }



    

    

}
