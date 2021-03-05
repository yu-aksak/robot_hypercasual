using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    Joystick joystick;

    [SerializeField] Camera camera;

    [SerializeField] GameObject antenaObject,antenaEffect,PS;

    [SerializeField] Animator playersModel, antena;

    Rigidbody playerController;

    [SerializeField] float moveForce, moveCameraSpeed, rotationSpeed;

    void OnEnable()
    {
        gameObject.transform.position = new Vector3(0, 1, 0);
        camera.orthographicSize = 20;
        playerController = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<Joystick>();
        joystick.Horizontal = 0;
        joystick.Vertical = 0;
    }

    private void OnDisable()
    {
        antena.SetBool("status", false);
        playersModel.SetBool("isMove", false);
    }

    void FixedUpdate()
    {
        if (camera.orthographicSize > 8.5)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 8.43f, 1 * Time.deltaTime);
        }
        Vector3 moveToTarger= new Vector3(joystick.Horizontal * moveForce * Time.deltaTime, playerController.velocity.y, joystick.Vertical * moveForce * Time.deltaTime);
        playerController.velocity = moveToTarger;
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            playersModel.SetBool("isMove", true);
            RotateTowardMovementVector(moveToTarger);
        }
        else
        {
            if (playersModel.GetBool("isMove"))
                playersModel.SetBool("isMove", false);
        }
        FollowingCamera();
    }

    public void AntennaOn(bool status)
    {
        if (status)
        {
            antenaObject.SetActive(true);
            antena.enabled = true;
            antenaEffect.SetActive(true);
        }
        else
        {
            antena.SetBool("status",true);
            StartCoroutine("AntenaDisabler");
            antenaEffect.SetActive(false);
        }
    }

    public IEnumerator AntenaDisabler()
    {
        yield return new WaitForSeconds(0.29f);
        antena.SetBool("status", false);
        antena.enabled = false;
        antenaObject.SetActive(false);
    }

    void FollowingCamera()
    {
        Vector3 newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 15, gameObject.transform.position.z -13);
        camera.transform.position = Vector3.Lerp(camera.transform.position, newPos, Time.deltaTime * moveCameraSpeed);
    }
    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) { return; }
        movementDirection.y = 0;
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    public void AnimationDisabler()
    {
        PS.SetActive(false);
        playersModel.enabled = false;
    }
}