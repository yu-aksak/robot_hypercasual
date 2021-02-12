using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject levelText, conditionsText, joystick,blur,
        contidionForLevel, levelPanel;

    [SerializeField] Text cryptaInfoText, timerText;

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
        Application.LoadLevel(0);
    }

    public void CompleteInit()
    {
        blur.SetActive(true);
        levelText.SetActive(false);
        conditionsText.SetActive(false);
        joystick.SetActive(false);
    }

    public void StartMission()
    {
        blur.SetActive(true);
        levelText.SetActive(false);
        conditionsText.SetActive(false);
        joystick.SetActive(true);
    }

    public void CryptaInfoTextRefresh(int amount)
    {
        cryptaInfoText.text = "" + amount;
    }
}
