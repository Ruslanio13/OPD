using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "News", menuName = "NewsPattern", order = 1)]
public class NewsSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] public string title = "/Компания .../ ";
    
    [TextArea(6, 6)]
    [SerializeField] public string text;
  
  




}
