using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    [SerializeField] GameObject newsFeed;
    [SerializeField] GameObject newsPrefab;
    [SerializeField] List<NewsSO> newsPatterns = new List<NewsSO>();
    



    public void SpawnNews(List<Company> companies)
    {
        foreach(Company company in companies)
        {
            GameObject temp;
            temp = Instantiate(newsPrefab, newsFeed.transform);
            newsPrefab.GetComponent<NewsSetUp>().SetUpNews(company, newsPatterns[UnityEngine.Random.Range(0, newsPatterns.Count)]);
        }
        
    }
    
        
        

}
