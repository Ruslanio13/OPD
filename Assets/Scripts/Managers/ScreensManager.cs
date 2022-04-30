using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreensManager : MonoBehaviour
{
    [SerializeField] private GameObject marketScreen;
    [SerializeField] private GameObject portfolioScreen;
    [SerializeField] private GameObject _continueButton;
    Info info;
    
    private void Awake() 
    {
        info = FindObjectOfType<Info>();
    }
    private void Start() {
        if(File.Exists(Application.persistentDataPath + "/gamesave.save") && _continueButton != null)
            _continueButton.SetActive(true);
        
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

    public void StartGame()
    {
        if(PreGameManager._instance.CurrentBroker != null && PreGameManager._instance.CurrentDifficulty!=null)
        {
            if(File.Exists(Application.persistentDataPath + "/gamesave.save"))
                File.Delete(Application.persistentDataPath + "/gamesave.save");
            SceneManager.LoadScene(1);
        }
    }
    public void ContinueGame()
    {
           SceneManager.LoadScene(1);
    }
}
