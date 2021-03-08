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
        controller, missionCanvas, gameCanvas;

    public GameObject levelsContent;
    public ButtonLevelManager[] levelButtons;

    [SerializeField] private Text cryptaInfoText, timerText;
    public int currentLevel = 1;
    private void Start()
    {
        levelButtons = levelsContent.GetComponentsInChildren<ButtonLevelManager>();
        CryptaInfoTextRefresh(0);
        
    }
    public void ToMenu()
    {
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        Destroy(level);
        SetCanvas();
    }
    public void NextLevel()
    {
        currentLevel++;
        LoadLevel();
    }

    public void LoadLevel()
    {
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        Destroy(level);
        InitLevel(0, currentLevel);
    }
    
    public void InitLevel(int typeLevel, int level)
    {
        GameObject levelManager = GameObject.FindGameObjectWithTag("Level Manager");
        if (levelManager.GetComponent<LevelGeneration>().Generate(typeLevel, level))
        {
            levelManager.GetComponent<Paramentrs>().Init();
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

    public void Quit()
    {
        Application.Quit();
    }
    
    public void Restart()
    {
        LoadLevel();
    }
    public void CompleteInit()
    {
        levelText.SetActive(false);
        conditionsText.SetActive(false);
        joystick.SetActive(false);
    }

    private void SetCanvas()
    {
        joystick.SetActive(false);
        gameCanvas.SetActive(false);
        missionCanvas.SetActive(true);
        controller.SetActive(false);
    }
    
    public void Win()
    {
        
        if (!levelButtons[currentLevel - 1].Status.Equals("passed"))
        {
            if (currentLevel < levelButtons.Length)
            {
                levelButtons[currentLevel].Status = "active";
                levelButtons[currentLevel].SetInteractable(true);
            }
            levelButtons[currentLevel - 1].Status = "passed";
        }
        winWindow.SetActive(true);
    }
    
    public void Lose()
    {
        loseWindow.SetActive(true);
    }
    
    public void StartMission()
    {
        winWindow.SetActive(false);
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

    public void CryptaInfoTextRefresh(int amount)
    {
        cryptaInfoText.text = "" + amount;
    }
    public void LevelInfoTextRefresh(int level)
    {
        levelText.GetComponent<Text>().text = "LVL: " + level;
    }
}
