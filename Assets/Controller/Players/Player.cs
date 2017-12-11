using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Controller.Players
{
    public class Player : MonoBehaviour
    {
        private Rigidbody rb;
        public int maxSpeed = 2;
        protected bool isFalling = false;
        protected int speed = 1300;

        private bool wait = false;
        public float waitTime = 0.5f;
        private float oritime;

        // Use this for initialization
        protected void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.freezeRotation = true;
            oritime = waitTime;
        }

        protected void Update()
        {
            waiter();
        }

        // Update is called once per frame
        protected void FixedUpdate()
        {
            if (Physics.Raycast(transform.position,Vector3.down,0.05f))
            {
                isFalling = false;
            }
            else
            {
                isFalling = true;
            }
            
            Debug.DrawRay(new Vector3(transform.position.x, -0.01f, transform.position.z), Vector3.down, Color.green);
        }

        protected void Move(Vector3 direction)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15F);
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.velocity = Vector3.Project(rb.velocity, transform.forward);
                rb.AddForce(transform.forward * speed * rb.mass * Time.deltaTime);
            }
        }

        protected void Jump(int strength)
        {
            if (!isFalling)
            {
                rb.AddForce(0, strength * rb.mass, 0);
                isFalling = true;
            }
        }

        public void run()
        {
            speed = 2600;
            wait = true;
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
                    speed = 1300;
                }
            }
        }
    }
}
