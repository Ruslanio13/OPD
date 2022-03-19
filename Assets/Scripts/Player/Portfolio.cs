using UnityEngine;
using System.Collections.Generic; 



public class Portfolio
{
    private Dictionary<Company, int> _portfolio;




    public void BuySecurities(Company company, Securities securities, int amount)
    {

        if (amount * securities.GetPrice() > PlayerManager._instance.GetBalance())

            if (_portfolio.ContainsKey(company))
                _portfolio[company] += amount;

            else
                _portfolio.Add(company, amount);

    }
}
