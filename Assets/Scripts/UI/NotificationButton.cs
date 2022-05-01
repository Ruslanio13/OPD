using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textInfo;
    [SerializeField] private GameObject _okButton;
    [SerializeField] private GameObject _quitButton;
    public void DeleteNot() => Destroy(gameObject);
    public void Quit() {
        Application.Quit();
        Debug.Log("FDASdsa");
    }

    public void SetInfo(string CompanyName, float amountOfDividends)
    {
        _textInfo.text = "You got dividends from " + CompanyName + " \n" + amountOfDividends.ToString("0.00")+ " P";
        _okButton.SetActive(true);
        _quitButton.SetActive(false);
    }

    public void SetInfo()
    {
        _textInfo.text = "Round is over!";
        _quitButton.SetActive(true);
        _okButton.SetActive(false);
    }


}
