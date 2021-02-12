using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Campaign : MonoBehaviour
{
    Paramentrs paramentrs;

    Accepter accepter;

    [SerializeField]
    GameObject particleSystem,canvas;

    [SerializeField]
    GameObject[] mans;

    [SerializeField]
    int amount;

    [SerializeField]
    Image progressBar;

    float timer;
    bool statusChecked;

    private void Start()
    {
        progressBar.fillAmount = 0;
        particleSystem.SetActive(false);
        timer = 2f;
        statusChecked = false;
        paramentrs = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<Paramentrs>();
        accepter = GameObject.FindGameObjectWithTag("Level").GetComponent<Accepter>();
        paramentrs.NeedCountInit(amount);
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!statusChecked)
            {
                progressBar.fillAmount = 0;
                timer = 2f;
                for (int i = 0; i < mans.Length; i++)
                {
                    mans[i].GetComponent<RobotBehavior>().LoseAnim();
                }
            }
            other.GetComponent<Controller>().AntennaOn(false);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!statusChecked)
                other.GetComponent<Controller>().AntennaOn(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (timer > 0)
            {
                timer = timer - Time.deltaTime;
                progressBar.fillAmount = (2- timer)/2;
            }
            else
            {
                if (!statusChecked)
                    FinishInit();
                statusChecked = true;
            }
        }
    }

    void FinishInit()
    {
        Destroy(canvas);
        for (int i=0;i<mans.Length;i++)
        {
            mans[i].GetComponent<RobotBehavior>().WinAnim(accepter.target());
        }
        particleSystem.SetActive(true);
        Destroy(particleSystem, 1f);
        //paramentrs.ResultCountInit(amount);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().AntennaOn(false);
    }
}
