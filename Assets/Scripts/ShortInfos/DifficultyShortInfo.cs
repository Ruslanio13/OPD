using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyShortInfo : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _name;
    [SerializeField]private Button _selectDifficulty;
    private Difficulty Difficulty;
    private void Start() {
        _selectDifficulty.onClick.AddListener(() => PreGameManager._instance.SetDifficulty(Difficulty));
    }
    public void SetDifficulty(Difficulty diff)
    {
        Difficulty = diff;
        _name.text = diff.Name;
    }



}

