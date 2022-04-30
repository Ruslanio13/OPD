[System.Serializable]
public class Broker
{
    public string Name{get; private set;}
    public float Commision{get; private set;}    
    public float Maintenance{get; private set;}    

    public Broker(string name, float comission, float maintenance)
    {
        Name = name;
        Commision = comission;
        Maintenance = maintenance;
    }


}


