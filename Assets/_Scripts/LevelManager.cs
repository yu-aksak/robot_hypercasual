using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] bool isBonusLevel;
    [SerializeField] GameObject[] missionPrefab;
    [SerializeField] GameObject bonusPrefab;
    [SerializeField] int missionIdOn;

    void Start()
    {

    }

    public void Mission()
    {
        if (isBonusLevel)
        {
            StartBonusMission();
        }
        else
        {
            StartMission(missionIdOn);
        }
    }

    void StartBonusMission()
    {
        for (int i = 0; i < missionPrefab.Length; i++)
        {
            missionPrefab[i].SetActive(false);
        }
        bonusPrefab.SetActive(true);
    }

    void StartMission(int id)
    {
        for (int i = 0; i < missionPrefab.Length; i++)
        {
            missionPrefab[i].SetActive(false);
        }
        missionPrefab[id].SetActive(true);
    }
}