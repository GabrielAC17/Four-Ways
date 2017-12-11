using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayerHealth : MonoBehaviour {
    public bool DayTime = false;
    private bool wait = false;
    public float waitTime = 1.0f;
    private float oritime;
    private bool isDead = false;

    private int health = 100;
    public int damageByDark = 20;
    public int regenerationRate = 1;
    private bool isAtLight = true;
    
    public Slider healthSlider;
    //public Image img;
    //public Color ce;
    
    public Image deathImg;

    public AudioClip[] audioClip;
    public AudioSource au;
    private bool isPlaying = false;
    private bool isPlayingDeath = false;


    private bool waitToStartRegen = false;
    public float waitTimeToStartRegen = 2.0f;
    private float oriTimeToStartRegen;

    private bool waitRegen = false;
    public float waitTimeRegen = 1.0f;
    private float oriTimeRegen;

    // Use this for initialization
    void Start () {
        oritime = waitTime;
        oriTimeToStartRegen = waitTimeToStartRegen;
        oriTimeRegen = waitTimeRegen;
        //img = GetComponent<Image>();
        //ce = img.color;

        Time.timeScale = 1f;

        if (!DayTime)
        {
            isAtLight = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (DayTime)
        {
            isAtLight = true;
        }

        healthSlider.value = health;

        waiter();
        waiterToStartRegen();
        waiterRegen();

        if (isAtLight && health < 100 && !isDead)
        {
            if (!waitToStartRegen && !waitRegen)
            {
                health += regenerationRate;
                waitRegen = true;
            }
            //if (health >= 80)
            //{
            //    au.Stop();
            //}
        }

        if (health > 100)
        {
            health = 100;
        }

        if (!wait && !isAtLight)
        {
            health -= damageByDark;
            if (!isPlaying)
            {
                PlaySound(0, false);
                isPlaying = true;
            }
            wait = true;
        }

        if (health <= 0)
        {
            if (!isPlayingDeath)
            {
                isDead = true;
                isPlaying = true;
                PlaySound(1, false);

                Color colordie = deathImg.color;
                colordie.a = 1;
                deathImg.color = colordie;
                isPlayingDeath = true;
                
            }
            Time.timeScale = 0f;
            if (au.time>=3.3)
            {
                Time.timeScale = 1f;
                isPlayingDeath = false;
                isDead = false;
                //Application.LoadLevel(Application.loadedLevelName);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        //ce.a = 1f - (health / 100f);
        au.volume = 1f - (health / 100f);
        //img.color = ce;
        //Debug.Log(health);
    }


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Caixalamp") {
            isAtLight = true;
            isPlaying = false;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Caixalamp")
        {
            isAtLight = false;
        }
    }


    void PlaySound(int clip, bool loop = false)
    {
        if (loop)
            au.loop = true;
        au.clip = audioClip[0];
        au.Play();
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

    void waiterToStartRegen()
    {
        if (waitToStartRegen)
        {
            waitTimeToStartRegen -= Time.deltaTime;
            if (waitTimeToStartRegen <= 0)
            {
                waitToStartRegen = false;
                waitTimeToStartRegen = oriTimeToStartRegen;
            }
        }
    }

    void waiterRegen()
    {
        if (waitRegen)
        {
            waitTimeRegen -= Time.deltaTime;
            if (waitTimeRegen <= 0)
            {
                waitRegen = false;
                waitTimeRegen = oriTimeRegen;
            }
        }
    }

    public void Damage(int dam)
    {
        health -= dam;
        waitToStartRegen = true;
    }
}
