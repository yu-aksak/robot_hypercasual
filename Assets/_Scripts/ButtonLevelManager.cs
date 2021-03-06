using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevelManager : MonoBehaviour
{
    public int level;
    public string status;
    private UIManager _uiManager;
    //private LevelGeneration _levelGeneration;
    private void Start()
    {
        level = int.Parse(gameObject.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text);
        if (status.Equals("unavailable"))
            SetInteractable(false);
        else
            SetInteractable(true);
        //_levelGeneration = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelGeneration>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    { 
        _uiManager.InitLevel(0, level);
        _uiManager.StartMission();
    }

    public string Status
    {
        get => status;
        set => status = value;
    }

    public void SetInteractable(bool temp)
    {
        gameObject.GetComponent<Button>().interactable = temp;
    }
}
