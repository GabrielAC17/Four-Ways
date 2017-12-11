using UnityEngine;
using System.Collections;

public class Bullet2Direction : MonoBehaviour
{
    private Transform player;
    private Vector3 direction = new Vector3();

    private bool wait = true;
    public float waitTime = 5;
    private float oritime;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player2").GetComponent<Transform>();
        transform.position = GameObject.Find("ShootStart2").GetComponent<Transform>().position;
        oritime = waitTime;
        direction = player.forward;
    }

    // Update is called once per frame
    void Update()
    {
        waiter();
        transform.Translate(direction / 5);
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
                Destroy(gameObject);
            }
        }
    }
}
