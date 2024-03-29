[System.Serializable]
public class News
{
    public Company comp { get; private set; }

    public string MainText { get; private set; }
    public string TitleText { get; private set; }
    public float maxChange { get; private set; }
    public float minChange { get; private set; }

    public void SetUpNews<T>(T obj, NewsSO template)
    {
        if(obj.GetType() == typeof(Company))
        {
            comp = (obj as Company);
            TitleText = "Компания " + (obj as Company).GetNameOfCompany() + " " + template.title;
            (obj as Company).ChangePriceVolatility(template.maxChange, template.minChange);
        }
        else
        {
            TitleText = "Страна " + (obj as Country).Name + " " + template.title;
            (obj as Country).OnPriceVolatilityChange(template.maxChange, template.minChange);
        }       
       
        MainText = template.text;
    
    }
}