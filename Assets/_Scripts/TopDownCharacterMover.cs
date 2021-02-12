using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class TopDownCharacterMover : MonoBehaviour
{
    InputHandler _input;

    [SerializeField]
    Animator playersModel;
 
    [SerializeField]
    float MovementSpeed, RotationSpeed,CameraFolowingSpeed;

    [SerializeField]
    Camera mainCam;

    Vector3 position;

    CharacterController playerController;

    private void Start()
    {
        position = transform.position;
        playerController = GetComponent<CharacterController>();
        _input = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((_input.InputVector.x!=0|| _input.InputVector.y != 0) && !Input.GetMouseButton(1))
        {
            Debug.Log(""+_input.InputVector.x+"/"+_input.InputVector.y);
            var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
            moveToPosition();
            var movementVector = MoveTowardTarget(targetVector);
            RotateTowardMovementVector(movementVector);
            playersModel.SetBool("isMove", true);
        }
        else
        {
            if (Input.GetMouseButton(1) && (_input.InputVector.x < 0.05f || _input.InputVector.y < 0.05f))
            {
                localePostition();
                moveToPositionMouse();
            }
            else
            {
                playersModel.SetBool("isMove", false);
            }
        }

        //if (_input.InputVector.x>0.1f || _input.InputVector.x < 0.1f)
        //{
        //    playersModel.SetTrigger("isMove");
        //}
        //else
        //{
        //    if (_input.InputVector.y > 0.1f || _input.InputVector.y < 0.1f)
        //    {
        //        playersModel.SetTrigger("isMove");
        //    }
        //    else
        //    {
        //        if (Input.GetMouseButton(1))
        //        {
        //            playersModel.SetTrigger("isMove");
        //        }
        //        else
        //        {
        //            playersModel.SetTrigger("isIdle");
        //        }
        //    }
        //}
        
        followingCamera();
    }

    void followingCamera()
    {
        Vector3 newPos = new Vector3(gameObject.transform.position.x - 10, gameObject.transform.position.y + 14, gameObject.transform.position.z - 10);
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newPos, Time.deltaTime* CameraFolowingSpeed);
    }

    void localePostition()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            Debug.Log(position);
        }
    }

    void moveToPositionMouse()
    {
        if (Vector3.Distance(transform.position, position) >1.3f)
        {
            Quaternion newRotation = Quaternion.LookRotation(position - transform.position, Vector3.forward);
            playersModel.SetBool("isMove", true);
            newRotation.x = 0f;
            newRotation.z = 0f;
            RotateTowardMovementVector(position - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * RotationSpeed);
            moveToPosition();
        }
        else
        {
            playersModel.SetBool("isMove", false);
        }
    }
    Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        targetVector = Quaternion.Euler(0, mainCam.transform.rotation.eulerAngles.y, 0) * targetVector;
        return targetVector;
    }

    void moveToPosition()
    {
        playerController.SimpleMove(transform.forward * (MovementSpeed*Time.deltaTime*100));
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if(movementDirection.magnitude == 0) { return; }
        movementDirection.y = 0;
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
    }
}
