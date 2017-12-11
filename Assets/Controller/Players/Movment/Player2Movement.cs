using Assets.Controller.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : Player {
    private Transform camTransform;
    private Vector3 currentDirection;
    private Animator anim;

    private bool isWalking = false;
    private bool isJumping = false;
    private int currentAnim = 0;
    new void Start () {
        base.Start();
        anim = GetComponent<Animator>();
        camTransform = GameObject.Find("Camera2").transform;
	}
	
	new void FixedUpdate() {
        base.FixedUpdate();
        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                Move(new Vector3(camTransform.forward.x, 0, camTransform.forward.z));
            }
            else
            {
                Move(new Vector3(-camTransform.forward.x, 0, -camTransform.forward.z));
            }
                
            if (!isWalking && !isJumping)
            {
                anim.SetInteger("AnimState", 1);
                isWalking = true;
            }
        }
        else
        {
            if (isWalking && !isJumping)
            {
                anim.SetInteger("AnimState", 0);
                isWalking = false;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump(300);
            isJumping = true;
            currentAnim = anim.GetInteger("AnimState");
            anim.SetInteger("AnimState", 2);
            
        }

        if (isJumping && !isFalling)
        {
            isJumping = false;
            anim.SetInteger("AnimState", currentAnim);
        }
    }
}
