using System;
using UnityEngine;

public class Camera2Movement : MonoBehaviour
{
    public float sensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private Transform playerPos;
    private float rotY = 0.0f; 

    void Start()
    {
        playerPos = GameObject.Find("Camera2Pos").transform;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
    }

    void FixedUpdate()
    {
        Vector3 speed = new Vector3();
        float mouseX = Input.GetAxis("Horizontal");

        rotY += mouseX * sensitivity * Time.deltaTime;

        Quaternion localRotation = Quaternion.Euler(0, rotY, 0);
        transform.rotation = localRotation;

        Vector3 targetPos = Vector3.SmoothDamp(transform.position, playerPos.position,ref speed, 0.05f);
        

        if (Input.GetAxis("Vertical") < 0)
        {
            transform.position = new Vector3(targetPos.x, targetPos.y, targetPos.z) + playerPos.forward;
        }
        else 
        {
            transform.position = new Vector3(targetPos.x, targetPos.y, targetPos.z);
        }
    }
}