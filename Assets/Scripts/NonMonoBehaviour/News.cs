[System.Serializable]
public class News
{
    public Company comp { get; private set; }

    public string MainText { get; private set; }
    public string TitleText { get; private set; }
    public float maxChange { get; private set; }
    public float minChange { get; private set; }

    public News(Company comp, NewsSO template)
    {
        TitleText = template.title;
        MainText = template.text;
        if (comp != null)
        {
            this.comp = comp;
            maxChange = template.maxChange;
            minChange = template.minChange;

            comp.SetMaxPriceChange(template.maxChange);
            comp.SetMinPriceChange(template.minChange);
        }
    }
}