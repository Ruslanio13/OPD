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
    private List<News> _spawnedNews = new List<News>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void SpawnNews(List<Company> companies)
    {
        int i;
        int numberOfRandomNew;
        GameObject temp;
        _newsFeedRT.sizeDelta += new Vector2(0, 375f);
        for (i = companies.Count - 1; i >= 0; i--)
        {
            numberOfRandomNew = UnityEngine.Random.Range(0, newsPatterns.Count);
            temp = Instantiate(newsPrefab, newsFeed.transform);
            _spawnedNews.Add(temp.GetComponent<News>());
            _spawnedNews[_spawnedNews.Count - 1].SetUpNews(companies[i], newsPatterns[numberOfRandomNew]);
            temp.transform.SetAsFirstSibling();
            temp.SetActive(DetailedInfoManager._instance.currentCompany == companies[i]);
        }
    }

    public void ShowCompanyNews(Company company)
    {
        foreach (News news in _spawnedNews)
        {
            news.gameObject.SetActive(company == news.Company);
           
        }
    }

}

