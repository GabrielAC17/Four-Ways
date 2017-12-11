using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Controller.Players.Quirks
{

    class Player2Quirks : PlayerQuirks
    {
        private bool wait = false;
        public float waitTime = 0.5f;
        private float oritime;

        private bool waitShield = false;
        public float waitTimeShield = 0.5f;
        private float oritimeShield;

        private bool waitBullet = false;
        public float waitTimeBullet = 1f;
        private float oritimeBullet;

        public GameObject barrier;

        public GameObject bullet;

        private Player2Movement player;

        new void Start()
        {
            base.Start();
            oritime = waitTime;
            oritimeShield = waitTimeShield;
            oritimeBullet = waitTimeBullet;
            barrier.SetActive(false);

            player = GetComponent<Player2Movement>();
        }

        new void Update()
        {
            base.Update();
            waiter();
            waiterShield();
            waiterBullet();
            if (!wait)
            {
                if (Input.GetButton("Power1"))
                {
                    CurrentPower = 1;
                }
                else if (Input.GetButton("Power2"))
                {
                    CurrentPower = 2;
                }
                else if (Input.GetButton("Power3"))
                {
                    CurrentPower = 3;
                }
                else if (Input.GetButton("Power4"))
                {
                    CurrentPower = 4;
                }
                wait = true;
            }
            

            if (Input.GetButton("Action"))
            {
                switch(CurrentPower){
                    case 1:
                        //Implementação em objetos
                        break;
                    case 2:
                        EnableShield();
                        break;
                    case 3:
                        Fly(1);
                        break;
                    case 4:
                        Shoot();
                        break;
                    default:
                        break;
                }
            }
            if (Input.GetButton("Action2"))
            {
                switch (CurrentPower)
                {
                    case 1:
                        player.run();
                        break;
                    case 2:
                        player.run();
                        break;
                    case 3:
                        Fly(-1);
                        break;
                    case 4:
                        player.run();
                        break;
                    default:
                        break;
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

        protected void EnableShield()
        {
            if (!waitShield && currentPower == 2)
            {
                barrier.SetActive(true);
                waitShield = true;
            }
        }

        protected void Shoot()
        {
            if (!waitBullet && currentPower == 4)
            {
                Instantiate(bullet);
                waitBullet = true;
            }
        }

        void waiterShield()
        {
            if (waitShield)
            {
                waitTimeShield -= Time.deltaTime;
                if (waitTimeShield <= 0)
                {
                    waitShield = false;
                    waitTimeShield = oritimeShield;
                    barrier.SetActive(false);
                }
            }
        }

        void waiterBullet()
        {
            if (waitBullet)
            {
                waitTimeBullet -= Time.deltaTime;
                if (waitTimeBullet <= 0)
                {
                    waitBullet = false;
                    waitTimeBullet = oritimeBullet;
                }
            }
        }
    }
}
