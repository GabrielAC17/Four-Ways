using UnityEngine;
using System.Collections;

public class BulletDirection : MonoBehaviour
{
    private Transform player;
    private Vector3 direction = new Vector3();

    private bool wait = true;
    public float waitTime = 5;
    private float oritime;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player1").GetComponent<Transform>();
        transform.position = GameObject.Find("ShootStart1").GetComponent<Transform>().position;
        oritime = waitTime;
        direction = player.forward;
    }

    // Update is called once per frame
    void Update()
    {
        waiter();
        transform.Translate(direction/5);
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
