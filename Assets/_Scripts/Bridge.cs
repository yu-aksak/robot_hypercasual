using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bridge : MonoBehaviour
{
    [SerializeField]
    int needToBuild;

    [SerializeField]
    GameObject beforeBridge, afterBridge;

    [SerializeField]
    Text info;

    [SerializeField]
    Image fillCircle;

    private void Start()
    {
        beforeBridge.SetActive(true);
        afterBridge.SetActive(false);
    }

    public void Init(int resultCount)
    {
        if (needToBuild<= resultCount)
        {
            beforeBridge.SetActive(false);
            afterBridge.SetActive(true);
            afterBridge.transform.SetParent(null);
            Destroy(gameObject);
        }
        else
        {
            info.text = "" + resultCount + "|" + needToBuild;
            fillCircle.fillAmount = (float)resultCount / (float)needToBuild;
        }
    }
}