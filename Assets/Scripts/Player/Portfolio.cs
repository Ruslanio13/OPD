using UnityEngine;
using System.Collections.Generic; 



public class Portfolio
{
    private Dictionary<CompanySO, int> _portfolio;

    


    public void BuySecurities(CompanySO company, int amount)
    {
        
        if(amount*company.GetPrice() > PlayerManager._instance.GetBalance())


        if(_portfolio.ContainsKey(company))
            _portfolio[company] += amount;
        else
            _portfolio.Add(company,amount);


    }
}