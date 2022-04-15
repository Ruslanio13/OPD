using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject marketScreen;
    [SerializeField] GameObject portfolioScreen;
    Info info;
    
    private void Awake() 
    {
        info = FindObjectOfType<Info>();
    }
    public void Exit()
    {
        Application.Quit();
    } 


    

    public void Portfolio()
    {
        foreach (GameObject item in info._marketHelpMessages) item.SetActive(false);
        marketScreen.SetActive(false);
        portfolioScreen.SetActive(true);
    }

    public void Market()
    {
        foreach (GameObject item in info._portfolioHelpMessages) item.SetActive(false);
        marketScreen.SetActive(true);
        portfolioScreen.SetActive(false);
    }

    public void MainScreen()
    {
        SceneManager.LoadScene(1);
    }
}
