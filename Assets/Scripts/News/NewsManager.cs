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
        int i;
        int numberOfRandomNew;
        GameObject temp;
        for (i = 0; i < companies.Count; i++)
        {
            numberOfRandomNew = UnityEngine.Random.Range(0, newsPatterns.Count);

            temp = Instantiate(newsPrefab, newsFeed.transform);
            newsPrefab.GetComponent<NewsSetUp>().SetUpNews(companies[i], newsPatterns[numberOfRandomNew]);
            companies[i].SetMaxPriceChange(newsPatterns[numberOfRandomNew].maxChange);
            companies[i].SetMinPriceChange(newsPatterns[numberOfRandomNew].minChange);
            Debug.Log(companies[i].GetNameOfCompany() + " " + numberOfRandomNew);
        }

    }
    
        
        

}
