using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{


    // Start is called before the first frame update
    bool paused = false;
    public Canvas pauseCanv;
    //public Canvas mainCanv;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu();
        }
    }


    public void pauseMenu()
    {
        if (!paused)
        {

            pauseCanv.gameObject.SetActive(true);
            //mainCanv.gameObject.SetActive(false);
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
            pauseCanv.gameObject.SetActive(false);
            //mainCanv.gameObject.SetActive(true);
        }

        paused = !paused;
    }


    public void exit()
    {
        Application.Quit();
    }
}