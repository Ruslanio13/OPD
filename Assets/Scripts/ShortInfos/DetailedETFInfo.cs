using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedETFInfo : DetailedInfo
{
    [SerializeField] private GameObject _shareInfoPrefab;
    [SerializeField] private RectTransform _verticalBox;
    private List<ShortInfo> fondInUI = new List<ShortInfo>();
    public override void SetInfo(Securities sec)
    {
        if (sec is ETF)
        {
            foreach (var item in fondInUI)
            {
                Destroy(item.gameObject);
            }

            fondInUI.Clear();
            _verticalBox.sizeDelta = Vector2.zero;



            foreach (var item in (sec as ETF).Fond)
            {
                var temp = Instantiate(_shareInfoPrefab, _verticalBox);
                
                var temp1 = temp.GetComponent<ShortInfo>();
                temp1.SetInfo(item.Item1, item.Item2);
                fondInUI.Add(temp1);
            }
            if(fondInUI.Count % 2 == 0)
                _verticalBox.sizeDelta += new Vector2(0, 85f) * fondInUI.Count / 2;
            else
                _verticalBox.sizeDelta += new Vector2(0, 85f) * (fondInUI.Count / 2 + 1);
        }
        else if(sec is Share)
        {
            foreach (var item in fondInUI)
            {
                item.UpdateInfo();
            }
        }
    }
}

