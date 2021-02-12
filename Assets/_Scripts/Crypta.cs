using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crypta : MonoBehaviour
{
    [SerializeField]
    GameObject psPickUp, cryptaCircle;

    int speedRot;

    private void Start()
    {
        speedRot = Random.Range(30,60);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            psPickUp.SetActive(true);
            psPickUp.transform.SetParent(null);
            Destroy(gameObject);
        }
        GameObject.FindGameObjectWithTag("Level Manager").GetComponent<Paramentrs>().
            TransferCrypta(1);
    }
    void Update()
    {
        cryptaCircle.transform.Rotate(0, 0, speedRot * Time.deltaTime);
    }
}
