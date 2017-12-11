using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour {
    public static int currentThings = 0;
    public static int maxtThings = 1;
    public Text thingText;

    private bool thingTouched = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentThings >= maxtThings)
        {
            thingText.text = "YOU WIN!";
        }
	}

    public void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.name.Equals("Player1") || collision.gameObject.name.Equals("Player2")) && !thingTouched)
        {
            thingTouched = true;
            currentThings++;
            thingText.text = "Things: " + currentThings+" / " + maxtThings;
        }
    }
}
