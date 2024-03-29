[System.Serializable]
public class Country
{
    public string Name {get; private set;}
    public int ID {get; private set;}

    public delegate void ChangePriceVolatile(float a, float b);
    public event ChangePriceVolatile HandlePriceVolatility;
    
    public void OnPriceVolatilityChange(float max, float min)
    {
        HandlePriceVolatility?.Invoke(max, min);
    }

    public Country(string name, int ID)
    {
        this.ID = ID;
        Name = name;
    }



}
