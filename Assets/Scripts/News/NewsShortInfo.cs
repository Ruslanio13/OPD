using UnityEngine;
using TMPro;

public class NewsShortInfo : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI mainText;
    public Company Company { get; private set; }


    public void SetUpNews(News news, bool activate = false)
    {
        Company = news.comp;
        titleText.text = "Компания " + news.comp.GetNameOfCompany() + " " + news.TitleText;
        mainText.text = news.MainText;
    }


}
