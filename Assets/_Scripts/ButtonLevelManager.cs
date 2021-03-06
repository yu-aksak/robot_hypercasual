using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevelManager : MonoBehaviour
{
    public int currentConditions;
    public int level;
    public int conditions;
    public string status;
    private UIManager _uiManager;
    //private LevelGeneration _levelGeneration;
    private void Start()
    {
        level = int.Parse(gameObject.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text);

        //_levelGeneration = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelGeneration>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    { 
        _uiManager.InitLevel(0, level);
        _uiManager.StartMission();
    }
}
