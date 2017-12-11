using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Assets.Controller.Misc;

public class PauseBehaviour : MonoBehaviour
{

    private bool isPaused = false;
    public Button continueButton;

    // Update is called once per frame
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetButton("Pause") || SerialInput.StartButton == 1)
        {
            if (!isPaused)
            {
                Cursor.visible = true;
                for (int i = 0; i < transform.childCount; ++i)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    Time.timeScale = 0f;
                    continueButton.Select();
                }
                isPaused = true;
            }
            else if (isPaused)
            {
                Cursor.visible = false;
                for (int i = 0; i < transform.childCount; ++i)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                    Time.timeScale = 1f;
                }
                isPaused = false;
            }
        }
    }

    public void continuegame()
    {
        if (!isPaused)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                Time.timeScale = 0f;

            }
            isPaused = true;
        }
        else if (isPaused)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            isPaused = false;
        }

    }

    public void exitgame()
    {
        Time.timeScale = 1f;
        //Application.LoadLevel(0);
        Application.Quit();
    }
}
