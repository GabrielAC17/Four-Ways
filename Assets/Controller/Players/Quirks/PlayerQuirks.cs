using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerQuirks : MonoBehaviour
{
    private Vector3 oriGravity = new Vector3();
    public Sprite[] powerImages = new Sprite[4];
    public Image powerHUD;
    public int currentPower = 1;
    public static bool enableAllPowerUps = false;

    private bool waitGravity = false;
    public float waitTimeGravity = 1.0f;
    private float oritimeGravity;

    public int CurrentPower
    {
        get
        {
            return currentPower;
        }

        set
        {
            if (currentPower != 2)
                ResetFly();
            currentPower = value;
            switch (currentPower)
            {
                case 1:
                    powerHUD.sprite = powerImages[0];
                    break;
                case 2:
                    powerHUD.sprite = powerImages[1];
                    break;
                case 3:
                    powerHUD.sprite = powerImages[2];
                    break;
                case 4:
                    powerHUD.sprite = powerImages[3];
                    break;
                default:
                    powerHUD.sprite = null;
                    break;

            }
        }
    }

    protected void Start()
    {
        oriGravity = Physics.gravity;
        oritimeGravity = waitTimeGravity;
        //barrier1 = GameObject.Find("Sphere1");
        //barrier2 = GameObject.Find("Sphere2");
    }


    protected void Update()
    {
        waiterGravity();
    }

    protected void Fly(float offset)
    {
        if ((CurrentPower == 3 || enableAllPowerUps) & !waitGravity)
        {
            if (Physics.gravity.y + offset > 0 || Physics.gravity.y + offset < -9.81)
                return;
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y + offset, Physics.gravity.z);
            waitGravity = true;
        }

    }

    protected void ResetFly()
    {
        Physics.gravity = oriGravity;
    }

    protected void setPowerUpState(int value)
    {
        if (value >= 0 && value < 5)
            currentPower = value;
    }

    void waiterGravity()
    {
        if (waitGravity)
        {
            waitTimeGravity -= Time.deltaTime;
            if (waitTimeGravity <= 0)
            {
                waitGravity = false;
                waitTimeGravity = oritimeGravity;
            }
        }
    }
}
