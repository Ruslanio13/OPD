using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    [SerializeField] private RectTransform _newsFeedRT;
    [SerializeField] GameObject newsFeed;
    [SerializeField] GameObject newsPrefab;
    [SerializeField] List<NewsSO> newsPatterns = new List<NewsSO>();
    public static NewsManager _instance;
    private List<News> _allNews = new List<News>();
    private List<NewsShortInfo> _newsInUI = new List<NewsShortInfo>();


    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void SpawnNews(List<Company> companies)
    {
        int i;
        int numberOfRandomNew;
        News temp;
        _newsFeedRT.sizeDelta += new Vector2(0, 375f);
        for (i = companies.Count - 1; i >= 0; i--)
        {
            numberOfRandomNew = UnityEngine.Random.Range(0, newsPatterns.Count);
            temp = new News(companies[i], newsPatterns[numberOfRandomNew]);
            _allNews.Add(temp);
            ShowCompanyNews(DetailedInfoManager._instance.currentCompany);
        }
    }

    public void ShowCompanyNews(Company company)
    {
        GameObject temp;
        foreach (NewsShortInfo newsShortInfo in _newsInUI)
        {
            Destroy(newsShortInfo.gameObject);
        }
        _newsInUI.Clear();

        foreach (News news in _allNews)
        {
            if (news.comp == company)
            {
                temp = Instantiate(newsPrefab, newsFeed.transform);
                temp.GetComponent<NewsShortInfo>().SetUpNews(news);
                _newsInUI.Add(temp.GetComponent<NewsShortInfo>());
            }
        }
    }

}

