using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Purchasing : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amountText;
    float amount;
   public void EnterAmount()
   {
       amount = Convert.ToFloat(amountText);
   }
}
