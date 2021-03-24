using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBonusManager : MonoBehaviour
{
    //private float timer = 26.0f;
    public Transform[] targetPoint;

    //[SerializeField] private Text timeText;
    [SerializeField]
    GameObject crypta, cryptaPackage;
    void Start()
    {
        for (int i = 0; i < targetPoint.Length; i++)
        {
            SpawnerInit(i);
        }
    }

    /*private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            timeText.text = "" + (int) timer;
        }
    }*/

    void SpawnerInit(int i)
    {
        GameObject coin = Instantiate(crypta, targetPoint[i].position, Quaternion.Euler(-90,125,0));
        coin.transform.SetParent(null);
        coin.transform.SetParent(cryptaPackage.transform);
    }
}