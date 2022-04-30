[System.Serializable]
public class Broker
{
    public string Name{get; private set;}
    public float Commision{get; private set;}    

    public Broker(string name, float comission)
    {
        Name = name;
        Commision = comission;
    }


}


