[System.Serializable]
public class Country
{
    public string Name {get; private set;}

    public Country(string name)
    {
        Name = name;
    }
}
