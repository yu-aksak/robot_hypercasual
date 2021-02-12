using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAccepter : MonoBehaviour
{
    [SerializeField]
    GameObject particleSystemPart1, particleSystemPart2, rocket, robotModel;

    [SerializeField]
    Animator glass;

    bool check;
    private void Start()
    {
        check = false;
        particleSystemPart1.SetActive(false);
        particleSystemPart2.SetActive(false);
        robotModel.SetActive(false);
        glass.enabled=false;
        rocket.GetComponent<Animator>().enabled = false;
    }
    public void RobotReached()
    {
        robotModel.SetActive(true);
        glass.enabled = true;
        particleSystemPart1.SetActive(true);
        StartCoroutine("OnPart2PS");
    }

    private void Update()
    {
        if (check)
        {
            rocket.transform.Translate(0, 0, 10 * Time.deltaTime);
        }
        
    }
    public IEnumerator OnPart2PS()
    {
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("Level Manager").GetComponent<Paramentrs>().ResultCountInit(1);
        particleSystemPart2.SetActive(true);
        check = true;
        Destroy(rocket, 5);
    }
}
