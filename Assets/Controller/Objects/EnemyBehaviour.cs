using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public int damage = 10;

    private bool wait = false;
    public float waitTime = 0.1f;
    private float oritime;

    // Use this for initialization
    void Start () {
        oritime = waitTime;
    }
	
	// Update is called once per frame
	void Update () {
        waiter();
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

    private void OnTriggerStay(Collider other)
    {
        if (!wait && (other.name.Equals("Player1") || other.name.Equals("Player2")))
        {
            other.gameObject.GetComponent<PlayerHealth>().Damage(damage);
            wait = true;
        }
    }
}
