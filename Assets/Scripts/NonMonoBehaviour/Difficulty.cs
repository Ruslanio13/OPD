[System.Serializable]
public class Difficulty
{
    public string Name{get; private set;}
    public float Coefficient{get; private set;}


    public Difficulty(string name, float coef)
    {
        Name = name;
        Coefficient = coef;
    }
}

