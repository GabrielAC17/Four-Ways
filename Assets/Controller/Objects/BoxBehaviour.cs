using Assets.Controller.Misc;
using Assets.Controller.Players;
using Assets.Controller.Players.Quirks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour {

    private Transform pinPointPos1;
    private Transform pinPointPos2;
    private Rigidbody rig;
    
    private GameObject[] boxes;
    private bool blocker = false;

    private bool is1Holding = false;
    private bool is2Holding = false;

    private bool wait = false;
    public float waitTime = 1.0f;
    private float oritime;

    private bool wait2 = false;
    public float waitTime2 = 1.0f;
    private float oritime2;

    private Player1Quirks p1Quirk;
    private Player2Quirks p2Quirk;

    // Use this for initialization
    void Start () {
        pinPointPos1 = GameObject.Find("PinPoint1").GetComponent<Transform>();
        pinPointPos2 = GameObject.Find("PinPoint2").GetComponent<Transform>();

        p1Quirk = GameObject.Find("Player1").GetComponent<Player1Quirks>();
        p2Quirk = GameObject.Find("Player2").GetComponent<Player2Quirks>();

        rig = GetComponent<Rigidbody>();
        oritime = waitTime;
        oritime2 = waitTime2;
    }
	
	// Update is called once per frame
	void Update () {
        boxes = GameObject.FindGameObjectsWithTag("Caixa");

        foreach (GameObject box in boxes)
        {
            if ((box.GetComponent<BoxBehaviour>().is1Holding == true || box.GetComponent<BoxBehaviour>().is2Holding == true) && box.GetInstanceID() != GetInstanceID())
            {
                blocker = true;
                break;
            }
            blocker = false;
        }

		if (((SerialInput.ActionButton == 1 && !wait) || SerialInput.Action2Button == 1) && is1Holding)
        {
            rig.isKinematic = false;
            if (is1Holding)
                is1Holding = false;
            wait2 = true;
        }

        //TODO: Alterar controles para o controle serial
        if (((Input.GetButton("Action") && !wait) || Input.GetButton("Jump")) && is2Holding)
        {
            rig.isKinematic = false;
            if (is2Holding)
                is2Holding = false;
            wait2 = true;
        }

        if (is1Holding)
        {
            transform.rotation = pinPointPos1.rotation;
            transform.position = new Vector3(pinPointPos1.position.x, pinPointPos1.position.y+0.2f + (GetComponent<Renderer>().bounds.size.y)/2, pinPointPos1.position.z);
            rig.isKinematic = true;
        }
        if (is2Holding)
        {
            transform.rotation = pinPointPos2.rotation;
            transform.position = new Vector3(pinPointPos2.position.x, pinPointPos2.position.y + 0.2f + (GetComponent<Renderer>().bounds.size.y) / 2 , pinPointPos2.position.z);
            rig.isKinematic = true;
        }
        waiter();
        waiter2();
    }

    private void OnCollisionStay(Collision col)
    {
        if (SerialInput.ActionButton == 1 && !is1Holding && !wait2 && !blocker && p1Quirk.currentPower == 1)
        {
            if (col.gameObject.tag == "Player1")
            {
                is1Holding = true;
                wait = true;
            }
        }
        //TODO: Alterar controles para o controle serial
        if (Input.GetButton("Action") && !is2Holding && !wait2 && !blocker && p2Quirk.currentPower == 1)
        {
            if (col.gameObject.tag == "Player2")
            {
                is2Holding = true;
                wait = true;
            }
        }
    }

    void waiter()
    {
        if (wait)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                wait = false;
                waitTime = oritime;
            }
        }
    }

    void waiter2()
    {
        if (wait2)
        {
            waitTime2 -= Time.deltaTime;
            if (waitTime2 <= 0)
            {
                wait2 = false;
                waitTime2 = oritime2;
            }
        }
    }
}
