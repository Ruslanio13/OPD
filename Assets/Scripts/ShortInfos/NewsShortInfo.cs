using UnityEngine;
using TMPro;

public class NewsShortInfo : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI mainText;
    public Company Company { get; private set; }


    public void SetUpNews(News news, bool activate = false)
    {
        titleText.text = news.TitleText;
        mainText.text = news.MainText;
    }


}
