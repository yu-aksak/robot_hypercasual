using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject levelText, conditionsText, joystick,winWindow, loseWindow,
        controller, missionCanvas, gameCanvas, bonusLevelButton, timerText, bonusWindow, shopCanvas;

    [SerializeField] private GameObject levelsContent;
    
    private ButtonLevelManager[] levelButtons;

    [SerializeField] private Text cryptaInfoText;
    private int currentLevel = 1;
    private int bonusLevel = 1;
    public int crypta = 0;
    private void Start()
    {
        levelButtons = levelsContent.GetComponentsInChildren<ButtonLevelManager>();
        CryptaInfoTextRefresh(Crypta);
        
    }

    public void ToMenuFromShop(bool tmp)
    {
        shopCanvas.SetActive(tmp);
        missionCanvas.SetActive(!tmp);
    }

    public void ToMenu()
    {
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        if(level == null) 
            level = GameObject.FindGameObjectWithTag("LevelBonusManager");
        Destroy(level);
        SetCanvas();
    }
    public void NextLevel()
    {
        currentLevel++;
        bonusLevelButton.SetActive(false);
        LoadLevel(0, currentLevel);
    }

    public void LoadLevel(int typeLevel, int numberLevel)
    {
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        if(level == null) 
            level = GameObject.FindGameObjectWithTag("LevelBonusManager");
        Destroy(level);
        InitLevel(typeLevel, numberLevel);
    }
    
    public void InitLevel(int typeLevel, int level)
    {
        GameObject levelManager = GameObject.FindGameObjectWithTag("Level Manager");
        if (levelManager.GetComponent<LevelGeneration>().Generate(typeLevel, level))
        {
            levelManager.GetComponent<Paramentrs>().Init();
            if(typeLevel != 1)
                currentLevel = level;
            LevelInfoTextRefresh(level);
            StartMission();
        }
        else
        {
            SetCanvas();
        }
    }

    private void OnApplicationQuit()
    {
    }

    public void StartBonusLevel()
    {
        LoadLevel(1, bonusLevel);
        timerText.SetActive(true);
        GameObject levelManager = GameObject.FindGameObjectWithTag("Level Manager");
        levelManager.GetComponent<Paramentrs>().BonusLevel();
    }

    public void BonusStop(int crypta)
    {
        Crypta += crypta;
        bonusWindow.SetActive(true);
        timerText.SetActive(false);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    
    public void Restart()
    {
        LoadLevel(0, currentLevel);
    }
    public void CompleteInit()
    {
        levelText.SetActive(false);
        conditionsText.SetActive(false);
        joystick.SetActive(false);
    }

    private void SetCanvas()
    {
        winWindow.SetActive(false);
        loseWindow.SetActive(false);
        bonusWindow.SetActive(false);
        joystick.SetActive(false);
        gameCanvas.SetActive(false);
        missionCanvas.SetActive(true);
        controller.SetActive(false);
    }
    
    public void Win(int crypta)
    {
        if (!loseWindow.activeInHierarchy)
        {
            if (!levelButtons[currentLevel - 1].Status.Equals("passed"))
            {

                if (currentLevel % 3 == 0)
                {
                    //bonusLevel++;
                    bonusLevelButton.SetActive(true);
                }

                if (currentLevel < levelButtons.Length)
                {
                    levelButtons[currentLevel].Status = "active";
                    levelButtons[currentLevel].SetInteractable(true);
                }

                levelButtons[currentLevel - 1].Status = "passed";
            }

            Crypta += crypta;
            winWindow.SetActive(true);
        }
    }
    
    public void Lose()
    {
        loseWindow.SetActive(true);
    }
    
    public void StartMission()
    {
        winWindow.SetActive(false);
        bonusWindow.SetActive(false);
        loseWindow.SetActive(false);
        levelText.SetActive(true);
        conditionsText.SetActive(true);
        joystick.SetActive(true);
        gameCanvas.SetActive(true);
        missionCanvas.SetActive(false);
        controller.SetActive(true);
        controller.GetComponent<Controller>().enabled = true;
        //controller.transform.position = new Vector3(0, 0.15f, 0);
    }

    public int Crypta
    {
        get => crypta;
        set => crypta = value;
    }

    public void CryptaInfoTextRefresh(int amount)
    {
        cryptaInfoText.text = "" + amount;
    }
    public void LevelInfoTextRefresh(int level)
    {
        levelText.GetComponent<Text>().text = "LVL: " + level;
    }
}
