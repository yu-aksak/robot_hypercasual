using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accepter : MonoBehaviour
{
    [SerializeField] Transform[] targetsFree;

    int lastID;
    void Start()
    {
        lastID = 0;
    }
    public Transform target()
    {
        Debug.Log(gameObject.name);
        lastID++;
        return targetsFree[lastID-1];
        
    }
}
