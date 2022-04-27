using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _newsButtonText;
    [SerializeField] private RectTransform _localNewsFeedRT;
    [SerializeField] private RectTransform _globalNewsFeedRT;
    [SerializeField] GameObject localNewsFeed;
    [SerializeField] GameObject globalNewsFeed;
    [SerializeField] GameObject localNews;
    [SerializeField] GameObject globalNews;
    [SerializeField] GameObject newsPrefab;
    [SerializeField] List<NewsSO> localNewsPatterns = new List<NewsSO>();
    [SerializeField] List<NewsSO> globalNewsPatterns = new List<NewsSO>();
    [SerializeField] private ScrollRect _scrollbar;
    
    public static NewsManager _instance;
    public List<News> AllLocalNews = new List<News>();
    public List<News> AllGlobalNews = new List<News>();
    private List<NewsShortInfo> _newsInUI = new List<NewsShortInfo>();
    private bool _isGlobalNews;


    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        _isGlobalNews = false;
    }

    public void SpawnNews(List<Company> companies)
    {
        int i;
        int numberOfRandomNew;
        News temp;
        if (!_isGlobalNews)
            _localNewsFeedRT.sizeDelta += new Vector2(0, 375f);
       
        for (i = 0; i < companies.Count; i++)
        {
            numberOfRandomNew = UnityEngine.Random.Range(0, localNewsPatterns.Count);
            temp = new News(companies[i], localNewsPatterns[numberOfRandomNew]);
            AllLocalNews.Add(temp);
        }
        ShowCompanyNews(DetailedInfoManager._instance.currentCompany);


        int numberOfRandomGlobalNew = UnityEngine.Random.Range(0, globalNewsPatterns.Count);
        _globalNewsFeedRT.sizeDelta += new Vector2(0, 375f);
        temp = new News(null, globalNewsPatterns[numberOfRandomGlobalNew]);
        AllGlobalNews.Add(temp);
        GameObject tempg = Instantiate(newsPrefab, globalNewsFeed.transform);
        tempg.GetComponent<NewsShortInfo>().SetUpNews(AllGlobalNews[AllGlobalNews.Count - 1], true);
    }

    public void ShowCompanyNews(Company company)
    {
        GameObject temp;
        foreach (NewsShortInfo newsShortInfo in _newsInUI)
        {
            Destroy(newsShortInfo.gameObject);
        }
        _newsInUI.Clear();
        _localNewsFeedRT.sizeDelta = Vector2.zero;

        int numberOfNews = 0;
        for (int i = AllLocalNews.Count - 1; (i >= 0) && (numberOfNews <= 15); i--)
        {
            if (AllLocalNews[i].comp == company)
            {
                _localNewsFeedRT.sizeDelta += new Vector2(0, 375);
                temp = Instantiate(newsPrefab, localNewsFeed.transform);
                temp.GetComponent<NewsShortInfo>().SetUpNews(AllLocalNews[i], false);
                _newsInUI.Add(temp.GetComponent<NewsShortInfo>());
                numberOfNews++;
            }
        }
    }

    public void ChangeNewsType()
    {
        _isGlobalNews = !_isGlobalNews;
        if (!_isGlobalNews)
        {
            _newsButtonText.text = "Global news";
            _scrollbar.content = _localNewsFeedRT; 
        }
        else
        {
            _newsButtonText.text = "Local news";
            _scrollbar.content = _globalNewsFeedRT;
        }

        localNews.SetActive(!_isGlobalNews);
        globalNews.SetActive(_isGlobalNews);
    }
}

