using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrokerShortInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField]private Button _selectBroker;
    private Broker Broker;
    private TextMeshProUGUI _selectedComission;

    private void Start() {
        _selectBroker.onClick.AddListener(() => 
        {
            PreGameManager._instance.SetBroker(Broker);
            PreGameManager._instance.SetCommision(Broker.Commision);
        }
        );
    }
    public void SetBroker(Broker broker)
    {
        Broker = broker;
        _name.text = broker.Name;
    }

}
