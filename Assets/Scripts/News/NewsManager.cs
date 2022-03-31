using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    [SerializeField] GameObject newsFeed;
    [SerializeField] GameObject newsPrefab;
    [SerializeField] List<NewsSO> newsPatterns = new List<NewsSO>();
    public static NewsManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void SpawnNews(List<Company> companies)
    {
        foreach(Company company in companies)
        {
            int numberOfRandomNew = UnityEngine.Random.Range(0, newsPatterns.Count);
            GameObject temp;
            temp = Instantiate(newsPrefab, newsFeed.transform);
            newsPrefab.GetComponent<NewsSetUp>().SetUpNews(company, newsPatterns[numberOfRandomNew]);
            company.SetMaxPriceChange(newsPatterns[numberOfRandomNew].maxChange);
            company.SetMinPriceChange(newsPatterns[numberOfRandomNew].minChange);
        }
        
    }
    
        
        

}
