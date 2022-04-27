using UnityEngine;
using TMPro;

public class NewsShortInfo : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI mainText;
    public Company Company { get; private set; }


    public void SetUpNews(News news, bool isGlobal, bool activate = false)
    {
        if (!isGlobal)
        {
            Company = news.comp;
            titleText.text = "Компания " + news.comp.GetNameOfCompany() + " " + news.TitleText;
        }
        else
            titleText.text = news.TitleText;
        mainText.text = news.MainText;
    }


}
