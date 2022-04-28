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

    public void SpawnCompanyNews(List<Company> companies)
    {
        int i;
        int numberOfRandomNew;
        News temp;

        _localNewsFeedRT.sizeDelta += new Vector2(0, 375f);
        
        for (i = 0; i < companies.Count; i++)
        {
            numberOfRandomNew = UnityEngine.Random.Range(0, localNewsPatterns.Count);
            temp = new News();
            temp.SetUpNews<Company>(companies[i], localNewsPatterns[numberOfRandomNew]);
            AllLocalNews.Add(temp);
        }
        ShowCompanyNews(GameManager._instance.currentCompany);


    }
    public void SpawnGlobalNews(List<Country> countries)
    {
        int countryID;
        int numberOfPattern;
        GameObject tempg;
        News temp;

        _globalNewsFeedRT.sizeDelta += new Vector2(0, 375f);

        countryID = UnityEngine.Random.Range(0, countries.Count);
        numberOfPattern = UnityEngine.Random.Range(0, globalNewsPatterns.Count);

        temp = new News();

        temp.SetUpNews<Country>(countries[countryID], globalNewsPatterns[numberOfPattern]);
        AllGlobalNews.Add(temp);
        tempg = Instantiate(newsPrefab, globalNewsFeed.transform);
        tempg.GetComponent<NewsShortInfo>().SetUpNews(AllGlobalNews[AllGlobalNews.Count - 1]);
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
                temp.GetComponent<NewsShortInfo>().SetUpNews(AllLocalNews[i]);
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

