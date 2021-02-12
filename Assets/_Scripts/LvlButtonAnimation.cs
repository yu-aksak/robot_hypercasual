using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlButtonAnimation : MonoBehaviour
{
    public float[] force;
    public GameObject[] circles;

    // Update is called once per frame
    void FixedUpdate()
    {
        circles[0].transform.Rotate(0, 0, force[0] * Time.deltaTime);
        circles[1].transform.Rotate(0, 0, -force[1] * Time.deltaTime);
    }
}
