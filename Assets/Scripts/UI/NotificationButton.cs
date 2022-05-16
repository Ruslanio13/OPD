using TMPro;
using UnityEngine;

public class NotificationButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textInfo;
    [SerializeField] private GameObject _okButton;
    [SerializeField] private GameObject _quitButton;
    public void DeleteNot() => Destroy(gameObject);
    public void Quit() {
        Application.Quit();
    }

    public void SetInfo(Securities sec, float amountOfMoney)
    {
        if (sec is Share)
            _textInfo.text = "Вы получили дивиденды от " + sec.ParentCompany.GetNameOfCompany() + " \n" + amountOfMoney.ToString("0.00") + " P";
        else if (sec is Obligation)
            _textInfo.text = "Вы получили выплаты по облигациям от " + (sec as Obligation).ParentCompanyName + " \n" + amountOfMoney.ToString("0.00") + " P";

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
