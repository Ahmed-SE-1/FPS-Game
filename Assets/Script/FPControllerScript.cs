using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPControllerScript : MonoBehaviour
{
    public CharacterController cc;
    public float walkSpeed = 5f;
    public float jumpPower = 5f;
    public float runSpeed = 10f;
    public float gravity = 9.81f; //Gravity value when character jumps.
    public float lookSensitivity = 2f;
    public float maxLookX = 45f;
    Vector3 moveDirection = Vector3.zero;
    public bool canMove = true;
    float rotate = 0;
    public Camera cam;


    // Update is called once per frame
    void Update()
    {
        //Walking
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float moveX = canMove ? (isRunning ? runSpeed : walkSpeed) * x : 0;
        float moveZ = canMove ? (isRunning ? runSpeed : walkSpeed) * y : 0;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Forward variable means movement Z will always be in 0,0,1
        //direction. So if we rotate the character,
        //forward will also rotate and movement will be in
        //the direction character is facing.

        Vector3 right = transform.TransformDirection(Vector3.right);
        //Right variable means movement X will always be in 1,0,0
        //direction. So if we rotate the character,
        //right will also rotate and movement will be in
        //the direction character is facing.

        moveDirection = (forward * moveZ) + (right * moveX);
        
        //Jumping
        if (Input.GetButton("Jump") && canMove && cc.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = 0;
        }
        if (!cc.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        cc.Move(moveDirection * Time.deltaTime);

        //Camera movement, head movement
        //value on x-axis will move camera along y-axis and z-axis.
        rotate += Input.GetAxis("Mouse Y") * lookSensitivity;
        rotate = Mathf.Clamp(rotate, -maxLookX, maxLookX);
        cam.transform.localRotation = Quaternion.Euler(rotate, 0, 0);

        //Side movement
        float rotateY = Input.GetAxis("Mouse X") * lookSensitivity;
        transform.rotation *= Quaternion.Euler(0, rotateY, 0);


    }
}
