using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DividendsNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dividendsInfo;
    public void DeleteNot() => Destroy(gameObject);

    public void SetInfo(string CompanyName, float amountOfDividends)
    {
        _dividendsInfo.text = "You got dividends from " + CompanyName + " " + amountOfDividends.ToString("0.00")+ " P";
    }
}
