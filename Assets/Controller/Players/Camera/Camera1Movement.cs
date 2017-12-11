using Assets.Controller.Misc;
using System;
using UnityEngine;

public class Camera1Movement : MonoBehaviour
{
    public float sensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private Transform playerPos;
    private float rotY = 0.0f;

    void Start()
    {
        playerPos = GameObject.Find("Camera1Pos").transform;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
    }

    void FixedUpdate()
    {
        Vector3 speed = new Vector3();
        //float mouseX = SerialInput.AnalogY;
        float mouseX = 0;
        if (SerialInput.DirLeftButton > 0 && SerialInput.DirRightButton == 0)
            mouseX = -1;
        else if (SerialInput.DirRightButton > 0 && SerialInput.DirLeftButton == 0)
            mouseX = 1;

        rotY += mouseX * sensitivity * Time.deltaTime;

        Quaternion localRotation = Quaternion.Euler(0, rotY, 0);
        transform.rotation = localRotation;

        Vector3 targetPos = Vector3.SmoothDamp(transform.position, playerPos.position, ref speed, 0.05f);


        if (SerialInput.DirDownButton > 0 )
        {
            transform.position = new Vector3(targetPos.x, targetPos.y, targetPos.z) + playerPos.forward;
        }
        else
        {
            transform.position = new Vector3(targetPos.x, targetPos.y, targetPos.z);
        }
    }
}