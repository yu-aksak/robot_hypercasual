using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject levelText, conditionsText, joystick,winWindow, loseWindow,
        controller, missionCanvas, gameCanvas;
    
    [SerializeField] private Text cryptaInfoText, timerText;
    private int maxLevel = 1;
    private void Start()
    {
        CryptaInfoTextRefresh(0);
    }
    public void Quit()
    {
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        Destroy(level);
        SetCanvas();
    }
    public void NextLevel()
    {
        maxLevel++;
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        Destroy(level);
        InitLevel(0, maxLevel);
    }

    public void InitLevel(int typeLevel, int level)
    {
        GameObject levelManager = GameObject.FindGameObjectWithTag("Level Manager");
        if (levelManager.GetComponent<LevelGeneration>().Generate(typeLevel, level))
        {
            levelManager.GetComponent<Paramentrs>().Init();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            maxLevel = level;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            LevelInfoTextRefresh(level);
            StartMission();
        }
        else
        {
            SetCanvas();
        }
    }

    public void Restart()
    {
        
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
        winWindow.SetActive(true);
    }
    
    public void Lose()
    {
        loseWindow.SetActive(true);
    }
    
    public void StartMission()
    {
        winWindow.SetActive(false);
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
