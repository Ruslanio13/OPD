using UnityEngine;
using TMPro;

public class NewsShortInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI mainText;

    public void SetUpNews(News news)
    {
        titleText.text = news.TitleText;
        mainText.text = news.MainText;
    }


}
