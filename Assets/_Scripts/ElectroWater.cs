using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Level Manager").GetComponent<Paramentrs>().MissionFailed();
        }
    }
}
