using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    int speed;

    void Update()
    {
        transform.Rotate(0,0,speed*Time.deltaTime);
    }
}
