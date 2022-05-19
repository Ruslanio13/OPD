using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField] public List<GameObject> _portfolioHelpMessages = new List<GameObject>();
    [SerializeField] public List<GameObject> _marketHelpMessages = new List<GameObject>();
    [SerializeField] public List<GameObject> _startScreenHelpMessages = new List<GameObject>();
    
    public void ShowMarketHelp()
    {
        _marketHelpMessages[0].SetActive(true);
    }

    public void ToTheSecondMarket()
    {
        _marketHelpMessages[0].SetActive(false);
        _marketHelpMessages[1].SetActive(true);
    }

    public void ToTheThirdMarket()
    {
        _marketHelpMessages[1].SetActive(false);
        _marketHelpMessages[2].SetActive(true);
    }

    public void ToTheFourthMarket()
    {
        _marketHelpMessages[2].SetActive(false);
        _marketHelpMessages[3].SetActive(true);
    }

    public void CloseMarketHelp()
    {
        _marketHelpMessages[3].SetActive(false);
    }


    public void ShowPortfolioHelp()
    {
        _portfolioHelpMessages[0].SetActive(true);
    }

    public void ToTheSecondPortfolio()
    {
        _portfolioHelpMessages[0].SetActive(false);
        _portfolioHelpMessages[1].SetActive(true);
    }

    public void ToTheThirdPortfolio()
    {
        _portfolioHelpMessages[1].SetActive(false);
        _portfolioHelpMessages[2].SetActive(true);
    }

    public void ClosePortfolioHelp()
    {
        _portfolioHelpMessages[2].SetActive(false);
    }

    public void ShowStartScreenHelp()
    {
        _startScreenHelpMessages[0].SetActive(true);
    }
    public void CloseStartScreenHelp()
    {
        _startScreenHelpMessages[0].SetActive(false);
    }
}
