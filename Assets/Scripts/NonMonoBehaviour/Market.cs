using UnityEngine;

public class Market
{

    public string Name{get; private set;}
    public int ID{get; private set;}

    public Market(string name, int id)
    {
        ID = id;
        Name = name;
    }

}
