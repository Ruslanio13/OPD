using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject marketScreen;
    [SerializeField] GameObject portfolioScreen;
    
    private void Start() {
    }
    
    public void Exit()
    {
        Application.Quit();
    } 


    

    public void Portfolio()
    {
        marketScreen.SetActive(false);
        portfolioScreen.SetActive(true);
    }

    public void Market()
    {
        marketScreen.SetActive(true);
        portfolioScreen.SetActive(false);
    }

    public void MainScreen()
    {
        SceneManager.LoadScene(1);
    }
}
