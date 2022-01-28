using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas controlsCanvas;
    public Canvas creditsCanvas;
    public Canvas levelselectCanvas;
    public bool isCanvasUp;
    public void ControlsMenu()
    {
        mainCanvas.gameObject.SetActive(false);
        controlsCanvas.gameObject.SetActive(true);
        creditsCanvas.gameObject.SetActive(false);
        levelselectCanvas.gameObject.SetActive(false);
        isCanvasUp = true;
        print("MenuActivated");

    }
    public void CreditsMenu()
    {
        mainCanvas.gameObject.SetActive(false);
        controlsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
        levelselectCanvas.gameObject.SetActive(false);
        isCanvasUp = true;
    }
    public void LevelSelectMenu()
    {
        mainCanvas.gameObject.SetActive(false);
        controlsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
        levelselectCanvas.gameObject.SetActive(true);
        isCanvasUp = true;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && isCanvasUp)
        {
            mainCanvas.gameObject.SetActive(true);
            isCanvasUp = false;
            controlsCanvas.gameObject.SetActive(false);
            creditsCanvas.gameObject.SetActive(false);
            levelselectCanvas.gameObject.SetActive(false);
        }
    }
}
