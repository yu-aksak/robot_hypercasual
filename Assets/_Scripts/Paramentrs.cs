using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Paramentrs : MonoBehaviour
{
    int resultCount;
    public static int needCount;
    int crypta;

    GameObject[] bridges;

    [SerializeField] UIManager uiManager;

    [SerializeField] Text levelCounterInfo;

    [SerializeField] bool withoutBridges;

    private float timer = 26.0f;
    [SerializeField] private Text timeText;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        withoutBridges = false;
        BridgesInit();
        if (bridges.Length == 0)
        {
            withoutBridges = true;
        }
    }

    public void SetZero()
    {
        crypta = 0;
        uiManager.CryptaInfoTextRefresh(crypta);
        resultCount = 0;
        needCount = 0;
    }
    
    public int needCountOnLevel()
    {
        return resultCount;
    }

    public void NeedCountInit(int amount)
    {
        needCount = needCount + amount;
        TextRefresher();
    }

    public void ResultCountInit(int amount)
    {
        resultCount = resultCount + amount;
        TextRefresher();
        if (!withoutBridges)
        {
            BridgesInit();
        }
        if (resultCount >= needCount)
        {
            MissionCompleted();
        }
    }

    void MissionCompleted()
    {
        uiManager.Win(crypta);
        uiManager.CompleteInit();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().enabled = false;
    }

    public void MissionFailed()
    {
        uiManager.Lose();
        uiManager.CompleteInit();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().AnimationDisabler();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().enabled = false;
    }

    void BridgesInit()
    {
        bridges = GameObject.FindGameObjectsWithTag("Bridge");
        for (int i = 0; i < bridges.Length; i++)
        {
            bridges[i].GetComponent<Bridge>().Init(resultCount);    
        }
    }

    void TextRefresher()
    {
        levelCounterInfo.text = "" + resultCount + "/" + needCount;
    }

    public void TransferCrypta(int amount)
    {
        crypta++;
        uiManager.CryptaInfoTextRefresh(crypta);
    }
    public void BonusLevel()
    {
        StartCoroutine(Timer());
    }
    
    IEnumerator Timer()
    {
        for(;;)
            if (timer > 0)
            {
                timer -= 1;
                timeText.text = "" + (int) timer;
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                timer = 26;
                uiManager.BonusStop(crypta);
                uiManager.CompleteInit();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().enabled = false;
                break;
            }
    }
}