using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBonusManager : MonoBehaviour
{

    public Transform[] targetPoint;

    [SerializeField]
    GameObject crypta, cryptaPackage;
    void Start()
    {
        for (int i = 0; i < targetPoint.Length; i++)
        {
            SpawnerInit(i);
        }
    }

    void SpawnerInit(int i)
    {
        GameObject coin = Instantiate(crypta, targetPoint[i].position, Quaternion.Euler(-90,125,0));
        coin.transform.SetParent(null);
        coin.transform.SetParent(cryptaPackage.transform);
    }
}