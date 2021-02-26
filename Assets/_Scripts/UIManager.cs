using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject levelText, conditionsText, joystick,blur,
        controller, levelPanel;
    
    [SerializeField] private Text cryptaInfoText, timerText;
    private int maxLevel = 1;
    private void Start()
    {
        CryptaInfoTextRefresh(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        maxLevel++;
        GameObject levelManager = GameObject.FindGameObjectWithTag("Level Manager");
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        Destroy(level);
        levelManager.GetComponent<LevelGeneration>().Generate(0, maxLevel);
        StartMission();
    }

    public void CompleteInit()
    {
        blur.SetActive(true);
        levelText.SetActive(false);
        conditionsText.SetActive(false);
        joystick.SetActive(false);
        //controller.SetActive(false);
    }

    public void StartMission()
    {
        blur.SetActive(false);
        levelText.SetActive(true);
        conditionsText.SetActive(true);
        joystick.SetActive(true);
        controller.GetComponent<Controller>().enabled = true;
        //controller.SetActive(true);
        //controller.transform.position = new Vector3(0, 0.15f, 0);
    }

    public void CryptaInfoTextRefresh(int amount)
    {
        cryptaInfoText.text = "" + amount;
    }
}
