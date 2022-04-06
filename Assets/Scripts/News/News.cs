using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class News
{
    public Company comp { get; private set; }
    public NewsSO template { get; private set; }

    public News(Company comp, NewsSO template)
    {
        this.comp = comp;
        this.template = template;

        comp.SetMaxPriceChange(template.maxChange);
        comp.SetMinPriceChange(template.minChange);
    }
}