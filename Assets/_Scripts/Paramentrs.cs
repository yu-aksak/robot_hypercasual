using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paramentrs : MonoBehaviour
{
    int resultCount,needCount,crypta;

    GameObject[] bridges;

    [SerializeField]
    UIManager uiManager;

    [SerializeField]
    Text levelCounterInfo;

    [SerializeField]
    bool withoutBridges;

    void Start()
    {
        crypta = 0;
        withoutBridges = false;
        resultCount = 0;
        BridgesInit();
        if (bridges.Length == 0)
        {
            withoutBridges = true;
        }
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
        uiManager.CompleteInit();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().enabled = false;
    }

    public void MissionFailed()
    {
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
}